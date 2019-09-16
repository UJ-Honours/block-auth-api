using Nethereum.ABI.FunctionEncoding.Attributes;

namespace block_auth_api.Models
{
    [FunctionOutput]
    public class Role
    {
        [Parameter("uint", "id", 1)]
        public int Id { get; set; }

        [Parameter("string", "name", 2)]
        public string Name { get; set; }

        [Parameter("string", "permission", 3)]
        public string Permission { get; set; }

    }
}
