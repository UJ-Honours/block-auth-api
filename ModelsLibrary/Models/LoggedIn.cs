using Newtonsoft.Json;

namespace block_auth_api.Models
{
    public class LoggedIn
    {
        [JsonProperty("sender")]
        public string Sender { get; set; }

        [JsonProperty("ip")]
        public string Ip { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("role")]
        public string Role { get; set; }

    }
}
