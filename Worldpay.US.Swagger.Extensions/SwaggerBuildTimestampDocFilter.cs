using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Interfaces;

using System.Reflection;

using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.OpenApi.Models;
using System.Reflection.Metadata;

namespace Worldpay.US.Swagger.Extensions;

/// <summary>
/// Optionally add a custom OpenAPI Info section extension containing the build timestamp
/// </summary>
/// <remarks>
/// This codes assumes the custom attribute is named: CompileTimestampUTC
/// </remarks>
/// <example>
///     Add this to the csproj file
///     
/// 	<ItemGroup>
///		    <AssemblyAttribute Include = "System.Reflection.AssemblyMetadata" >
///            <_Parameter1> CompileTimestampUTC </_Parameter1>
///            <_Parameter2>$([System.DateTime]::UtcNow.ToString('F'))</_Parameter2>
///		    </AssemblyAttribute>
///	    </ItemGroup>
/// </example>
public class SwaggerBuildTimestampDocFilter : IDocumentFilter
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="swaggerDoc"></param>
    /// <param name="context"></param>
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        var infoExtensions = swaggerDoc.Info.Extensions;

        // add custom info into the OpenAPI file info section
        var customExtensions = BuildCustomExtensions();
        customExtensions.ToList().ForEach(x => infoExtensions.TryAdd(x.Key, x.Value));
    }

    private static IDictionary<string, IOpenApiExtension> BuildCustomExtensions()
    {
        var tabExtensionInfo = new OpenApiObject();

        // add the build timestamp
        tabExtensionInfo.Add(@"build-timestampUTC", new OpenApiString(GetCompileTimeStamp()));

        // build the extension property
        var extensionInfo = new Dictionary<string, IOpenApiExtension>()
        {
            { @"x-build-Info", tabExtensionInfo }
        };

        return extensionInfo;
    }

    private static string GetCompileTimeStamp()
    {
        var assembly = Assembly.GetExecutingAssembly();
        string compileTimestampText = string.Empty;

        try
        {
            // use reflection to get a custom attribute created at compile time and stored in the assembly metadata
            var compileTimestampUTCText = assembly
                                    .GetCustomAttributes<AssemblyMetadataAttribute>()
                                    .First(a => a.Key == "CompileTimestampUTC")
                                    .Value;

            return DateTime.Parse(compileTimestampUTCText).ToString("u");
        }
        catch
        {
            return @"N/A";
        }
    }
}
