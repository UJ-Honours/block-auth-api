using Nethereum.ABI.FunctionEncoding.Attributes;

namespace block_auth_api.Models
{
    [FunctionOutput]
    public class User
    {
        [Parameter("string", "username", 1)]
        public string Username { get; set; }

        [Parameter("address", "account", 2)]
        public string Account { get; set; }

        [Parameter("address", "password", 3)]
        public string Password { get; set; }

        [Parameter("string", "token", 4)]
        public string Token { get; set; }
    }
}