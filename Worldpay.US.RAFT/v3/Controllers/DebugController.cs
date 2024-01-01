using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Mvc;

using Asp.Versioning;
using FluentValidation;
using FluentValidation.AspNetCore;
using Swashbuckle.AspNetCore.Annotations;

using Worldpay.US.RAFT.Swagger;
using Worldpay.US.Swagger.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace Worldpay.US.RAFT.v3.Controllers;

/// <summary>
/// This class implements the Debug Service endpoints
/// </summary>
[ApiVersion(3.0)]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[SwaggerControllerDisplayOrder(2)]
[Authorize]
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
    public ActionResult<string> GetVersion(ApiVersion apiVersion) => Ok($"Controller = {GetType().Name}\nVersion = {apiVersion}");

    /// <summary>
    /// Returns all of the HTTP headers in the request
    /// </summary>
    /// <returns></returns>
    [HttpGet(template: "headers", Name = "getHttpHeaders")]
    [Produces("text/plain")]
    [SwaggerOperation(Tags = new[] { "debug" })]
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
}
