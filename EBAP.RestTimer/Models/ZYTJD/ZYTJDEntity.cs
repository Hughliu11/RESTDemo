using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBAP.RestTimer.Models.ZYTJD
{
    public class ZYTJDEntity
    {
        [JsonProperty("billtype")]
        private string billtype { get; set; }
        [JsonProperty("billdata")]
        private ZYTJDBilldataEntity billdata { get; set; }
        public ZYTJDEntity() { }
        public ZYTJDEntity(string billtype, ZYTJDBilldataEntity billdata)
        {
            this.billtype = billtype;
            this.billdata = billdata;

        }
    }
}
