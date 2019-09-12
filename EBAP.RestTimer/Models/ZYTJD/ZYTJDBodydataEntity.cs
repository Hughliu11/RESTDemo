using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBAP.RestTimer.Models.ZYTJD
{
   public class ZYTJDBodydataEntity
    {
        [JsonProperty("rowno")]
        public string rowno { get; set; }
        [JsonProperty("ccostcentercode")]
        public string ccostcentercode { get; set; }
        [JsonProperty("cactivitycode")]
        public string cactivitycode { get; set; }
        [JsonProperty("nnum")]
        public string nnum { get; set; }
        [JsonProperty("vnote")]
        public string vnote { get; set; }

        public ZYTJDBodydataEntity() { }
    }
}
