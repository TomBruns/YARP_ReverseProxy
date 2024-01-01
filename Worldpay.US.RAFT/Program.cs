using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

using Asp.Versioning;
using Swashbuckle.AspNetCore.SwaggerGen;

using Worldpay.US.RAFT.Swagger;
using Worldpay.US.RAFT.Utilities;
using Worldpay.US.Swagger.Extensions;
using Swashbuckle.AspNetCore.Filters;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
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
        //options.OperationFilter<SwaggerDefaultValues>();  // I think this issue may be resolved in the RTM version

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
    });

builder.Services.AddAuthentication("Bearer").AddJwtBearer();

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

app.UseAuthorization();

app.MapControllers();

app.Run();