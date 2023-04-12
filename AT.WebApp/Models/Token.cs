using System.Text.Json.Serialization;

namespace AT.WebApp.Models
{
    public class Token
    {
        [JsonPropertyName("accessToken")]
        public string AccessToken { get; set; }
    }
}
