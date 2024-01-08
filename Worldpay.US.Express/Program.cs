using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using Asp.Versioning;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;

using Worldpay.US.Express.Swagger;
using Worldpay.US.Express.v1.Routes;
using Worldpay.US.Express.v2.Routes;
using Worldpay.US.Express.v4.Routes;
using Worldpay.US.Express.Utilities;
using Worldpay.US.Swagger.Extensions;
using Worldpay.US.JWTTokens;

using Worldpay.US.IDP;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Cryptography;

var builder = WebApplication.CreateBuilder(args);

// source generator class to correctly support TimeSpan types
builder.Services.Configure<JsonOptions>(options => options.JsonSerializerOptions.TypeInfoResolverChain.Add(TokenSigningInfoContextBE.Default));

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer()
                .AddApiVersioning(
                    options =>
                    {
                        options.ApiVersionReader = new UrlSegmentApiVersionReader();

                        // reporting api versions will return the headers
                        // "api-supported-versions" and "api-deprecated-versions"
                        options.ReportApiVersions = true;

                        options.Policies.Sunset(1.0)
                                        .Effective(DateTimeOffset.Now.AddDays(60))
                                        .Link("policy.html")
                                            .Title("Versioning Policy")
                                            .Type("text/html");
                    })
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

builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

builder.Services.AddHealthChecks()
                    .AddCheck<HealthChecks>(@"Express");

builder.Services.AddSwaggerGen(
    options =>
    {
        options.AddJWTSecurityDefinition();
        // only show the lock icon on Controllers/Operations that do not have the AllowAnonymous Attribute
        options.OperationFilter<SwaggerApiSecureOperationFilter>();

        // integrate xml comments
        options.AddXmlComments(@"Worldpay");

        // add support for request & response examples
        options.ExampleFilters();

        // enable swagger annotations in Swashbuckle.AspNetCore.Annotations
        options.EnableAnnotations();

        // add filter to add custom Extensions to the info section of the OpenAPI file
        options.DocumentFilter<SwaggerBuildTimestampDocFilter>();

        // add filter to add Tag Descriptions (order listed controls the tag display order)
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
                @"v4",
                new List<TagDescription>()
                {
                    new TagDescription() { Name = @"payments", Description = @"Payments Service endpoints"},
                    new TagDescription() { Name = @"weather", Description = @"Weather Service endpoints"},
                    new TagDescription() { Name = @"debug", Description = @"Debug Service endpoints"}
                }
             }
        };
        options.DocumentFilter<SwaggerTagDescriptionsDocFilter>(apiVersionTagDescriptions);

        options.InferSecuritySchemes();

        // create instance of class supporting custom "Order By Custom Attribute" 
        //var swaggerControllerOrder = new SwaggerTagDisplayOrder(Assembly.GetEntryAssembly());

        // sort the order that the tags are listed using the custom attribute: SwaggerTagDisplayOrder, by default they are alphabetical
        //  Note: "controller" is key in this oob collection even for minimal apis
        //options.OrderActionsBy((apiDesc) => $"{swaggerControllerOrder.SortKey(apiDesc.ActionDescriptor.RouteValues["controller"])}");
    });
builder.Services.AddSwaggerExamplesFromAssemblies(Assembly.GetEntryAssembly());

// for now the JWT singing info is stored in a user secrets file
//  in real use this info would be in an identity provider (ex iDP)
var identityRepoInfo = builder.Configuration.GetSection("identityRepoInfo").Get<IdentityRepoInfoBE>() ?? throw new ArgumentNullException("Missing App Config: [identityRepoInfo]");
// create an instance of the iDP service
var idp = new IdentityService(identityRepoInfo);
builder.Services.AddSingleton(idp);

// configure JWT authentication for Services or endpoints that have the [Authorize] attribute
RSA rsa = RSA.Create();
rsa.ImportSubjectPublicKeyInfo(Convert.FromBase64String(identityRepoInfo.TokenSigningInfo.PublicKey), out int bytesRead);
var securityKey = new RsaSecurityKey(rsa);
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
                        IssuerSigningKey = securityKey
                    };
                });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ValidExpressAuthHeader", policy => policy.Requirements.Add(new ValidExpressAuthHeader()));
});

builder.Services.AddSingleton<IAuthorizationHandler, ExpressAuthorizationHandler>();

var app = builder.Build();

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
    app.UseSwaggerUI(options =>
    {
        options.DocumentTitle = "Express API";          // set the title on the browser tab
        options.DefaultModelsExpandDepth(0);   // by default show schemes section collapsed

        // https://github.com/domaindrivendev/Swashbuckle.AspNetCore?tab=readme-ov-file#working-with-virtual-directories-and-reverse-proxies
        options.RoutePrefix = "swagger";

        // needed this to get all versions to show up in the swagger ui
        options.SwaggerEndpoint("v1/swagger.json", "Version 1.0");
        options.SwaggerEndpoint("v2/swagger.json", "Version 2.0");
        options.SwaggerEndpoint("v4/swagger.json", "Version 4.0");

        #region === Note: This standard code only found 1 version
        // TODO: we need to find out how to get this working for minimal api without the explicit code above
        // build a swagger endpoint for each discovered API version
        //var descriptions = app.DescribeApiVersions();
        //foreach (var description in descriptions)
        //{
        //    var url = $"{description.GroupName}/swagger.json";
        //    var name = description.GroupName.ToUpperInvariant();
        //    options.SwaggerEndpoint(url, name);
        //}
        #endregion
    });
}

// Configure the API Versions
var apis = app.NewVersionedApi();

var v1 = apis.MapGroup("/api/v{version:apiVersion}")
                .MapV1Endpoints()
                .HasApiVersion(1.0);

var v2 = apis.MapGroup("/api/v{version:apiVersion}")
                .MapV2PaymentEndpoints()
                .MapV2DebugEndpoints()
                .MapV2WeatherEndpoints()
                .HasApiVersion(2.0);

var v4 = apis.MapGroup("/api/v{version:apiVersion}")
                .MapV4PaymentEndpoints()
                .MapV4DebugEndpoints()
                .MapV4WeatherEndpoints()
                .HasApiVersion(4.0);

//app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.Run();