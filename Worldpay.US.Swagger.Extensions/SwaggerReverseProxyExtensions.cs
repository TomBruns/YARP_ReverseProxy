using System.ComponentModel;

using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.Swagger;

namespace Worldpay.US.Swagger.Extensions;

/// <summary>
/// Adds support to create or overwrite the OpenAPI Servers collection using X-Forwared-* headers
/// </summary>
public static class SwaggerReverseProxyExtensions
{
    /// <summary>
    /// Optionally create or overwrite the OpenAPI Servers collection using X-Forwared-* headers
    /// </summary>
    /// <param name="options"></param>
    /// <param name="proxyPrefix">The optional prefix to add to all paths if Swagger is being called via a proxy.</param>
    public static void AddReverseProxyConfig(this SwaggerOptions options, string proxyPrefix)
    {
        options.PreSerializeFilters.Add((document, request) =>
        {
            // presence of X-Forwarded-Host header indicates this service is behind a reverse proxy
            if (!request.Headers.TryGetValue("X-Forwarded-Host", out var proxyHost))
            {
                return;
            }

            if (!request.Headers.TryGetValue("X-Forwarded-Proto", out var proxyScheme))
            {
                return;
            }

            proxyPrefix = proxyPrefix.TrimStart('/').TrimEnd('/');

            if (!string.IsNullOrEmpty(proxyPrefix))
            {
                document.Servers = new List<OpenApiServer> { new OpenApiServer { Url = $"{proxyScheme}://{proxyHost}/{proxyPrefix}" } };
            }
            else
            {
                document.Servers = new List<OpenApiServer> { new OpenApiServer { Url = $"{proxyScheme}://{proxyHost}" } };
            }
        });
    }

    /// <summary>
    /// Optionally create or overwrite the OpenAPI Servers collection using X-Forwared-* headers
    /// </summary>
    /// <param name="options"></param>
    public static void AddReverseProxyConfig(this SwaggerOptions options)
    {
        options.PreSerializeFilters.Add((document, request) =>
        {
            // presence of X-Forwarded-Host header indicates this service is behind a reverse proxy
            if (!request.Headers.TryGetValue("X-Forwarded-Host", out var proxyHost))
            {
                return;
            }

            if (!request.Headers.TryGetValue("X-Forwarded-Proto", out var proxyScheme))
            {
                return;
            }

            // presence of X-Forwarded-Prefix header indicates we need to add a prefix to the route
            var proxyPrefix = string.Empty;
            if (request.Headers.TryGetValue("X-Forwarded-Prefix", out var proxyPrefixes))
            {
                proxyPrefix = proxyPrefixes.FirstOrDefault().TrimStart('/').TrimEnd('/');
            }

            document.Servers = new List<OpenApiServer> { new OpenApiServer { Url = $"{proxyScheme}://{proxyHost}/{proxyPrefix}" } };
        });
    }
}
