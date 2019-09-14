using Newtonsoft.Json;

namespace block_auth_api.Models
{
    public class DeviceAuth
    {
        [JsonProperty("message")]
        public string Message { get; set; } 

        [JsonProperty("public_key")]
        public string PublicKey { get; set; } 

    }
}
