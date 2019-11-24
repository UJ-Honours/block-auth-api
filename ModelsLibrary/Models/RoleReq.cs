using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModelsLibrary.Models
{
    public class RoleReq
    {
        [JsonProperty("on")]
        public int On { get; set; }

        [JsonProperty("off")]
        public int Off { get; set; }
    }
}
