using Nethereum.ABI.FunctionEncoding.Attributes;
using Newtonsoft.Json;

namespace block_auth_api.Models
{
    [FunctionOutput]
    public class User
    {
        [Parameter("string", "id", 1)]
        [JsonProperty("id")]
        public int Id { get; set; }

        [Parameter("string", "name", 2)]
        [JsonProperty("name")]
        public string Name { get; set; }

        [Parameter("address", "account", 3)]
        [JsonProperty("account")]
        public string Account { get; set; }

        [JsonProperty("is_admin")]
        public bool? IsAdmin { get; set; } = false;
    }
}