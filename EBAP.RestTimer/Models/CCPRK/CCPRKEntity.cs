using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBAP.RestTimer.Models
{
    public class CCPRKEntity
    {
        [JsonProperty("billtype")]
        private string billtype { get; set; }
        [JsonProperty("billdata")]
        private CCPRKBilldataEntity billdata { get; set; }
        public CCPRKEntity() { }
        public CCPRKEntity(string billtype, CCPRKBilldataEntity billdata)
        {
            this.billtype = billtype;
            this.billdata = billdata;

        }
    }
}
