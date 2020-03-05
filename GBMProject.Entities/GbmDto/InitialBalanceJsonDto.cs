using Newtonsoft.Json;

namespace GBMProject.Entities.GbmDto
{
    public class InitialBalanceJsonDto
    {
        [JsonProperty(PropertyName = "initialBalances")]
        public InitialBalanceDto InitialBalance { get; set; }
    }
}
