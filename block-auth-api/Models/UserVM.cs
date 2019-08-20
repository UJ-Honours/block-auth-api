using Newtonsoft.Json;

namespace block_auth_api.Models
{
    public class UserVM
    {
        [JsonProperty("account")]
        public string Account { get; set; } // Unique account name (the Ethereum account)

        [JsonProperty("name")]
        public string Name { get; set; } // The user name

        [JsonProperty("email")]
        public string Email { get; set; } // The user Email
    }
}
