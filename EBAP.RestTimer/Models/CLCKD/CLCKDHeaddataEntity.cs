using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBAP.RestTimer.Models.CLCKD
{
    public class CLCKDHeaddataEntity
    {
        [JsonProperty("vorgcode")]
        public string vorgcode{ get; set; }
        [JsonProperty("billcode")]
        public string billcode	{get;set; }
        [JsonProperty("dbilldate")]
        public string dbilldate	{get;set; }
        [JsonProperty("cwarehouse")]
        public string cwarehouse	{get;set; }
        [JsonProperty("vtrantypecode")]
        public string vtrantypecode	{get;set; }
        [JsonProperty("vdeptcode")]
        public string vdeptcode	{get;set; }
        [JsonProperty("cwhsmanager")]
        public string cwhsmanager	{get;set; }
        [JsonProperty("cbiz")]
        public string cbiz	{get;set; }
        [JsonProperty("vnote")]
        public string vnote	{get;set; }
        [JsonProperty("billmaker")]
        public string billmaker	{get;set; }


        public CLCKDHeaddataEntity() { }

    }
}
