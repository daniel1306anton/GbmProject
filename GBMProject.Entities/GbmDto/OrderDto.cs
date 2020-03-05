using Newtonsoft.Json;
using System;

namespace GBMProject.Entities.GbmDto
{
    public class OrderDto
    {
        private DateTime timeStamp;
        private long timeStampTicks;

        public DateTime TimeStamp
        {
            get { return timeStamp; }
            set { timeStamp = value; }
        }
        [JsonProperty(PropertyName = "timestamp")]
        public long TimeStampTicks
        {
            get { return timeStampTicks; }
            set { timeStampTicks = value; timeStamp = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(value).ToLocalTime(); }                
        }

        [JsonProperty(PropertyName = "operation")]
        public string Operation { get; set; }
        [JsonProperty(PropertyName = "IssuerName")]
        public string IssuerName { get; set; }
        [JsonProperty(PropertyName = "TotalShares")]
        public int TotalShares { get; set; }
        [JsonProperty(PropertyName = "SharePrice")]
        public decimal SharePrice { get; set; }
        public bool StayFiveMinutes(DateTime timeStampReq)
        {
            var moreFiveMinutes = TimeStamp.Add(new TimeSpan(0, 5, 0));
            var lessFiveMinutes = TimeStamp.Subtract(new TimeSpan(0, 5, 0));

            return (timeStampReq >= lessFiveMinutes && timeStampReq <= moreFiveMinutes);
        }
    }
}
