using Nethereum.ABI.FunctionEncoding.Attributes;
using System;

namespace ModelsLibrary.Models
{
    [FunctionOutput]
    public class Log
    {
        [Parameter("uint", "id", 1)]
        public int LogID {get;set;}

        [Parameter("string", "action", 2)]
        public string Action {get;set;}

        [Parameter("address", "account", 3)]
        public string Account {get;set;}

        [Parameter("string", "role", 5)]
        public string Role {get;set;}
    }
}
