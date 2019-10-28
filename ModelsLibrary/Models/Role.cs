using Nethereum.ABI.FunctionEncoding.Attributes;

namespace block_auth_api.Models
{
    [FunctionOutput]
    public class RolePermission
    {
        [Parameter("bool", "on", 1)]
        public int On { get; set; }

        [Parameter("bool", "off", 2)]
        public string Off { get; set; }

    }
}
