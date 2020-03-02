using GBMProject.Entities.Common;
using GBMProject.Entities.Request;
using GBMProject.Entities.Response;
using System.Collections.Generic;
using System.Linq;

namespace GBMProject.Business.Client
{
    internal class HandleSellOrderDto
    {
        private readonly ProcessSellOrder processSellOrder;
        private readonly ValidateSellOrderBusinessRules validateSellOrderBusinessRules;
        private readonly List<ErrorDto> messageErrorList;
        internal HandleSellOrderDto(ProcessSellOrder processSellOrder, ValidateSellOrderBusinessRules validateSellOrderBusinessRules)
        {
            this.processSellOrder = processSellOrder;
            this.validateSellOrderBusinessRules = validateSellOrderBusinessRules;
            messageErrorList = new List<ErrorDto>();
        }
        internal SellOrderResponseDto Execute(SellOrdersRequestDto request)
        {
            var validateRequest = ValidateRequestStructure.Execute(request);
            if (validateRequest.Any()) return SellOrderResponseDto.Build(validateRequest, null);

            var validateBusinessRules = validateSellOrderBusinessRules.Execute(request);
            if (validateBusinessRules.ErrorList.Any()) messageErrorList.AddRange(validateBusinessRules.ErrorList);

            request.OrderList = validateBusinessRules.Result;

            var sellOrderProcess = processSellOrder.Execute(request);
            if (sellOrderProcess.ErrorList.Any()) messageErrorList.AddRange(sellOrderProcess.ErrorList);

            return SellOrderResponseDto.Build(messageErrorList, sellOrderProcess.Result);
        }
    }
}
