using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.Extensions.Options;
using System.Text.Json.Serialization;

namespace rush01.Services
{
    public class ServiceSettings
    {
        [JsonPropertyName("ApiKey")]
        public string? ApiKey { get; set; }

        //public ServiceSettings()
        //{
        //    var config = new ConfigurationBuilder()
        //        .SetBasePath(Directory.GetCurrentDirectory())
        //        .AddJsonFile("appsettings.json")
        //        .Build();

        //    ApiKey = config["ApiKey"];
        //}
    }
}
