using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Text.Json;
using WeatherForecast.Model;
using static System.Net.WebRequestMethods;

namespace WeatherForecast.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    //private static readonly string[] Summaries = new[]
    //{
    //    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    //};

    private readonly ILogger<WeatherForecastController> _logger;
    public HttpClient Client { get; }

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
        Client=new HttpClient();
    }

    ///// <summary>
    ///// Return weatherForecast for point with current coordinates
    ///// </summary>
    ///// <param>
    ///// 
    ///// </param>
    ///// <returns>
    ///// IEnumerable<WeatherForecast>
    ///// </returns>
    ///// <response code="200">Success</response>
    ///// <response code="400">If error occured</response>
    ///// 


    /// <summary>
    /// This is method summary I want displayed
    /// </summary>
    /// <param name="guid">guid as parameter</param>
    /// <param name="page_number">Page number - defaults to 0</param>
    /// <returns>List of objects returned</returns>
    [HttpGet]
    [Description("The image associated with the control")]
    [ProducesResponseType(statusCode: 200)]
    [ProducesResponseType(statusCode: 400)]
    public async Task<ActionResult> GetAsync(string latitude, string longitude)
    {
        string key = "b03a2cfad336d11bd9140ffd92074504";


        string request = $@"https://api.openweathermap.org/data/2.5/weather?lat={latitude}&lon={longitude}&appid={key}";
        
        var response = await Client.GetAsync(request);

        var result = await response.Content.ReadAsStringAsync();
        
        Rootobject ggg = new Rootobject();

        if (response.StatusCode != System.Net.HttpStatusCode.OK)
        {
            return BadRequest();
        }
        try
        {  
            ggg = JsonSerializer.Deserialize<Rootobject>(result);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return Ok(ggg);
    }
}
