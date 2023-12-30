using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace Worldpay.US.Swagger.Extensions;

/// <summary>
/// Adds descriptions to OpenAPI Tags
/// </summary>
/// <remarks>
/// This is useful for Minimal APIs that do not currently support this
/// Controller style APIs usually use the controller class XML comments
///     Note: if you use this on Controller style APIs, this code will replace those tags
/// </remarks>
/// <example>
/// config like this in builder.Services.AddSwaggerGen
/// 
///     // add filter to add Tag Descriptions
///     var apiVersionTagDescriptions = new Dictionary<string, List<TagDescription>>()
///             {
///                 {
///                     @"v1",
///                     new List<TagDescription>()
///                     {
///                         new TagDescription() { Name = @"weather", Description = @"Weather Service endpoints"}
///                     }
///                 },
///                 {
///                     @"v2",
///                     new List<TagDescription>()
///                     {
///                         new TagDescription() { Name = @"payments", Description = @"Payments Service endpoints"},
///                         new TagDescription() { Name = @"debug", Description = @"Debug Service endpoints"},
///                         new TagDescription() { Name = @"weather", Description = @"Weather Service endpoints"}
///                     }
///                 }
///             };
///     options.DocumentFilter<SwaggerTagDescriptionsDocFilter>(apiVersionTagDescriptions);
/// </example>
public class SwaggerTagDescriptionsDocFilter : IDocumentFilter
{
    private readonly Dictionary<string, List<TagDescription>> _apiVersionTagDescriptions;

    public SwaggerTagDescriptionsDocFilter(Dictionary<string, List<TagDescription>> apiVersionTagDescriptions)
    {
        _apiVersionTagDescriptions = apiVersionTagDescriptions;
    }

    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        if (_apiVersionTagDescriptions.TryGetValue(context.DocumentName, out var tagDescriptions)) 
        {
            swaggerDoc.Tags = tagDescriptions.Select(t => new OpenApiTag() { Name = t.Name, Description = t.Description}).ToList();
        }
    }
}

public class TagDescription
{
    public string Name { get; set; }
    public string Description { get; set; }
}