using System.Collections.Generic;
using VotingSystemAPI.Models;

namespace block_auth_api.Models
{
    public class ResourceContractOptions
    {
        public string Endpoint { get; set; }
        public string ContractAddress { get; set; }
        public List<ABIOptions> ABI { get; set; }
    }
}