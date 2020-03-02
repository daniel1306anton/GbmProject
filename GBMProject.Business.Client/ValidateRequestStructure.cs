using GBMProject.Business.Contracts;
using GBMProject.Entities.Common;
using GBMProject.Entities.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBMProject.Business.Client
{
    internal static class ValidateRequestStructure
    {        
        internal static IEnumerable<ErrorDto> Execute(SellOrdersRequestDto request)
        {
            var messageErrorList = new List<ErrorDto>();
            if (request == null) {
                messageErrorList.Add(ErrorDto.BuildUser("Request can't be null."));
                return messageErrorList;
            };
            if(request.InitialBalance == null)
            {
                messageErrorList.Add(ErrorDto.BuildUser("InitialBalance can't be null."));                
            }
            if (request.OrderList == null || !request.OrderList.Any())
            {
                messageErrorList.Add(ErrorDto.BuildUser("Your need at least one order for the process."));
            }
            return messageErrorList;
        }

    }
}
