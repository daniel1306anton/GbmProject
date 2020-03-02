using GBMProject.Entities.GbmDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBMProject.Entities.Request
{
    public class SellOrdersRequestDto
    {
        public InitialBalanceDto InitialBalance { get; set; }
        public IEnumerable<OrderDto> OrderList { get; set; }
    }
}
