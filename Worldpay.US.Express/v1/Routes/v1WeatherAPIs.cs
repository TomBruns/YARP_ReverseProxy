using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

using Asp.Versioning.Builder;
using FluentValidation;
using FluentValidation.AspNetCore;

using Worldpay.US.Express.v1.Models;

namespace Worldpay.US.Express.v1.Routes;

/// <summary>
/// This class defines all the Weather Routes
/// </summary>
internal static class v1WeatherAPIs
{
    const string ROUTE_GROUP_PREFIX = @"weather";

    private static readonly string[] Summaries = new[]
    {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

    public static RouteGroupBuilder MapV1Endpoints(this RouteGroupBuilder group)
    {
        // ===================
        // GET /weather/forecast
        // ===================
        group.MapGet($"/{ROUTE_GROUP_PREFIX}/forecast", ([FromQuery] int? numberOfDays = 5) =>
        {
            #region == Validation the input params
            if (numberOfDays != null)
            {
                var validator = new InlineValidator<int>();
                validator.RuleFor(l => l).InclusiveBetween(1, 5).WithErrorCode("400").WithName(nameof(numberOfDays));
                var validationResults = validator.Validate(numberOfDays.Value);
                if (!validationResults.IsValid)
                {
                    return Results.ValidationProblem(validationResults.ToDictionary());
                }
            }
            #endregion

            var forecast = Enumerable.Range(1, numberOfDays.Value).Select(index => new WeatherForecastDTO
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToList();

            return TypedResults.Ok(forecast);
        })
        .AllowAnonymous()
        .WithName("getWeatherForecastv1")
        .WithTags("weather")
        .Produces<IEnumerable<WeatherForecastDTO>>(StatusCodes.Status200OK, @"application/json")
        .Produces<IEnumerable<ValidationProblemDetails>>(StatusCodes.Status400BadRequest, @"application/json")
        .WithOpenApi(operation => new(operation)
        {
            Summary = @"Return a weather forecast for the next N days.",
            Description = @"Longer info",
        });

        return group;
    }
}

