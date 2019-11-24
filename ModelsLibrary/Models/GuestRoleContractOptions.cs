using System.Collections.Generic;

namespace block_auth_api.Models
{
    public class GuestRoleContractOptions
    {
        public string Endpoint { get; set; }

        public string Address { get; set; }

        public List<ABIOptions> ABI { get; set; }

        public string AdminAccount { get; set; }

    }
}