using Nethereum.ABI.FunctionEncoding.Attributes;

namespace block_auth_api.Models
{
    [FunctionOutput]
    public class User
    {
        [Parameter("uint", "id", 1)]
        public int Id { get; set; }

        [Parameter("string", "username", 2)]
        public string Username { get; set; }

        [Parameter("address", "account", 3)]
        public string Account { get; set; }

        [Parameter("string", "password", 4)]
        public string Password { get; set; }

        [Parameter("string", "role", 5)]
        public string Role { get; set; }

        [Parameter("string", "token", 6)]
        public string Token { get; set; }

    }
}