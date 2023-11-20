using System.Text.Json.Serialization;

namespace WeatherForecast.Model
{
    public class Rootobject
    {
        [JsonPropertyName("wind")]
        public Wind? Wind { get; set; }

        [JsonPropertyName("weather")]
        public Weather[]? Weather { get; set; }

        [JsonPropertyName("main")]
        public Main? Main { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }        
    }

    public class Main
    {
        [JsonPropertyName("temp")]
        public float? Temp { get; set; }

        [JsonPropertyName("pressure")]
        public int? Pressure { get; set; }

        [JsonPropertyName("humidity")]
        public int? Humidity { get; set; }        
    }

    public class Wind
    {
        [JsonPropertyName("speed")]
        public float? Speed { get; set; }
    }
    
    public class Weather
    {
        [JsonPropertyName("description")]
        public string? Description { get; set; }
    }
}