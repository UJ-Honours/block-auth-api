using System.Collections.Generic;
using VotingSystemAPI.Models;

namespace block_auth_api.Models
{
    public class DeviceContractOptions
    {
        public string Endpoint { get; set; }

        public string Address { get; set; }

        public List<ABIOptions> ABI { get; set; }

        public string AdminAccount { get; set; }

        public string ConnectionString { get; set; }
    }
}