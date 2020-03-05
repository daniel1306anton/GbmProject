using GBMProject.Entities.Request;
using GBMProject.Entities.Response;

namespace GBMProject.Business.Contracts
{
    public interface IExecutionSellOrder
    {
        SellOrderResponseDto Execute(SellOrdersRequestDto request);
    }
}
