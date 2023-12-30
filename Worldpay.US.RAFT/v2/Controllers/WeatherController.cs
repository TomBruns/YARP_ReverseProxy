using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Mvc;

using Asp.Versioning;
using FluentValidation;
using FluentValidation.AspNetCore;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

using Worldpay.US.RAFT.v2.Models;
using Worldpay.US.RAFT.v2.Examples;
using Worldpay.US.Swagger.Extensions;

namespace Worldpay.US.RAFT.v2.Controllers;

/// <summary>
/// This class implements the Weather Service endpoints
/// </summary>
[ApiVersion(2.0)]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[SwaggerControllerDisplayOrder(3)]
public class WeatherController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherController> _logger;

    /// <summary>
    /// Create an instance of the Weather Controller
    /// </summary>
    /// <param name="logger"></param>
    public WeatherController(ILogger<WeatherController> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Return a weather forecast for the next N days.
    /// </summary>
    /// <remarks>
    /// Longer info
    /// </remarks>
    /// <param name="numberOfDays">The number of days to return the forecast for (default = 5).</param>
    /// <returns></returns>
    [HttpGet(template: "forecast", Name = "getWeatherForecast")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(IEnumerable<WeatherForecastDTO>), 200)]
    [ProducesResponseType(typeof(ValidationProblemDetails), 400)]
    [SwaggerOperation(Tags = new[] { "weather" })]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetWeatherForecastv2_OkExamples))]
    [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(GetWeatherForecastv2_BadRequestExamples))]
    public ActionResult<IEnumerable<WeatherForecastDTO>> GetWeatherForecast([FromQuery]int? numberOfDays = 5)
    {
        #region == Validation the input params
        if (numberOfDays != null)
        {
            var validator = new InlineValidator<int>();
            validator.RuleFor(l => l).InclusiveBetween(1,5).WithErrorCode("400").WithName(nameof(numberOfDays));
            var results = validator.Validate(numberOfDays.Value);
            if (!results.IsValid)
            {
                results.AddToModelState(this.ModelState, nameof(numberOfDays));
                return new BadRequestObjectResult(new ValidationProblemDetails(this.ModelState));
            }
        }
        #endregion

        var forecast =  Enumerable.Range(1, numberOfDays.Value).Select(index => new WeatherForecastDTO
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();

        return new OkObjectResult(forecast);
    }
}
