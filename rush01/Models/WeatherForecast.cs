using System.Text.Json.Serialization;

namespace rush01.Models
{
    public class WeatherForecast
    {
        /// <summary>
        /// Base class for our weather API
        /// </summary>
        public class Rootobject
        {
            /// <summary>
            /// Wind parameter
            /// </summary>
            [JsonPropertyName("wind")]
            public Wind? Wind { get; set; }

            /// <summary>
            /// Weather parameter
            /// </summary>
            [JsonPropertyName("weather")]
            public Weather[]? Weather { get; set; }

            /// <summary>
            /// Main parameter
            /// </summary>
            [JsonPropertyName("main")]
            public Main? Main { get; set; }

            /// <summary>
            /// Name parameter
            /// </summary>
            [JsonPropertyName("name")]
            public string? Name { get; set; }
        }

        /// <summary>
        /// Main class with temperature, pressure and humidity parameters
        /// </summary>
        public class Main
        {
            private double _temp;

            /// <summary>
            /// Temperature parameter
            /// </summary>
            [JsonPropertyName("temp")]
            public double? Temp
            {
                get => _temp;

                set => _temp = Math.Round((double)(value - 273.15), 1);
            }

            /// <summary>
            /// Pressure parameter
            /// </summary>
            [JsonPropertyName("pressure")]
            public int? Pressure { get; set; }

            /// <summary>
            /// Humidity parameter
            /// </summary>
            [JsonPropertyName("humidity")]
            public int? Humidity { get; set; }
        }

        /// <summary>
        /// Wind class
        /// </summary>
        public class Wind
        {
            /// <summary>
            /// Speed of wind parameter
            /// </summary>
            [JsonPropertyName("speed")]
            public float? Speed { get; set; }
        }

        /// <summary>
        /// Weather class
        /// </summary>
        public class Weather
        {
            /// <summary>
            /// Shortly weather description
            /// </summary>
            [JsonPropertyName("description")]
            public string? Description { get; set; }
        }
    }
}
