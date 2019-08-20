using Nethereum.ABI.FunctionEncoding.Attributes;

namespace block_auth_api.Models
{
    [FunctionOutput]
    public class Device
    {
        [Parameter("string", "name", 2)]
        public string Name { get; set; }

        [Parameter("string", "ip", 3)]
        public string Ip { get; set; }

        [Parameter("address", "account", 4)]
        public string Account { get; set; }
    }
}
