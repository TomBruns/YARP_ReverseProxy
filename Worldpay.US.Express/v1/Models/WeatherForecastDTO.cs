﻿namespace Worldpay.US.Express.v1.Models;

/// <summary>
/// An object describing the weather for a date
/// </summary>
public record WeatherForecastDTO
{
    /// <summary>
    /// The date of the temperature reading
    /// </summary>
    public DateOnly Date { get; set; }

    /// <summary>
    /// The Temperature in Degrees Centigrade
    /// </summary>
    public int TemperatureC { get; set; }

    /// <summary>
    /// A short description of the weather for a date.
    /// </summary>
    public string? Summary { get; set; }
}

