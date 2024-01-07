using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

using Asp.Versioning;
using Asp.Versioning.Builder;
using FluentValidation;
using FluentValidation.AspNetCore;

using Worldpay.US.Express.v2.Models;
using Worldpay.US.Express.Swagger;
using Worldpay.US.Swagger.Extensions;
using Worldpay.US.Express.Utilities;

namespace Worldpay.US.Express.v2.Routes;

/// <summary>
/// This class defines all the Debug Routes
/// </summary>
[SwaggerTagDisplayOrder(2)]
internal static class v4DebugAPIs
{
    const string ROUTE_GROUP_PREFIX = @"debug";

    public static RouteGroupBuilder MapV2DebugEndpoints(this RouteGroupBuilder group)
    {
        // ===================
        // GET /debug/headers
        // ===================
        group.MapGet($"/{ROUTE_GROUP_PREFIX}/headers", (HttpRequest request) =>
        {
            var headers = new StringBuilder();

            // loop thru all the headers
            foreach (var key in request.Headers.Keys)
            {
                headers.AppendLine($"[{key}]: {request.Headers[key]}");
            }

            return Results.Text(headers.ToString());
        })
        .AllowAnonymous()
        .WithName("getHttpHeadersv2")
        .WithTags("debug")
        .Produces<string>(StatusCodes.Status200OK, @"text/plain")
        .WithOpenApi(operation => new(operation)
        {
            Summary = @"Returns all of the HTTP headers in the request.",
            Description = @"Longer info",
        });

        // ===================
        // GET /debug/health
        // ===================
        group.MapGet($"/{ROUTE_GROUP_PREFIX}/health", ([FromServices] IServiceCollection services) =>
        {
            return Results.Text("ok");
        })
        .AllowAnonymous()
        .WithName("getHealthv2")
        .WithTags("debug")
        .Produces<string>(StatusCodes.Status200OK, @"text/plain")
        .WithOpenApi(operation => new(operation)
        {
            Summary = @"Returns The current Health Check setting.",
            Description = @"Longer info",
        });

        return group;
    }
}
