using System;
using GBMProject.Business.Client;
using GBMProject.Entities.Response;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GBMProject.UnitTest
{
    [TestClass]
    public class ExecutionSellOrderTest
    {
        private ExecutionSellOrder executionSellOrder;
        private ProcessSellOrder processSellOrder;
        private ValidateSellOrderBusinessRules validateSellOrderBusinessRules;
        [TestInitialize]
        public void TestInitialize()
        {
            processSellOrder = new ProcessSellOrder();
            validateSellOrderBusinessRules = new ValidateSellOrderBusinessRules();
            executionSellOrder = new ExecutionSellOrder(processSellOrder,validateSellOrderBusinessRules);            
        }
        [TestMethod]
        public void Failure_Process_Execution_For_Request_Null()
        {
            var request = InitializeSellOrderRequest.RequestNull;
            var response = executionSellOrder.Execute(request);
            SellOrderResponseAnalize.FailureUser(response);
        }
        [TestMethod]
        public void Failure_Process_Execution_For_InitialBalance_And_OrderList_Null()
        {
            var request = InitializeSellOrderRequest.RequestInitialBalanceAndOrderNull;
            var response = executionSellOrder.Execute(request);
            SellOrderResponseAnalize.FailureUser(response);
        }
        [TestMethod]
        public void Failure_Process_Execution_For_Invalid_Operation()
        {
            var request = InitializeSellOrderRequest.RequestInvalidOperation;
            var response = executionSellOrder.Execute(request);
            SellOrderResponseAnalize.FailureUser(response);
        }
        [TestMethod]
        public void Failure_Process_Execution_For_Closed_Market()
        {
            var request = InitializeSellOrderRequest.RequestClosedMarket;
            var response = executionSellOrder.Execute(request);
            SellOrderResponseAnalize.FailureUser(response);
        }
        [TestMethod]
        public void Failure_Process_Execution_For_Insufficient_Stocks_By_Dont_Exit_InitialBalance()
        {
            var request = InitializeSellOrderRequest.RequestIssuerNotExitInBalance;
            var response = executionSellOrder.Execute(request);
            SellOrderResponseAnalize.FailureUser(response);
        }
        [TestMethod]
        public void Failure_Process_Execution_For_DuplicatedOperation()
        {
            var request = InitializeSellOrderRequest.RequestDuplicatedOperation;
            var response = executionSellOrder.Execute(request);
            SellOrderResponseAnalize.FailureUser(response);
        }
        [TestMethod]
        public void Failure_Process_Execution_Sell_For_Insufficent_Stock()
        {
            var request = InitializeSellOrderRequest.RequestSellInsufficentStockOperation;
            var response = executionSellOrder.Execute(request);
            SellOrderResponseAnalize.FailureUser(response);
        }
        [TestMethod]
        public void Failure_Process_Execution_Buy_For_Insufficent_Balance()
        {
            var request = InitializeSellOrderRequest.RequestBuyInsufficentBalanceOperation;
            var response = executionSellOrder.Execute(request);
            SellOrderResponseAnalize.FailureUser(response);
        }
        [TestMethod]
        public void Sucess_Process_Execution()
        {
            var request = InitializeSellOrderRequest.RequestSucess;
            var response = executionSellOrder.Execute(request);
            SellOrderResponseAnalize.Succesfull(response);
        }

    }
}
