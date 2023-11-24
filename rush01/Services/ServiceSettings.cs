using System.Text.Json.Serialization;

namespace rush01.Services
{
    public class ServiceSettings
    {
        [JsonPropertyName("ApiKey")]
        public string? ApiKey { get; set; }        
    }
}
