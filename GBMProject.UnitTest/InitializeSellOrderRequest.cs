using GBMProject.Entities.GbmDto;
using GBMProject.Entities.Request;
using System;
using System.Collections.Generic;

namespace GBMProject.UnitTest
{
    internal static class InitializeSellOrderRequest
    {
        internal static SellOrdersRequestDto RequestNull => null;
        internal static SellOrdersRequestDto RequestInitialBalanceAndOrderNull => new SellOrdersRequestDto() { InitialBalance = null, OrderList = null};
        internal static SellOrdersRequestDto RequestInvalidOperation => new SellOrdersRequestDto()
        {
            InitialBalance = new Entities.GbmDto.InitialBalanceDto() {
                Cash = 1000,
                IssuerList = new List<Entities.GbmDto.IssuerDto>() {
                    new Entities.GbmDto.IssuerDto()
                    {
                        IssuerName = "GBM",
                        SharePrice = 22,
                        TotalShares = 12
                    }
                }
            },
            OrderList = new List<OrderDto>() {
                new OrderDto(){
                    IssuerName = "GBM",
                    Operation="TOT",
                    SharePrice = 22,
                    TimeStamp = new DateTime(2020,1,1,0,12,0),
                    TotalShares = 22
                }

            } };
        internal static SellOrdersRequestDto RequestClosedMarket => new SellOrdersRequestDto()
        {
            InitialBalance = new Entities.GbmDto.InitialBalanceDto()
            {
                Cash = 1000,
                IssuerList = new List<Entities.GbmDto.IssuerDto>() {
                    new Entities.GbmDto.IssuerDto()
                    {
                        IssuerName = "GBM",
                        SharePrice = 22,
                        TotalShares = 12
                    }
                }
            },
            OrderList = new List<OrderDto>() {
                new OrderDto(){
                    IssuerName = "GBM",
                    Operation="BUY",
                    SharePrice = 22,
                    TimeStamp = new DateTime(2020,1,1,21,12,0),
                    TotalShares = 22
                }

            }
        };
        internal static SellOrdersRequestDto RequestIssuerNotExitInBalance => new SellOrdersRequestDto()
        {
            InitialBalance = new Entities.GbmDto.InitialBalanceDto()
            {
                Cash = 1000,
                IssuerList = new List<Entities.GbmDto.IssuerDto>() {
                    new Entities.GbmDto.IssuerDto()
                    {
                        IssuerName = "GBM_2",
                        SharePrice = 22,
                        TotalShares = 12
                    }
                }
            },
            OrderList = new List<OrderDto>() {
                new OrderDto(){
                    IssuerName = "GBM",
                    Operation="SELL",
                    SharePrice = 22,
                    TimeStamp =new DateTime(2020,1,1,14,12,0),
                    TotalShares = 22
                }

            }
        };
        internal static SellOrdersRequestDto RequestDuplicatedOperation => new SellOrdersRequestDto()
        {
            InitialBalance = new Entities.GbmDto.InitialBalanceDto()
            {
                Cash = 1000,
                IssuerList = new List<Entities.GbmDto.IssuerDto>() {
                    new Entities.GbmDto.IssuerDto()
                    {
                        IssuerName = "GBM",
                        SharePrice = 22,
                        TotalShares = 12
                    }
                }
            },
            OrderList = new List<OrderDto>() {
                new OrderDto(){
                    IssuerName = "GBM",
                    Operation="SELL",
                    SharePrice = 22,
                    TimeStamp = new DateTime(2020,1,1,14,12,0),
                    TotalShares = 22
                },
                new OrderDto(){
                    IssuerName = "GBM",
                    Operation="SELL",
                    SharePrice = 22,
                    TimeStamp = new DateTime(2020,1,1,14,12,0),
                    TotalShares = 22
                },
                new OrderDto(){
                    IssuerName = "GBM",
                    Operation="BUY",
                    SharePrice = 22,
                    TimeStamp = new DateTime(2020,1,1,14,12,0),
                    TotalShares = 22
                },
                new OrderDto(){
                    IssuerName = "GBM",
                    Operation="BUY",
                    SharePrice = 22,
                    TimeStamp = new DateTime(2020,1,1,14,16,0),
                    TotalShares = 22
                },
                new OrderDto(){
                    IssuerName = "GBM",
                    Operation="BUY",
                    SharePrice = 22,
                    TimeStamp = new DateTime(2020,1,1,14,8,0),
                    TotalShares = 22
                }

            }
        };
        internal static SellOrdersRequestDto RequestSellInsufficentStockOperation => new SellOrdersRequestDto()
        {
            InitialBalance = new Entities.GbmDto.InitialBalanceDto()
            {
                Cash = 1000,
                IssuerList = new List<Entities.GbmDto.IssuerDto>() {
                    new Entities.GbmDto.IssuerDto()
                    {
                        IssuerName = "GBM",
                        SharePrice = 12,
                        TotalShares = 12
                    }
                }
            },
            OrderList = new List<OrderDto>() {
                new OrderDto(){
                    IssuerName = "GBM",
                    Operation="SELL",
                    SharePrice = 12,
                    TimeStamp = new DateTime(2020,1,1,14,12,0),
                    TotalShares = 100
                }

            }
        };
        internal static SellOrdersRequestDto RequestBuyInsufficentBalanceOperation => new SellOrdersRequestDto()
        {
            InitialBalance = new Entities.GbmDto.InitialBalanceDto()
            {
                Cash = 1000,
                IssuerList = new List<Entities.GbmDto.IssuerDto>() {
                    new Entities.GbmDto.IssuerDto()
                    {
                        IssuerName = "GBM",
                        SharePrice = 12,
                        TotalShares = 12
                    }
                }
            },
            OrderList = new List<OrderDto>() {
                new OrderDto(){
                    IssuerName = "GBM",
                    Operation="BUY",
                    SharePrice = 12,
                    TimeStamp = new DateTime(2020,1,1,14,12,0),
                    TotalShares = 1000
                }

            }
        };
        internal static SellOrdersRequestDto RequestSucess => new SellOrdersRequestDto()
        {
            InitialBalance = new Entities.GbmDto.InitialBalanceDto()
            {
                Cash = 10000,
                IssuerList = new List<Entities.GbmDto.IssuerDto>() {
                    new Entities.GbmDto.IssuerDto()
                    {
                        IssuerName = "GBM",
                        SharePrice = 12,
                        TotalShares = 120
                    },
                    new Entities.GbmDto.IssuerDto()
                    {
                        IssuerName = "GBM_2",
                        SharePrice = 12,
                        TotalShares = 120
                    }
                }
            },
            OrderList = new List<OrderDto>() {
                new OrderDto(){
                    IssuerName = "GBM",
                    Operation="BUY",
                    SharePrice = 12,
                    TimeStamp = new DateTime(2020,1,1,14,12,0),
                    TotalShares = 100
                },
                new OrderDto(){
                    IssuerName = "GBM",
                    Operation="SELL",
                    SharePrice = 12,
                    TimeStamp = new DateTime(2020,1,1,14,12,0),
                    TotalShares = 20
                },
                new OrderDto(){
                    IssuerName = "GBM",
                    Operation="SELL",
                    SharePrice = 12,
                    TimeStamp = new DateTime(2020,1,1,14,18,0),
                    TotalShares = 20
                },
                new OrderDto(){
                    IssuerName = "GBM_2",
                    Operation="SELL",
                    SharePrice = 12,
                    TimeStamp = new DateTime(2020,1,1,14,12,0),
                    TotalShares = 20
                }
            }
        };
    }
}
