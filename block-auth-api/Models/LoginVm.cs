using Newtonsoft.Json;

namespace block_auth_api.Models
{
    public class LoginVM
    {
        [JsonProperty("signer")]
        public string Signer { get; set; } // Ethereum account that claim the signature

        [JsonProperty("signature")]
        public string Signature { get; set; } // The signature

        [JsonProperty("message")]
        public string Message { get; set; } // The plain message

        [JsonProperty("hash")]
        public string Hash { get; set; } // The prefixed and sha3 hashed message 
    }
}
