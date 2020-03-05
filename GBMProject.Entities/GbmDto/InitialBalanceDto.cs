using Newtonsoft.Json;
using System.Collections.Generic;

namespace GBMProject.Entities.GbmDto
{
    public class InitialBalanceDto
    {
        [JsonProperty(PropertyName = "cash")]
        public decimal Cash { get; set; }
        [JsonProperty(PropertyName = "issuers")]
        public List<IssuerDto> IssuerList { get; set; }
    }
}
