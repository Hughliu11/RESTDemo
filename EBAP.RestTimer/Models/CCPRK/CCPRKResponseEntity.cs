﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBAP.RestTimer.Models
{
    public class CCPRKResponseEntity
    {
        [JsonProperty("result")]
        public string result { get; set; }
        [JsonProperty("oldbillcode")]
        public string oldbillcode { get; set; }
        [JsonProperty("info")]
        public string info { get; set; }
        public CCPRKResponseEntity(string result, string oldbillcode, string info)
        {
            this.result = result;
            this.oldbillcode = oldbillcode;
            this.info = info;

        }
    }
}
