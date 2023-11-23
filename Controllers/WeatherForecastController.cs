using Microsoft.AspNetCore.Mvc;
using rush01.Services;
using System.ComponentModel;

namespace WeatherForecast.Controllers;

/// <summary>
/// WeatherForecastController
/// </summary>
[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly IWeatherClient _weatherClient;

    /// <summary>
    /// WeatherForecastController Constructor
    /// </summary>
    /// <param name="client">
    /// WeatherClient object 
    /// </param>
    public WeatherForecastController(IWeatherClient client)
    {       
        _weatherClient = client;
    }

    /// <summary>
    /// Get weatherForecast for point with current coordinates
    /// </summary>
    /// <param>
    /// latitude and longitude
    /// </param>
    /// <returns>
    /// Object our model
    /// </returns>
    /// <response code="200">The request succeeded</response>
    /// <response code="400">Bad request</response>
    [HttpGet("{latitude}, {longitude}")]
    [Description("Get weatherForecast for point with current coordinates")]
    [ProducesResponseType(statusCode: 200)]
    [ProducesResponseType(statusCode: 400)]
    public Task<ActionResult> Get(string latitude, string longitude) =>
       _weatherClient.GetAsync(latitude, longitude);

    /// <summary>
    /// Get weatherForecast for the required city
    /// </summary>
    /// <param>
    /// City name
    /// </param>
    /// <returns>
    /// Object our model
    /// </returns>
    /// <response code="200">The request succeeded</response>
    /// <response code="400">Bad request</response>
    [HttpGet]
    [Route("/[controller]/{city}")]
    [Description("Get weatherForecast for the required city")]
    [ProducesResponseType(statusCode: 200)]
    [ProducesResponseType(statusCode: 400)]
    public Task<ActionResult> Get(string city) =>
      _weatherClient.GetAsync(city);
}
