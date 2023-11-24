using Microsoft.AspNetCore.Mvc;

namespace rush01.Services
{
    public interface IWeatherClient
    {
        public Task<ActionResult> GetAsync(string? city = null);

        public Task<ActionResult> GetAsync(string latitude, string longitude);

        public ActionResult Post(string defaultCityName);
    }
}
