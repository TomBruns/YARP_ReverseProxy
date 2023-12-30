using System.Text;

using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace Worldpay.US.Express.Swagger
{
    /// <summary>
    /// Configures the Swagger generation options.
    /// </summary>
    /// <remarks>This allows API versioning to define a Swagger document per API version after the
    /// <see cref="IApiVersionDescriptionProvider"/> service has been resolved from the service container.</remarks>
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider provider;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigureSwaggerOptions"/> class.
        /// </summary>
        /// <param name="provider">The <see cref="IApiVersionDescriptionProvider">provider</see> used to generate Swagger documents.</param>
        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) => this.provider = provider;

        /// <inheritdoc />
        public void Configure(SwaggerGenOptions options)
        {
            // add a swagger document for each discovered API version
            // note: you might choose to skip or document deprecated API versions differently
            foreach (var description in provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
            }
        }

        private static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
        {
            var text = new StringBuilder();

            var info = new OpenApiInfo()
            {
                Title = "Express API",
                Version = description.ApiVersion.ToString(),
                Contact = new OpenApiContact() { Name = "Tom Bruns", Email = "xtobr39@hotmail.com" },
                License = new OpenApiLicense() { Name = "MIT", Url = new Uri("https://opensource.org/licenses/MIT") }
            };

            switch (description.ApiVersion.ToString())
            {
                case "1.0":
                    info.Description = @"<b>Minimal API</b> based webAPIs endpoints
                                    <br/>
                                    <br/> 
                                    Features:
                                    <pre>   <span>&#8226;</span>  This Version will be marked as deprecated.
                                    <br/> 
                            <table>
                                <thead>
                                    <tr>
                                        <th>Date</th>
                                        <th>Version</th>
                                        <th>Changes</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td>2023/12/19</td>
                                        <td>v1.0</td>
                                        <td>Alpha implementation</td>
                                    </tr>
                                </tbody>
                            </table>";
                    break;

                case "2.0":
                    info.Description = @"<b>Minimal API</b> based webAPIs endpoints
                                    <br/>
                                    <br/> 
                                    Features:
                                    <pre>   <span>&#8226;</span>  Implemented using newer minimal api style
                                    <pre>   <span>&#8226;</span>  Supports multiple API Versions
                                    <pre>   <span>&#8226;</span>  Uses FluentValidation.AspNetCore to validate input parms
                                    <pre>   <span>&#8226;</span>   Convert FluentValidation results to ProblemDetails
                                    <pre>   <span>&#8226;</span>  Uses Swashbuckle.AspNetCore for Swagger Support
                                    <pre>   <span>&#8226;</span>   Uses custom IDocumentFilter to add build d/t to info section of openapi file
                                    <pre>   <span>&#8226;</span>   Uses custom IDocumentFilter to add Tag descriptions which are not supported OOB on minimal APIs
                                    <pre>   <span>&#8226;</span>   Uses custom attribute to control order that tag groups are displayed
                                    <pre>   <span>&#8226;</span>   Uses custom PreSerializeFilter to use X-Forwared-* headers when behind a reverse proxy
                                    <br/> 
                            <table>
                                <thead>
                                    <tr>
                                        <th>Date</th>
                                        <th>Version</th>
                                        <th>Changes</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td>2023/12/19</td>
                                        <td>v2.0</td>
                                        <td>Alpha implementation</td>
                                    </tr>
                                </tbody>
                            </table>";
                    break;
            };

            if (description.IsDeprecated)
            {
                text.Append(" This API version has been deprecated.");
            }

            if (description.SunsetPolicy is SunsetPolicy policy)
            {
                if (policy.Date is DateTimeOffset when)
                {
                    text.Append(" The API will be sunset on ")
                        .Append(when.Date.ToShortDateString())
                        .Append('.');
                }

                if (policy.HasLinks)
                {
                    text.AppendLine();

                    for (var i = 0; i < policy.Links.Count; i++)
                    {
                        var link = policy.Links[i];

                        if (link.Type == "text/html")
                        {
                            text.AppendLine();

                            if (link.Title.HasValue)
                            {
                                text.Append(link.Title.Value).Append(": ");
                            }

                            text.Append(link.LinkTarget.OriginalString);
                        }
                    }
                }
            }

            info.Description = $"{info.Description} {text.ToString()}";

            return info;
        }
    }
}
