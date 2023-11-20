using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Text.Json;
using WeatherForecast.Model;
using static System.Net.WebRequestMethods;

namespace WeatherForecast.Controllers;

/// <summary>
/// WeatherForecastController
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;

    /// <summary>
    /// HttpClient
    /// </summary>
    public HttpClient Client { get; }

    /// <summary>
    /// WeatherForecastController Constructor
    /// </summary>
    /// <param name="logger"></param>
    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
        Client = new HttpClient();
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
    public async Task<ActionResult> GetAsync(string latitude, string longitude)
    {
        string key = "b03a2cfad336d11bd9140ffd92074504";

        string request = $@"https://api.openweathermap.org/data/2.5/weather?lat={latitude}&lon={longitude}&appid={key}";
        
        var response = await Client.GetAsync(request);

        var result = await response.Content.ReadAsStringAsync();
        
        Rootobject? weatherObject = new();

        if (response.StatusCode != System.Net.HttpStatusCode.OK)
        {
            return BadRequest();
        }
        try
        {
            weatherObject = JsonSerializer.Deserialize<Rootobject>(result);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return Ok(weatherObject);
    }
}
