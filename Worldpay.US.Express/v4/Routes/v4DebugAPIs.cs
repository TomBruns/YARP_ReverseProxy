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
        // GET /debug/claimsFromAuthHeader
        // ===================
        group.MapGet($"/{ROUTE_GROUP_PREFIX}/claimsFromAuthHeader", (HttpContext context) =>
        {
            var authenticatedUser = context.User;

            var claims = new StringBuilder();

            // loop thru all the headers
            foreach (var claim in authenticatedUser.Claims)
            {
                claims.AppendLine($"[{claim.Type}]: {claim.Value}");
            }

            return Results.Text(claims.ToString());
        })
        .RequireAuthorization()
        .WithName("getClaimsFromAuthHeaderv4")
        .WithTags("debug")
        .Produces<string>(StatusCodes.Status200OK, @"text/plain")
        .WithOpenApi(operation => new(operation)
        {
            Summary = @"Returns the list of claims in the Auth Header.",
            Description = @"Longer info",
        });

        // ===================
        // GET /debug/hmacjwt?apiKey=1234
        // ===================
        group.MapGet($"/{ROUTE_GROUP_PREFIX}/hmacjwt", Results<Ok<string>, BadRequest<ProblemDetails>> (HttpRequest request, [FromServices] IdentityService identityService, [FromQuery] string apiKey) =>
        {
            var callerIdentity = identityService.ValidateAPIKey(apiKey);

            if (callerIdentity == null)
            {
                return TypedResults.BadRequest<ProblemDetails>(new ProblemDetails()
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = $"apiKey: [{apiKey}] is not valid.",
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
                });
            }

            (bool isValid, string jwtString) = identityService.CreateHMACJWT(callerIdentity);

            if (!isValid)
            {
                return TypedResults.BadRequest<ProblemDetails>(new ProblemDetails()
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = $"apiKey: [{apiKey}] is not valid.",
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
                });
            }

            return TypedResults.Ok<string>($"apiKey = [{apiKey}], hmacjwt = [{jwtString}]");
        })
        .AllowAnonymous()
        .WithName("getHMACJWTv4")
        .WithTags("debug")
        .Produces<string>(StatusCodes.Status200OK, @"text/plain")
        .Produces<ProblemDetails>(StatusCodes.Status400BadRequest, @"application/problem+json")
        .WithOpenApi(operation => new(operation)
        {
            Summary = @"Returns a test HMAC signed jwt for a API Key.",
            Description = @"Longer info",
        });

        // ===================
        // POST /debug/hmacjwt
        // ===================
        group.MapPost($"/{ROUTE_GROUP_PREFIX}/hmacjwt", Results<Ok<Dictionary<string, string>>, UnauthorizedHttpResult, BadRequest<ProblemDetails>> (HttpRequest request, [FromServices] IdentityService identityService, [FromBody] string hmacToken) =>
        {
            (bool isValid, Dictionary<string, string> claims) = identityService.ValidateHMACJWT(hmacToken);

            if (!isValid)
            {
                return TypedResults.BadRequest<ProblemDetails>(new ProblemDetails()
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = (hmacToken.Length > 10) ? $"hmacToken [{hmacToken[0..10]}...] is not valid." : $"hmacToken [{hmacToken}] is not valid.",
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
                });
            }

            return TypedResults.Ok<Dictionary<string, string>>(claims);
        })
        .AllowAnonymous()
        .WithName("postHMACJWTv4")
        .WithTags("debug")
        .Produces<Dictionary<string, string>>(StatusCodes.Status200OK, @"application/json")
        .Produces<ProblemDetails>(StatusCodes.Status400BadRequest, @"application/problem+json")
        .WithOpenApi(operation => new(operation)
        {
            Summary = @"Validates the hmac (symmetric) signed jwt and returns a list of the claims.",
            Description = @"Longer info",
        });

        // ===================
        // GET /debug/rsajwt?apiKey=1234
        // ===================
        group.MapGet($"/{ROUTE_GROUP_PREFIX}/rsajwt", Results<Ok<string>, BadRequest<ProblemDetails>> (HttpRequest request, [FromServices] IdentityService identityService, [FromQuery] string apiKey) =>
        {
            var callerIdentity = identityService.ValidateAPIKey(apiKey);

            if (callerIdentity == null)
            {
                return TypedResults.BadRequest<ProblemDetails>(new ProblemDetails()
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = $"apiKey: [{apiKey}] is not valid.",
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
                });
            }

            (bool isValid, string jwtString) = identityService.CreateRSAJWT(callerIdentity);

            if (!isValid)
            {
                return TypedResults.BadRequest<ProblemDetails>(new ProblemDetails()
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = $"apiKey: [{apiKey}] is not valid.",
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
                });
            }

            return TypedResults.Ok<string>($"apiKey = [{apiKey}], rsajwt = [{jwtString}]");
        })
        .AllowAnonymous()
        .WithName("getRSAJWTv4")
        .WithTags("debug")
        .Produces<string>(StatusCodes.Status200OK, @"text/plain")
        .Produces<ProblemDetails>(StatusCodes.Status400BadRequest, @"application/problem+json")
        .WithOpenApi(operation => new(operation)
        {
            Summary = @"Returns a test RSA signed jwt for a API Key.",
            Description = @"Longer info",
        });

        // ===================
        // POST /debug/hmacjwt
        // ===================
        group.MapPost($"/{ROUTE_GROUP_PREFIX}/rsajwt", Results<Ok<Dictionary<string, string>>, UnauthorizedHttpResult, BadRequest<ProblemDetails>> (HttpRequest request, [FromServices] IdentityService identityService, [FromBody] string rsaToken) =>
        {
            (bool isValid, Dictionary<string, string> claims) = identityService.ValidateRSAJWT(rsaToken);

            if (!isValid)
            {
                return TypedResults.BadRequest<ProblemDetails>(new ProblemDetails()
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = (rsaToken.Length > 10) ? $"rsaToken [{rsaToken[0..10]}...] is not valid." : $"rsaToken [{rsaToken}] is not valid.",
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
                });
            }

            return TypedResults.Ok<Dictionary<string, string>>(claims);
        })
        .AllowAnonymous()
        .WithName("postRSAJWTv4")
        .WithTags("debug")
        .Produces<Dictionary<string, string>>(StatusCodes.Status200OK, @"application/json")
        .Produces<ProblemDetails>(StatusCodes.Status400BadRequest, @"application/problem+json")
        .WithOpenApi(operation => new(operation)
        {
            Summary = @"Validates the rsa (asymmetric) signed jwt and returns a list of the claims.",
            Description = @"Longer info",
        });

        return group;
    }
}
