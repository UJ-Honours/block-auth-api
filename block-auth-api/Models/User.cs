using Newtonsoft.Json;

namespace block_auth_api.Models
{
    public class User
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("is_admin")]
        public bool IsAdmin { get; set; }
    }
}