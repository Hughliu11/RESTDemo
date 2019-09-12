using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBAP.RestTimer.Models.ZYTJD
{
    public class ZYTJDBilldataEntity
    {
        [JsonProperty("headdata")]
        public ZYTJDHeaddataEntity headdata { get; set; }
        [JsonProperty("bodydata")]
        public List<ZYTJDBodydataEntity> bodydata { get; set; }

        public ZYTJDBilldataEntity()
        {
            this.bodydata = new List<ZYTJDBodydataEntity>();

        }
        public ZYTJDBilldataEntity(ZYTJDHeaddataEntity headdata, List<ZYTJDBodydataEntity> bodydata)
        {
            this.headdata = headdata;
            this.bodydata = bodydata;
        }
    }
}
