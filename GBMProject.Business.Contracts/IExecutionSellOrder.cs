using GBMProject.Entities.Request;
using GBMProject.Entities.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBMProject.Business.Contracts
{
    public interface IExecutionSellOrder
    {
        SellOrderResponseDto Execute(SellOrdersRequestDto request);
    }
}
