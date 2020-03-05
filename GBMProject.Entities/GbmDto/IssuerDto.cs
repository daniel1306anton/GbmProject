using Newtonsoft.Json;

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
        public int TotalShares { get; set; }
        [JsonProperty(PropertyName = "sharePrice")]
        public decimal SharePrice { get; set; }        
    }
}
