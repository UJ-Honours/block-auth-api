using Nethereum.ABI.FunctionEncoding.Attributes;

namespace block_auth_api.Models
{
    [FunctionOutput]
    public class Device
    {
        [Parameter("string", "name", 1)]
        public string Name { get; set; }

        [Parameter("string", "ip", 2)]
        public string Ip { get; set; }

    }
}
