using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBAP.RestTimer.Models
{
    public class CCPRKBodydataEntity
    {

        [JsonProperty("rowno")]
        public string rowno { get; set; }
        [JsonProperty("materialcode")]
        public string materialcode { get; set; }
        [JsonProperty("nshouldassistnum")]
        public string nshouldassistnum { get; set; }
        [JsonProperty("vbatchcode")]
        public string vbatchcode { get; set; }
        [JsonProperty("vproductbatch")]      
        public string vproductbatch { get; set; }

        [JsonProperty("casscustid")]
        public string casscustid { get; set; }
        [JsonProperty("vnotebody")]
        public string vnotebody { get; set; }
        [JsonProperty("sobillcode")]
        public string sobillcode { get; set; }
        [JsonProperty("customeraddress")]
        public string customeraddress { get; set; }
        [JsonProperty("packets")]
        public string packets { get; set; }

        [JsonProperty("vbdef1")]
        public string vbdef1 { get; set; }

        public CCPRKBodydataEntity() { }
        public CCPRKBodydataEntity(string rowno, string materialcode, string nshouldassistnum, string vbatchcode, string vproductbatch, string casscustid,
            string vnotebody, string sobillcode, string customeraddress, string packets,string vbdef1)
        {

            this.rowno = rowno;
            this.materialcode = materialcode;
            this.nshouldassistnum = nshouldassistnum;
            this.vbatchcode = vbatchcode;
            this.vproductbatch = vproductbatch;
            this.casscustid = casscustid;
            this.vnotebody = vnotebody;
            this.sobillcode = sobillcode;
            this.customeraddress = customeraddress;
            this.packets = packets;
            this.vbdef1 = vbdef1;
            
        }

    }
}
