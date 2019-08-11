using Newtonsoft.Json;

namespace block_auth_api.Models
{
    public class Device
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("account")]
        public string Account { get; set; }

        [JsonProperty("ip")]
        public string Ip { get; set; }
    }
}