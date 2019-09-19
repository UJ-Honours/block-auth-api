using Nethereum.ABI.FunctionEncoding.Attributes;

namespace block_auth_api.Models
{
    [FunctionOutput]
    public class Device
    {
        [Parameter("uint", "id", 1)]
        public int Id { get; set; }

        [Parameter("string", "name", 2)]
        public string Name { get; set; }

        [Parameter("string", "ip", 3)]
        public string Ip { get; set; }

        [Parameter("string", "status", 4)]
        public string Status { get; set; }

        [Parameter("string","role",5)]
        public string Role { get; set; }

    }
}
