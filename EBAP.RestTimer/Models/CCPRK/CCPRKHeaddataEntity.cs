using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBAP.RestTimer.Models
{
    public class CCPRKHeaddataEntity
    {
        [JsonProperty("vorgcode")]
        public string vorgcode { get; set; }
        [JsonProperty("billcode")]
        public string billcode { get; set; }
        [JsonProperty("dbilldate")]
        public string dbilldate { get; set; }
        [JsonProperty("cwarehouse")]
        public string cwarehouse { get; set; }
        [JsonProperty("vtrantypecode")]
        public string vtrantypecode { get; set; }
        [JsonProperty("vdeptcode")]
        public string vdeptcode { get; set; }
        [JsonProperty("cwhsmanager")]
        public string cwhsmanager { get; set; }
        [JsonProperty("cbiz")]
        public string cbiz { get; set; }
        [JsonProperty("vnote")]
        public string vnote { get; set; }
        [JsonProperty("billmaker")]
        public string billmaker { get; set; }

        public CCPRKHeaddataEntity() { }
        public CCPRKHeaddataEntity(string vorgcode, string billcode, string dbilldate, string cwarehouse, string vtrantypecode,
             string vdeptcode, string cwhsmanager, string cbiz, string vnote, string billmaker)
        {
            this.vorgcode = vorgcode;
            this.billcode = billcode;
            this.dbilldate = dbilldate;
            this.cwarehouse = cwarehouse;
            this.vtrantypecode = vtrantypecode;
            this.vdeptcode = vdeptcode;
            this.cwhsmanager = cwhsmanager;
            this.cbiz = cbiz;
            this.vnote = vnote;
            this.billmaker = billmaker;

        }

    }
}
