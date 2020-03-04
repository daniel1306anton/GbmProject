using GBMProject.Business.Contracts;
using GBMProject.Entities.Common;
using GBMProject.Entities.GbmDto;
using GBMProject.Entities.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBMProject.Business.Client
{
    public class ValidateSellOrderBusinessRules
    {
        private readonly IEnumerable<string> validOperation = new List<string>() { "BUY", "SELL"};
        private readonly string SELL_OPERATION = "SELL";
        private readonly TimeSpan InitialHourDay = TimeSpan.Parse("06:00");
        private readonly TimeSpan FinalHourDay = TimeSpan.Parse("15:00");

        
        private readonly string DUPLICATED_OPERATION_CODE = "DUPLICATED_OPERATION";
        private readonly string CLOSED_MARKET_CODE = "CLOSED_MARKET";
        private readonly string INSUFFICIENT_STOCKS_CODE = "INSUFFICIENT_STOCKS";
        private readonly string INVALID_OPERATION_CODE = "INVALID_OPERATION_CODE";
        internal OperationResult<IEnumerable<OrderDto>> Execute(SellOrdersRequestDto request)
        {
            var goodOrder = new List<OrderDto>();
            var messageErrorList = new List<ErrorDto>();

            goodOrder.AddRange(request.OrderList.Where(
                x => {
                    var correct = true;
                    if(!validOperation.Any(y => y == x.Operation)) 
                    {
                        correct = false;
                        messageErrorList.Add(ErrorDto.BuildUser(
                            string.Format("Issuer {0} with operation {1} is invalid.",x.IssuerName,x.Operation),
                            INVALID_OPERATION_CODE));
                        return correct;
                    }
                    if(!(x.TimeStamp >= InitialHourDay && x.TimeStamp <= FinalHourDay))
                    {
                        correct = false;
                        messageErrorList.Add(ErrorDto.BuildUser(
                            string.Format("Issuer {0} with time {1} is invalid (closed market).", x.IssuerName, x.TimeStamp),
                            CLOSED_MARKET_CODE));
                        return correct;
                    }                    
                    return correct;            
            }).Select(x => x).ToList());
           
            var goodOrderGroup = goodOrder.GroupBy(x => x.IssuerName);
            goodOrder = new List<OrderDto>();

            foreach (var item in goodOrderGroup)
            {
                if(item.Any(x => x.Operation == SELL_OPERATION) && !request.InitialBalance.IssuerList.Any(x => x.IssuerName == item.Key))
                {
                    
                    messageErrorList.Add(ErrorDto.BuildUser(
                        string.Format("Issuer {0} can't be operation {1} because doesn't exist in initial balance.", item.Key,SELL_OPERATION),
                        INSUFFICIENT_STOCKS_CODE));
                    continue;
                }

                if (item.Count() == 1) {
                    goodOrder.AddRange(item.Select(x => x));
                    continue;
                }
                var indexOrder = item.Select((x, y) => new { order = x, indx = y });
                goodOrder.AddRange(indexOrder.Where(x =>
                {
                    var correct = true;
                    if (indexOrder.Any(z => z.indx != x.indx && z.order.Operation == x.order.Operation && z.order.StayFiveMinutes(x.order.TimeStamp))) {
                        correct = false;
                        messageErrorList.Add(ErrorDto.BuildUser(
                            string.Format("Issuer {0} with timestamp {1} is duplicated with another records.", x.order.IssuerName, x.order.TimeStamp),
                            DUPLICATED_OPERATION_CODE));

                    }
                    
                    
                    return correct;

                }).Select(x => x.order).ToList());
            }
            return new OperationResult<IEnumerable<OrderDto>>(goodOrder, messageErrorList, true);
        }
    }
}
