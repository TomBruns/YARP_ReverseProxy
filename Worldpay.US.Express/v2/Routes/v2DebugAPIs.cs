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

namespace Worldpay.US.Express.v2.Routes;

/// <summary>
/// This class defines all the Debug Routes
/// </summary>
[SwaggerTagDisplayOrder(2)]
internal static class v2DebugAPIs
{
    const string ROUTE_GROUP_PREFIX = @"debug";

    public static RouteGroupBuilder MapV2DebugEndpoints(this RouteGroupBuilder group)
    {
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
        .WithName("getHttpHeadersv2")
        .WithTags("debug")
        .Produces<string>(StatusCodes.Status200OK, @"text/plain")
        .WithOpenApi(operation => new(operation)
        {
            Summary = @"Returns all of the HTTP headers in the request.",
            Description = @"Longer info",
        });

        //group.MapGet("/version", (HttpRequest request) =>
        //{
        //});

        return group;
    }
}
