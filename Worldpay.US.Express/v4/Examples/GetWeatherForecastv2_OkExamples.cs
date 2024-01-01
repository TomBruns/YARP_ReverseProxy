using Swashbuckle.AspNetCore.Filters;

using Worldpay.US.Express.v4.Models;

namespace Worldpay.US.Express.v4.Examples;

/// <summary>
/// This class defines examples for the GetWeatherForecastv2 endpoint for 200 (Ok) responses
/// </summary>
public class GetWeatherForecastv4_OkExamples : IMultipleExamplesProvider<List<WeatherForecastDTO>>
{
    /// <summary>
    /// Return the examples
    /// </summary>
    /// <returns></returns>
    public IEnumerable<SwaggerExample<List<WeatherForecastDTO>>> GetExamples()
    {
        yield return SwaggerExample.Create(
            name: "GetWeatherForecastv2_Default",
            summary: "Default 5 day forecast.",
            description: "This example shows the default response with a 5 day forecast",
            value: new List<WeatherForecastDTO>()
            {
                new WeatherForecastDTO()
                {
                    Date = DateOnly.FromDateTime(DateTime.Today),
                    Summary = @"Sunny",
                    TemperatureC = 30
                },
                new WeatherForecastDTO()
                {
                    Date = DateOnly.FromDateTime(DateTime.Today.AddDays(1)),
                    Summary = @"Warm",
                    TemperatureC = 32
                },
                new WeatherForecastDTO()
                {
                    Date = DateOnly.FromDateTime(DateTime.Today.AddDays(2)),
                    Summary = @"Cold",
                    TemperatureC = 18
                },
                new WeatherForecastDTO()
                {
                    Date = DateOnly.FromDateTime(DateTime.Today.AddDays(3)),
                   Summary = @"Warm",
                    TemperatureC = 32
                },
                new WeatherForecastDTO()
                {
                    Date = DateOnly.FromDateTime(DateTime.Today.AddDays(4)),
                    Summary = @"Cold",
                    TemperatureC = 20
                }
            }
        );
    }
}