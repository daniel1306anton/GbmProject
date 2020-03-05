using GBMProject.Entities.GbmDto;
using System.Collections.Generic;

namespace GBMProject.Entities.Request
{
    public class SellOrdersRequestDto
    {
        public InitialBalanceDto InitialBalance { get; set; }
        public IEnumerable<OrderDto> OrderList { get; set; }
    }
}
