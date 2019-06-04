using System.Collections.Generic;

namespace VotingSystemAPI.Models
{
    public class ABIOptions
    {
        public bool constant { get; set; }
        public List<ABIInputs> inputs { get; set; } = new List<ABIInputs>();
        public string name { get; set; }
        public List<ABIOutputs> outputs { get; set; } = new List<ABIOutputs>();
        public bool payable { get; set; }
        public string stateMutability { get; set; }
        public string type { get; set; }
    }
}
