using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBMProject.Entities.GbmDto
{
    public class IssuerDto
    {
        public IssuerDto() { }
        public IssuerDto(OrderDto order)
        {
            IssuerName = order.IssuerName;
            TotalShares = order.TotalShares;
            SharePrice = order.SharePrice;
        }
        [JsonProperty(PropertyName = "issuerName")]
        public string IssuerName { get; set; }
        [JsonProperty(PropertyName = "totalShares")]
        public uint TotalShares { get; set; }
        [JsonProperty(PropertyName = "sharePrice")]
        public decimal SharePrice { get; set; }        
    }
}
