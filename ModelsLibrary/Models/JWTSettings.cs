using Newtonsoft.Json;

namespace block_auth_api.Models
{
    public class JWTSettings
    {
        [JsonProperty("secret_key")]
        public string SecretKey { get; set; }
    }
}