using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Worldpay.US.Swagger.Extensions;

public static class SwaggerXmlComments
{
    /// <summary>
    /// Adds XML comments from all assys that have a specified name prefix
    /// </summary>
    /// <param name="options"></param>
    /// <param name="assyNamePrefix"></param>
    /// <example>
    ///     Add this to the csproj file
    ///         <GenerateDocumentationFile>true</GenerateDocumentationFile>
    ///         
    ///     Add this to the builder.Services.AddSwaggerGen in Porgram.cs
    ///         options.AddXmlComments(@"Worldpay");
    /// </example>
    public static void AddXmlComments(this SwaggerGenOptions options, string assyNamePrefix)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies()
                .Where(x => x.GetName().Name?.StartsWith(assyNamePrefix) ?? false);

        foreach (var assembly in assemblies)
        {
            var xmlPath = Path.Combine(AppContext.BaseDirectory, $"{assembly.GetName().Name}.xml");
            if (File.Exists(xmlPath))
            {
                options.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
            }
        }
    }
}
