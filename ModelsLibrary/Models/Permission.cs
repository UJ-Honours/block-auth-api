using Newtonsoft.Json;

namespace block_auth_api.Models
{
    public class Permission
    {
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("selected")]
        public bool Selected { get; set; }
    }
}
