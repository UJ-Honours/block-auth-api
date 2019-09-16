using block_auth_api.Models.ABI;
using System.Collections.Generic;

namespace block_auth_api.Models
{
    public class ABIOutputs
    {
        public List<ABIComponent> components { get; set; } = new List<ABIComponent>();

        public string name { get; set; }
        public string type { get; set; }
    }
}