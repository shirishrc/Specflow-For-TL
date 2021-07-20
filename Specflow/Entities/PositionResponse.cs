using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Specflow.Entities
{
    class PositionResponse
    {
        
        [JsonProperty("timestamp")]
        public string timestamp { get; set; }             
        public string name { get; set; }
        public int id { get; set; }
        public long latitude { get; set; }
        public long longitude { get; set; }
        public double altitude { get; set; }
        public long velocity { get; set; }
        public string visibility { get; set; }
        public string footprint { get; set; }
        public string Altitude { get; set; }
        public string Timestamp { get; set; }
        public double daynum { get; set; }
        public double solar_lat { get; set; }
        public double solar_lon { get; set; }
        public string units { get; set; }       
    }
}
