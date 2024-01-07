using System.Net.Http;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

using Asp.Versioning;
using FluentValidation;
using FluentValidation.AspNetCore;
using Swashbuckle.AspNetCore.Annotations;

using Worldpay.US.RAFT.Swagger;
using Worldpay.US.Swagger.Extensions;
using Worldpay.US.IDP;
using Worldpay.US.JWTTokens;

namespace Worldpay.US.RAFT.v3.Controllers;

/// <summary>
/// This class implements the Debug Service endpoints
/// </summary>
[ApiVersion(3.0)]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[SwaggerControllerDisplayOrder(2)]
public class DebugController : ControllerBase
{
    private readonly ILogger<DebugController> _logger;

    /// <summary>
    /// Returns Debug Info about this service version
    /// </summary>
    /// <param name="logger"></param>
    public DebugController(ILogger<DebugController> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Returns the API Version
    /// </summary>
    /// <param name="apiVersion"></param>
    /// <returns></returns>
    [HttpGet(template: "version", Name = "getVersion")]
    [Produces("text/plain")]
    [SwaggerOperation(Tags = new[] { "debug" })]
    [AllowAnonymous]
    public ActionResult<string> GetVersion(ApiVersion apiVersion) => Ok($"Controller = {GetType().Name}\nVersion = {apiVersion}");

    /// <summary>
    /// Returns all of the HTTP headers in the request
    /// </summary>
    /// <returns></returns>
    [HttpGet(template: "headers", Name = "getHttpHeaders")]
    [Produces("text/plain")]
    [SwaggerOperation(Tags = new[] { "debug" })]
    [Authorize]
    public ActionResult<string> GetHttpHeaders()
    {
        var headers = new StringBuilder();
        // loop thru all the headers
        foreach (var key in Request.Headers.Keys)
        {
            headers.AppendLine($"[{key}]: {Request.Headers[key]}");
        }

        return new OkObjectResult(headers.ToString());
    }

    /// <summary>
    /// Returns the list of claims in the Auth Header
    /// </summary>
    /// <returns>ActionResult&lt;System.String&gt;.</returns>
    [HttpGet(template: "claimsFromAuthHeader", Name = "getClaimsFromAuthHeader")]
    [Produces("text/plain")]
    [SwaggerOperation(Tags = new[] { "debug" })]
    [Authorize]
    public ActionResult<string> GetClaimsFromAuthHeader()
    {
        var authenticatedUser = User;

        var claims = new StringBuilder();

        // loop thru all the headers
        foreach (var claim in authenticatedUser.Claims)
        {
            claims.AppendLine($"[{claim.Type}]: {claim.Value}");
        }

        return new OkObjectResult(claims.ToString());
    }

    /// <summary>
    /// Returns a test HMAC signed jwt for a API Key.
    /// </summary>
    /// <param name="identityService">The identity service.</param>
    /// <param name="merchantId">The merchant id.</param>
    /// <returns>ActionResult&lt;System.String&gt;.</returns>
    [HttpGet(template: "hmacjwt", Name = "getHMACJWT")]
    [Produces("text/plain")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [SwaggerOperation(Tags = new[] { "debug" })]
    [AllowAnonymous]
    public ActionResult<string> GetHMACJWT([FromServices] IdentityService identityService, [FromQuery] string apiKey)
    {
        var callerIdentity = identityService.ValidateAPIKey(apiKey);

        if (callerIdentity == null)
        {
            return new UnauthorizedResult();
        }

        (bool isValid, string jwtString) = identityService.CreateHMACJWT(callerIdentity);

        if (!isValid)
        {
            return new BadRequestObjectResult(new ProblemDetails()
            {
                Status = StatusCodes.Status400BadRequest,
                Title = $"apiKey: [{apiKey}] is not valid.",
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
            });
        }

        return new OkObjectResult($"apiKey = [{apiKey}], hmacjwt = [{jwtString}]");
    }

    /// <summary>
    /// Validates the hmac (symmetric) signed jwt and returns a list of the claims.
    /// </summary>
    /// <param name="identityService">The identity service.</param>
    /// <param name="hmacToken">The hmac token.</param>
    /// <returns>ActionResult&lt;Dictionary&lt;System.String, System.String&gt;&gt;.</returns>
    [HttpPost(template: "hmacjwt", Name = "postHMACJWT")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(Dictionary<string, string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [SwaggerOperation(Tags = new[] { "debug" })]
    [AllowAnonymous]
    public ActionResult<Dictionary<string, string>> ValidateHMACJWT([FromServices] IdentityService identityService, [FromBody] string hmacToken)
    {
        (bool isValid, Dictionary<string, string> claims) = identityService.ValidateHMACJWT(hmacToken);

        if (!isValid)
        {
            return new BadRequestObjectResult(new ProblemDetails()
            {
                Status = StatusCodes.Status400BadRequest,
                Title = (hmacToken.Length > 10) ? $"hmacToken [{hmacToken[0..10]}...] is not valid." : $"hmacToken [{hmacToken}] is not valid.",
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
            });
        }

        return new OkObjectResult(claims);
    }

    /// <summary>
    /// Returns a test RSA signed jwt for a API Key.
    /// </summary>
    /// <param name="identityService">The identity service.</param>
    /// <param name="merchantId">The merchant id.</param>
    /// <returns>ActionResult&lt;System.String&gt;.</returns>
    [HttpGet(template: "rsajwt", Name = "getRSAJWT")]
    [Produces("text/plain")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [SwaggerOperation(Tags = new[] { "debug" })]
    [AllowAnonymous]
    public ActionResult<string> GetRSAJWT([FromServices] IdentityService identityService, [FromQuery] string apiKey)
    {
        var callerIdentity = identityService.ValidateAPIKey(apiKey);

        if (callerIdentity == null)
        {
            return new UnauthorizedResult();
        }

        (bool isValid, string jwtString) = identityService.CreateRSAJWT(callerIdentity);

        if (!isValid)
        {
            return new BadRequestObjectResult(new ProblemDetails()
            {
                Status = StatusCodes.Status400BadRequest,
                Title = $"apiKey: [{apiKey}] is not valid.",
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
            });
        }

        return new OkObjectResult($"apiKey = [{apiKey}], rsajwt = [{jwtString}]");
    }

    /// <summary>
    /// Validates the rsa (asymmetric) signed jwt and returns a list of the claims.
    /// </summary>
    /// <param name="identityService">The identity service.</param>
    /// <param name="rsaToken">The RSA token.</param>
    /// <returns>ActionResult&lt;Dictionary&lt;System.String, System.String&gt;&gt;.</returns>
    [HttpPost(template: "rsajwt", Name = "postRSAJWT")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(Dictionary<string, string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [SwaggerOperation(Tags = new[] { "debug" })]
    [AllowAnonymous]
    public ActionResult<Dictionary<string, string>> ValidateRSAJWT([FromServices] IdentityService identityService, [FromBody] string rsaToken)
    {
        (bool isValid, Dictionary<string, string> claims) = identityService.ValidateRSAJWT(rsaToken);

        if (!isValid)
        {
            return new BadRequestObjectResult(new ProblemDetails()
            {
                Status = StatusCodes.Status400BadRequest,
                Title = (rsaToken.Length > 10) ? $"rsaToken [{rsaToken[0..10]}...] is not valid." : $"rsaToken [{rsaToken}] is not valid.",
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
            });
        }

        return new OkObjectResult(claims);
    }
}