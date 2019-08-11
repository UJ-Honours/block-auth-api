using Newtonsoft.Json;

namespace block_auth_api.Models
{
    public class User
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("account")]
        public string Account { get; set; }

        [JsonProperty("is_admin")]
        public bool IsAdmin { get; set; }
    }
}