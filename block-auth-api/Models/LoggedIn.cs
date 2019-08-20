using Newtonsoft.Json;

namespace block_auth_api.Models
{
    public class LoggedIn
    {
        [JsonProperty("sender")]
        public string Sender { get; set; }

        [JsonProperty("device")]
        public string Device { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }
    }
}
