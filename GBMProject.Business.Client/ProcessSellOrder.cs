using GBMProject.Business.Contracts;
using GBMProject.Entities.Common;
using GBMProject.Entities.GbmDto;
using GBMProject.Entities.Request;
using System.Collections.Generic;
using System.Linq;

namespace GBMProject.Business.Client
{
    public class ProcessSellOrder
    {
        private readonly string INSUFFICIENT_BALANCE_CODE = "INSUFFICIENT_BALANCE";
        private readonly string INSUFFICIENT_STOCKS_CODE = "INSUFFICIENT_STOCKS";
        private List<ErrorDto> messageErrorList;
        private CurrentBalanceDto currentBalance;
        internal OperationResult<CurrentBalanceDto> Execute(SellOrdersRequestDto request)
        {
            currentBalance = CurrentBalanceDto.ConvertFromInitialBalance(request.InitialBalance);
            if (!request.OrderList.Any()) return new OperationResult<CurrentBalanceDto>(currentBalance);
            messageErrorList = new List<ErrorDto>();
            var orderList = request.OrderList.OrderBy(x => x.TimeStamp).ToList();
            orderList.ForEach(x =>
            {
                switch (x.Operation)
                {
                    case "SELL":
                        SellOperation(x);
                        break;
                    case "BUY":
                        BuyOperation(x);
                        break;
                }
            });
            return new OperationResult<CurrentBalanceDto>(currentBalance, messageErrorList, true);
        }
        private void SellOperation(OrderDto order)
        {
            if (currentBalance.IssuerList.Any(x => x.IssuerName == order.IssuerName && x.SharePrice == order.SharePrice))
            {
                currentBalance.IssuerList = currentBalance.IssuerList.Select(x => {
                    if (x.IssuerName == order.IssuerName && x.SharePrice == order.SharePrice) {
                        if (order.TotalShares > x.TotalShares)
                        {
                            messageErrorList.Add(ErrorDto.BuildUser(
                            string.Format("Issuer {0} can't be operation {1} beacuse doesn't stick in stock balance.", order.IssuerName, order.Operation),
                            INSUFFICIENT_STOCKS_CODE));
                        }
                        else
                        {
                            var cashOperation = order.SharePrice * order.TotalShares;
                            currentBalance.Cash = currentBalance.Cash + cashOperation;
                            x.TotalShares = x.TotalShares - order.TotalShares;
                        }

                    }
                    
                    return x;

                }).ToList();
            }
            else
            {
                messageErrorList.Add(ErrorDto.BuildUser(
                        string.Format("Issuer {0} can't be operation {1} beacuse doesn't stick in stock balance.", order.IssuerName, order.Operation),
                        INSUFFICIENT_STOCKS_CODE));                
            }
        }
        private void BuyOperation(OrderDto order)
        {
            var cashOperation = order.SharePrice * order.TotalShares;
            if(cashOperation > currentBalance.Cash)
            {
                messageErrorList.Add(ErrorDto.BuildUser(
                        string.Format("Issuer {0} can't be operation {1} because need more cash in initial balance.", order.IssuerName, order.Operation),
                        INSUFFICIENT_BALANCE_CODE ));
                return;
            }

            currentBalance.Cash = currentBalance.Cash - cashOperation;
            if(currentBalance.IssuerList.Any(x => x.IssuerName == order.IssuerName && x.SharePrice == order.SharePrice))
            {
                currentBalance.IssuerList = currentBalance.IssuerList.Select(x => {
                    if(x.IssuerName == order.IssuerName && x.SharePrice == order.SharePrice)
                    {
                        x.TotalShares = x.TotalShares + order.TotalShares;
                    }
                    return x;
                
                }).ToList();
            }
            else
            {
                currentBalance.IssuerList.Add(new IssuerDto(order));
            }

        }
    }
}
