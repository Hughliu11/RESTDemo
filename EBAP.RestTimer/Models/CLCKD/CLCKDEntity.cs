using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBAP.RestTimer.Models.CLCKD
{
    public class CLCKDEntity
    {
        [JsonProperty("billtype")]
        private string billtype { get; set; }
        [JsonProperty("billdata")]
        private CLCKDBilldataEntity billdata { get; set; }
        public CLCKDEntity() { }
        public CLCKDEntity(string billtype, CLCKDBilldataEntity billdata)
        {
            this.billtype = billtype;
            this.billdata = billdata;

        }
    }
}
