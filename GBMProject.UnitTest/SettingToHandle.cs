using GBMProject.Entities.Common;
using GBMProject.Entities.GbmDto;
using GBMProject.Entities.Response;
using System;
using System.Collections.Generic;

namespace GBMProject.UnitTest
{
    internal static class SettingToHandle
    {
        internal static IEnumerable<string> InitializeFileNames => new List<string>() { "GBM_Order" };

        internal static IEnumerable<string> InitializeReadLines => new List<string>() 
        { "{'initialBalances': {'cash': 1000, 'issuers': []}}",
          "{'timestamp': 1571350755, 'operation': 'BUY', 'IssuerName': 'GBM', 'TotalShares': 5, 'SharePrice': 100}"
        };

        internal static IEnumerable<string> InitializeIncorrectReadLines => new List<string>()
        { "{'initialBalances': {'cash': 1000, 'issuers': []}}"          
        };

        internal static string InitializeLineErrorList => "ErrorList:[]";
        internal static string InitializeSerializeResponse => "{'initialBalances': {'cash': 1000, 'issuers': []}, 'bussinessErrors': ['INVALID_OPERATION']} ";

        internal static InitialBalanceJsonDto InitializeInitialBalance => new InitialBalanceJsonDto() { 
            InitialBalance = new InitialBalanceDto()
            {
                Cash = 1000,
                IssuerList = new List<IssuerDto>() { new IssuerDto() { IssuerName = "GBM", TotalShares = 12, SharePrice = 12 } }
            }
        
        };

        internal static IEnumerable<OrderDto> InitializeOrderList => new List<OrderDto>() { 
            new OrderDto(){ 
                IssuerName = "GBM",
                Operation = "BUY",
                SharePrice = 12,
                TotalShares = 12,
                TimeStamp = new DateTime(2020,1,1,10,10,0)
            
            }
        
        };

        internal static SellOrderResponseDto InitializeSellOrderResponse => new SellOrderResponseDto() { 
            CurrentBalance = new CurrentBalanceDto() {
                Cash = 1000,
                IssuerList = new List<IssuerDto>() { new IssuerDto() { IssuerName = "GBM", TotalShares = 12, SharePrice = 12 } }

            },
            BusinessErrorList = new List<ErrorDto>()
        
        };

        internal static SellOrderResponseDto InitializeSellOrderWithErrorResponse => new SellOrderResponseDto()
        {
            CurrentBalance = new CurrentBalanceDto()
            {
                Cash = 1000,
                IssuerList = new List<IssuerDto>() { new IssuerDto() { IssuerName = "GBM", TotalShares = 12, SharePrice = 12 } }

            },
            BusinessErrorList = new List<ErrorDto>() { 
                ErrorDto.BuildUser("Hola")
            
            }

        };

    }
}
