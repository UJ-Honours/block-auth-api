using Newtonsoft.Json;

namespace block_auth_api.Models
{
    public class BlockAuthAccount
    {
        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("private_key")]
        public string PrivateKey { get; set; }
    }
}
