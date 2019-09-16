using Nethereum.ABI.FunctionEncoding.Attributes;

namespace block_auth_api.Models
{
    [FunctionOutput]
    public class Task
    {
        [Parameter("uint", "id", 1)]
        public int Id { get; set; }

        [Parameter("string", "content", 2)]
        public string Content { get; set; }

        [Parameter("bool", "completed", 3)]
        public bool Completed { get; set; }
    }
}
