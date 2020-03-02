using GBMProject.Entities.Common;
using GBMProject.Entities.GbmDto;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace GBMProject.Entities.Response
{
    public class SellOrderResponseDto
    {
        [JsonProperty(PropertyName = "currentBalance")]
        public CurrentBalanceDto CurrentBalance { get; set; }
        public bool Success { get; set; }        
        [JsonProperty(PropertyName = "bussinessErrors")]
        public IEnumerable<ErrorDto> BusinessErrorList { get; set; }
        public static SellOrderResponseDto Build(IEnumerable<ErrorDto> errorList, CurrentBalanceDto currentBalance)
        {
            return new SellOrderResponseDto()
            {
                CurrentBalance = currentBalance,
                Success = errorList == null || !errorList.Any() || !errorList.Any(x => x.Type == 1),
                BusinessErrorList = errorList ?? new List<ErrorDto>()
            };
        }
    }
}
