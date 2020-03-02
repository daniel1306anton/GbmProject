using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBMProject.Entities.GbmDto
{
    public class OrderDto
    {
        [JsonProperty(PropertyName = "timestamp")]
        public TimeSpan TimeStamp { get; set; }
        [JsonProperty(PropertyName = "operation")]
        public string Operation { get; set; }
        [JsonProperty(PropertyName = "IssuerName")]
        public string IssuerName { get; set; }
        [JsonProperty(PropertyName = "TotalShares")]
        public uint TotalShares { get; set; }
        [JsonProperty(PropertyName = "SharePrice")]
        public decimal SharePrice { get; set; }
        public bool StayFiveMinutes(TimeSpan timeStampReq)
        {
            var moreFiveMinutes = TimeStamp.Add(new TimeSpan(0, 5, 0));
            var lessFiveMinutes = TimeStamp.Subtract(new TimeSpan(0, 5, 0));

            return !(timeStampReq >= moreFiveMinutes && timeStampReq <= lessFiveMinutes);
        }
    }
}
