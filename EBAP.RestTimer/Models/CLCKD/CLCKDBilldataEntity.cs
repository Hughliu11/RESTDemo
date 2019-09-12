using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBAP.RestTimer.Models.CLCKD
{
    public class CLCKDBilldataEntity
    {
        [JsonProperty("headdata")]
        public CLCKDHeaddataEntity headdata { get; set; }
        [JsonProperty("bodydata")]
        public List<CLCKDBodydataEntity> bodydata { get; set; }

        public CLCKDBilldataEntity()
        {
            this.bodydata = new List<CLCKDBodydataEntity>();

        }
        public CLCKDBilldataEntity(CLCKDHeaddataEntity headdata, List<CLCKDBodydataEntity> bodydata)
        {
            this.headdata = headdata;
            this.bodydata = bodydata;
        }
    }
}
