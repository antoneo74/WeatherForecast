using Microsoft.AspNetCore.Mvc;
using static rush01.Models.WeatherForecast;
using System.ComponentModel;
using System.Text.Json;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Caching.Memory;

namespace rush01.Services
{
    public class WeatherClient : IWeatherClient
    {
        private readonly string _key;

        private IMemoryCache _cache;

        /// <summary>
        /// HttpClient
        /// </summary>
        public HttpClient Client { get; }

        public WeatherClient(IOptions<ServiceSettings> options, IMemoryCache memoryCache)
        {
            Client = new HttpClient();

            _key = options.Value.ApiKey;

            _cache = memoryCache;
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
            string request = $@"https://api.openweathermap.org/data/2.5/weather?lat={latitude}&lon={longitude}&appid={_key}";

            var response = await Client.GetAsync(request);

            var result = await response.Content.ReadAsStringAsync();

            Rootobject? weatherObject = new();

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return new BadRequestResult();
            }
            try
            {
                weatherObject = JsonSerializer.Deserialize<Rootobject>(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return new OkObjectResult(weatherObject);
        }

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
        /// <response code="404">Not found request</response>
        [HttpGet]
        //[Route("/[controller]/{city?}")]
        [Description("Get weatherForecast for the required city")]
        [ProducesResponseType(statusCode: 200)]
        [ProducesResponseType(statusCode: 400)]
        [ProducesResponseType(statusCode: 404)]
        public async Task<ActionResult> GetAsync(string? city)
        {
            if (city == null)
            {
                _cache.TryGetValue("name", out string defaultCity);
                if (defaultCity == null)
                {
                    return new NotFoundResult();
                }
                city = defaultCity;
            }

            string request = $@"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={_key}";

            var response = await Client.GetAsync(request);

            var result = await response.Content.ReadAsStringAsync();

            Rootobject? weatherObject = new();

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return new BadRequestResult();
            }
            try
            {
                weatherObject = JsonSerializer.Deserialize<Rootobject>(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return new OkObjectResult(weatherObject);
        }

        /// <summary>
        /// Set default city name
        /// </summary>
        /// <param>
        /// City name
        /// </param>
        /// <returns>
        /// Status code
        /// </returns>
        /// <response code="200">The request succeeded</response>
        /// <response code="400">Bad request</response>
        [HttpPost]
        [Description("Set default city name")]
        [ProducesResponseType(statusCode: 200)]
        [ProducesResponseType(statusCode: 400)]
        public ActionResult Post(string defaultCityName)
        {
            try
            {
                _cache?.Remove("name");

                _cache.Set("name", defaultCityName);

                return new OkResult();
            }
            catch (Exception)
            {
                return new BadRequestResult();
            }
        }
    }
}
