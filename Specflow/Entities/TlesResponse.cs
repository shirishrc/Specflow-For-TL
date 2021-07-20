using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Specflow.Entities
{
    public class TlesResponse
    {
        [JsonProperty("requested_timestamp")]
        public string RequestedTimestamp { get; set; }
        [JsonProperty("tle_timestamp")]
        public string TleTimestamp { get; set; }
        [JsonProperty("id")]
        public int id { get; set; }
        public string name { get; set; }
        public string header { get; set; }
        public string line1 { get; set; }
        public string line2 { get; set; }
        
    }
}

