using Nethereum.ABI.FunctionEncoding.Attributes;
using Newtonsoft.Json;

namespace block_auth_api.Models
{
    [FunctionOutput]
    public class RolePermission
    {
        [JsonProperty("on")]
        [Parameter("bool", "on", 1)]
        public bool On { get; set; }

        [JsonProperty("off")]
        [Parameter("bool", "off", 2)]
        public bool Off { get; set; }

    }
}
