using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBAP.RestTimer.Models.ZYTJD
{
    public class ZYTJDHeaddataEntity
    {
        [JsonProperty("vorgcode")]
        public string vorgcode { get; set; }
        [JsonProperty("vbillcode")]
        public string vbillcode { get; set; }
        [JsonProperty("dbusinessdate")]
        public string dbusinessdate { get; set; }
        [JsonProperty("cperiod")]
        public string cperiod { get; set; }
        [JsonProperty("ccostcentercode")]
        public string ccostcentercode { get; set; }
        [JsonProperty("ccostobjectcode")]
        public string ccostobjectcode { get; set; }
        [JsonProperty("vnote")]
        public string vnote { get; set; }
        [JsonProperty("billmaker")]
        public string billmaker { get; set; }

        public ZYTJDHeaddataEntity() { }

    }
}
