using Newtonsoft.Json;

namespace block_auth_api.Models
{
    public class Device
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("ip")]
        public string IP { get; set; }
    }
}