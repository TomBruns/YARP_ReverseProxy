using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

using Asp.Versioning;
using Asp.Versioning.Builder;
using FluentValidation;
using FluentValidation.AspNetCore;

using Worldpay.US.Express.v4.Models;
using Worldpay.US.Express.Swagger;
using Worldpay.US.Swagger.Extensions;
using Microsoft.IdentityModel.JsonWebTokens;
using Worldpay.US.JWTTokens;
using Worldpay.US.IDP;
using Microsoft.AspNetCore.Http;

namespace Worldpay.US.Express.v4.Routes;

/// <summary>
/// This class defines all the Debug Routes
/// </summary>
[SwaggerTagDisplayOrder(3)]
internal static class v4DebugAPIs
{
    const string ROUTE_GROUP_PREFIX = @"debug";

    public static RouteGroupBuilder MapV4DebugEndpoints(this RouteGroupBuilder group)
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
        .RequireAuthorization()
        .WithName("getHttpHeadersv4")
        .WithTags("debug")
        .Produces<string>(StatusCodes.Status200OK, @"text/plain")
        .WithOpenApi(operation => new(operation)
        {
            Summary = @"Returns all of the HTTP headers in the request.",
            Description = @"Longer info",
        });

        // ===================
        // GET /debug/hmacjwt?merchantId=1234
        // ===================
        group.MapGet($"/{ROUTE_GROUP_PREFIX}/hmacjwt", (HttpRequest request, [FromServices] IdentityService identityService, [FromQuery] string merchantId) =>
        {
            //var lifeTime = new TimeSpan(0, 0, 15, 0);
            //(bool isValid, string jwtString) = identityService.CreateHMACJWT(merchantId, lifeTime);

            //if (!isValid) 
            //{ 
            //    return Results.BadRequest(new ProblemDetails()
            //    {
            //        Status = StatusCodes.Status400BadRequest,
            //        Title = $"MerchantId: [{merchantId}] is not valid.",
            //        Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
            //    });
            //}

            //return TypedResults.Ok($"merchantId = [{merchantId}], hmacjwt = [{jwtString}]");
        })
        .AllowAnonymous()
        .WithName("gethmacjwtv4")
        .WithTags("debug")
        .Produces<string>(StatusCodes.Status200OK, @"text/plain")
        .Produces<ProblemDetails>(StatusCodes.Status400BadRequest, @"application/problem+json")
        .WithOpenApi(operation => new(operation)
        {
            Summary = @"Returns a test HMAC signed jwt for a MID.",
            Description = @"Longer info",
        });

        // ===================
        // GET /debug/certjwt?merchantCode=1234
        // ===================
        group.MapGet($"/{ROUTE_GROUP_PREFIX}/certjwt", (HttpRequest request, [FromServices] IdentityService identityService, [FromQuery] string merchantCode) =>
        {
            (bool isValid, string jwtString) = identityService.CreateCertJWT(merchantCode);

            if (!isValid)
            {
                return Results.BadRequest(new ProblemDetails()
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = $"Merchant Code: [{merchantCode}] is not valid.",
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
                });
            }

            return Results.Text($"merchantCode = [{merchantCode}], hmacjwt = [{jwtString}]");
        })
        .AllowAnonymous()
        .WithName("getcertjwtv4")
        .WithTags("debug")
        .Produces<string>(StatusCodes.Status200OK, @"text/plain")
        .Produces<ProblemDetails>(StatusCodes.Status400BadRequest, @"application/problem+json")
        .WithOpenApi(operation => new(operation)
        {
            Summary = @"Returns a test Asymetric signed jwt for a MID.",
            Description = @"Longer info",
        });


        return group;
    }
}
