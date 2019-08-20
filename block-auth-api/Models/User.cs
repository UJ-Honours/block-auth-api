using Nethereum.ABI.FunctionEncoding.Attributes;

namespace block_auth_api.Models
{
    [FunctionOutput]
    public class User
    {

        [Parameter("string", "name", 2)]
        public string Name { get; set; }

        [Parameter("address", "account", 3)]
        public string Account { get; set; }

    }
}