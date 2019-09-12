using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBAP.RestTimer.Models.CLCKD
{
    public class CLCKDBodydataEntity
    {
        [JsonProperty("rowno")]
        public string rowno { get; set; }
        [JsonProperty("materialcode")]
       public string materialcode	{get;set;}
        [JsonProperty("nshouldassistnum")]
        public string nshouldassistnum	{get;set;}
        [JsonProperty("ccostobject")]
        public string ccostobject	{get;set;}
        [JsonProperty("vproductbatch")]
        public string vproductbatch	{get;set;}
        [JsonProperty("vnotebody")]
        public string vnotebody	{get;set;}


        public CLCKDBodydataEntity() { }

    }
}
