using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Swashbuckle.AspNetCore.Filters;

using Worldpay.US.Express.v4.Models;

namespace Worldpay.US.Express.v4.Examples;

/// <summary>
/// This class defines examples for the GetWeatherForecastv2 endpoint for 400 (Bad Request) responses
/// </summary>
public class GetWeatherForecastv4_BadRequestExamples : IMultipleExamplesProvider<ValidationProblemDetails>
{
    /// <summary>
    /// Return the examples
    /// </summary>
    /// <returns></returns>
    public IEnumerable<SwaggerExample<ValidationProblemDetails>> GetExamples()
    {
        yield return SwaggerExample.Create(
            name: "GetWeatherForecastv2_OutOfRange",
            summary: "Input Param out of range.",
            description: "This example shows the default response for an out of range input param",
            value: new ValidationProblemDetails()
                {
                    Type = @"https://tools.ietf.org/html/rfc9110#section-15.5.1",
                    Title = @"One or more validation errors occurred.",
                    Status = StatusCodes.Status400BadRequest,
                    Errors = new Dictionary<string, string[]>()
                    {
                        { @"numberOfDays", new List<string> { @"'numberOfDays' must be between 1 and 5. You entered 7." }.ToArray() }
                    }
                }
        );
    }
}
