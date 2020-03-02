using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
