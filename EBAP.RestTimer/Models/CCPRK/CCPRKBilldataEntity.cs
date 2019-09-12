using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBAP.RestTimer.Models
{
    public class CCPRKBilldataEntity
    {
        [JsonProperty("headdata")]
        public CCPRKHeaddataEntity headdata { get; set; }
        [JsonProperty("bodydata")]
        public List<CCPRKBodydataEntity> bodydata { get; set; }

        public CCPRKBilldataEntity() {
            this.bodydata = new List<CCPRKBodydataEntity>();

        }
        public CCPRKBilldataEntity(CCPRKHeaddataEntity headdata, List<CCPRKBodydataEntity> bodydata)
        {
            this.headdata = headdata;
            this.bodydata = bodydata;
        }
    }
}
