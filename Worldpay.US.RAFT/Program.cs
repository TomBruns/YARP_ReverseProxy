using System.Reflection;
using System.Text;
using System.Xml.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

using Asp.Versioning;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.Filters;

using Worldpay.US.RAFT.Swagger;
using Worldpay.US.RAFT.Utilities;
using Worldpay.US.Swagger.Extensions;
using Worldpay.US.IDP;
using Worldpay.US.JWTTokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
                .AddJsonOptions(
                                    // source generator class to correctly support TimeSpan types
                                    static options => options.JsonSerializerOptions.TypeInfoResolverChain.Add(TokenSigningInfoContextBE.Default)
                                );

builder.Services.AddProblemDetails();

builder.Services.AddApiVersioning(
                    options =>
                    {
                        // reporting api versions will return the headers
                        // "api-supported-versions" and "api-deprecated-versions"
                        options.ReportApiVersions = true;

                        options.Policies.Sunset(1.0)
                                        .Effective(DateTimeOffset.Now.AddDays(60))
                                        .Link("policy.html")
                                            .Title("Versioning Policy")
                                            .Type("text/html");
                    })
                .AddMvc()
                .AddApiExplorer(
                    options =>
                    {
                        // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
                        // note: the specified format code will format the version as "'v'major[.minor][-status]"
                        options.GroupNameFormat = "'v'VVV";

                        // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
                        // can also be used to control the format of the API version in route templates
                        options.SubstituteApiVersionInUrl = true;
                    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

builder.Services.AddHealthChecks()
                    .AddCheck<HealthChecks>(@"RAFT");

builder.Services.AddSwaggerGen(
    options =>
    {
        options.AddJWTSecurityDefinition();
        // only show the lock icon on Controllers/Operations that do not have the AllowAnonymous Attribute
        options.OperationFilter<SwaggerApiSecureOperationFilter>();

        // integrate xml comments
        options.AddXmlComments(@"Worldpay");

        // add a custom operation filter which sets default values
        //options.OperationFilter<SwaggerDefaultValues>();  // I think this issue may be resolved in the .NET 8 RTM version

        // enable swagger annotations in Swashbuckle.AspNetCore.Annotations
        options.EnableAnnotations();

        // add filter to add custom Extensions to OpenAPI info section
        options.DocumentFilter<SwaggerBuildTimestampDocFilter>();

        // create instance of class supporting custom "Order By Custom Attribute" 
        var swaggerControllerOrder = new SwaggerControllerDisplayOrder<ControllerBase>(Assembly.GetEntryAssembly());

        // add filter to add Tag Descriptions
        var apiVersionTagDescriptions = new Dictionary<string, List<TagDescription>>()
        {
            {
                @"v1",
                new List<TagDescription>()
                {
                    new TagDescription() { Name = @"weather", Description = @"Weather Service endpoints"}
                }
            },
            {
                @"v2",
                new List<TagDescription>()
                {
                    new TagDescription() { Name = @"payments", Description = @"Payments Service endpoints"},
                    new TagDescription() { Name = @"debug", Description = @"Debug Service endpoints"},
                    new TagDescription() { Name = @"weather", Description = @"Weather Service endpoints"}
                }
            },
            {
                @"v3",
                new List<TagDescription>()
                {
                    new TagDescription() { Name = @"payments", Description = @"Payments Service endpoints"},
                    new TagDescription() { Name = @"weather", Description = @"Weather Service endpoints"},
                    new TagDescription() { Name = @"debug", Description = @"Debug Service endpoints"}
                }
            }
        };
        options.DocumentFilter<SwaggerTagDescriptionsDocFilter>(apiVersionTagDescriptions);

        // sort the order that the controllers are listed using the custom attribute: SwaggerControllerOrder, by default they are alphabetical
        options.OrderActionsBy((apiDesc) => $"{swaggerControllerOrder.SortKey(apiDesc.ActionDescriptor.RouteValues["controller"])}");

        // inject an "unversioned" endpoint into all versioned swagger pages adding an Endpoint for the OOB Health Check monitoring
        options.DocumentFilter<SwaggerHealthCheckEndpointDocFilter>();
    });
builder.Services.AddSwaggerExamplesFromAssemblies(Assembly.GetEntryAssembly());

// for now the JWT singing info is stored in a user secrets file
//  in real use this info would be in an identity provider (ex iDP)
var identityRepoInfo = builder.Configuration.GetSection("identityRepoInfo").Get<IdentityRepoInfoBE>() ?? throw new ArgumentNullException("Missing App Config: [identityRepoInfo]");
// create an instance of the iDP service
var idp = new IdentityService(identityRepoInfo);
builder.Services.AddSingleton(idp);

// configure JWT authentication for Services or endpoints that have the [Authorize] attribute
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = identityRepoInfo.TokenSigningInfo.Issuer,
                        ValidAudience = identityRepoInfo.TokenSigningInfo.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(identityRepoInfo.TokenSigningInfo.HMACSecret))
                    };
                });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ValidRAFTAuthHeader", policy => policy.Requirements.Add(new ValidRAFTAuthHeader()));
});
builder.Services.AddSingleton<IAuthorizationHandler, RAFTAuthorizationHandler>();

var app = builder.Build();

// add the health check endpoint
app.MapHealthChecks("/health");

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
{
    app.UseSwagger(
        options =>
        {
            // modify OpenAPI servers collection if X-Forwarded HTTP headers exist
            options.AddReverseProxyConfig();
        });
    app.UseSwaggerUI(
        options =>
        {
            options.DocumentTitle = "RAFT API";          // set the title on the browser tab
            options.DefaultModelsExpandDepth(0);   // by default show schemes section collapsed

            // https://github.com/domaindrivendev/Swashbuckle.AspNetCore?tab=readme-ov-file#working-with-virtual-directories-and-reverse-proxies
            options.RoutePrefix = "swagger";

            // build a swagger endpoint for each discovered API version
            var descriptions = app.DescribeApiVersions();
            foreach (var description in descriptions)
            {
                var url = $"{description.GroupName}/swagger.json";
                var name = description.GroupName.ToUpperInvariant();
                options.SwaggerEndpoint(url, name);
            }
        });
}

//app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();