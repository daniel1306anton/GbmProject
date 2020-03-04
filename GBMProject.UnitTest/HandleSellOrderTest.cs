using GBMProject.Business.Client;
using GBMProject.Business.Client.CoreFiles;
using GBMProject.Business.Contracts;
using GBMProject.Business.Contracts.Repository;
using GBMProject.Business.Contracts.Values;
using GBMProject.Entities.Common;
using GBMProject.Entities.GbmDto;
using GBMProject.Entities.Request;
using GBMProject.Entities.Response;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBMProject.UnitTest
{
    [TestClass]
    public class HandleSellOrderTest
    {
        private Mock<IExecutionSellOrder> executionSellOrder;

        private Mock<IDirectoryPathConfig> directoryPath;
        private Mock<IFileManager> fileManager;

        private Mock<ISerialize> serialize;
        private Mock<IDeserialize> deserialize;

        private ProcessorFilesRequest processorFilesRequest;
        private ProcessorFilesResponse processorFilesResponse;
        private MapProcessFile mapProcessFile;

        private HandleSellOrder handleSellOrder;

        [TestInitialize]
        public void TestInitialize()
        {
            executionSellOrder = new Mock<IExecutionSellOrder>();
            fileManager = new Mock<IFileManager>();
            directoryPath = SettingValueMoq.DirectoryPathMoq();
            serialize = new Mock<ISerialize>();
            deserialize = new Mock<IDeserialize>();

            processorFilesRequest = new ProcessorFilesRequest(directoryPath.Object, fileManager.Object);
            processorFilesResponse = new ProcessorFilesResponse(directoryPath.Object, fileManager.Object, serialize.Object);
            mapProcessFile = new MapProcessFile(directoryPath.Object, fileManager.Object, deserialize.Object);

            handleSellOrder = new HandleSellOrder(processorFilesRequest, processorFilesResponse, mapProcessFile, executionSellOrder.Object);
        }
        private bool successGetFiles = true;
        private bool emptyGetFiles = false;

        private bool successMoveFiles = true;

        private bool successReadLines = true;
        private bool emptyReadLines = false;

        private bool successLineErrorList = true;
        private bool successDeserializeInitialBalance = true;
        private bool successDeserializeOrderList = true;

        private bool successExecutionSellOrder = true;
        private bool successSerializeResponse = true;
        [TestMethod]
        public void Failure_Process_For_Get_Files()
        {
            successGetFiles = false;
            IntializeMoq();
            var response = handleSellOrder.Execute();
            RobotResponseAnalize.FailureThecnical(response);
        }

        [TestMethod]
        public void Sucess_Process_For_Empty_Files()
        {
            emptyGetFiles = true;
            IntializeMoq();
            var response = handleSellOrder.Execute();
            RobotResponseAnalize.Succesfull(response);
        }

        [TestMethod]
        public void Failure_Process_For_Move_Files()
        {
            successMoveFiles = false;
            IntializeMoq();
            var response = handleSellOrder.Execute();
            RobotResponseAnalize.FailureThecnical(response);
        }
        [TestMethod]
        public void Failure_Process_For_ReadLines()
        {
            successReadLines = false;
            IntializeMoq();
            var response = handleSellOrder.Execute();
            RobotResponseAnalize.FailureThecnical(response);
        }

        [TestMethod]
        public void Failure_Process_For_Empty_File_Lines()
        {
            emptyReadLines = true;
            IntializeMoq();
            var response = handleSellOrder.Execute();
            RobotResponseAnalize.FailureUser(response);
        }

        [TestMethod]
        public void Failure_Process_For_Incorrect_File_Lines()
        {            
            IntializeMoq();
            fileManager.Setup(x => x.ReadLines(It.IsAny<string>()))
                .Returns(
                BuildOperationResultForTest<string>.OperationGetList
                     (SettingToHandle.InitializeIncorrectReadLines, emptyReadLines, successReadLines));
            var response = handleSellOrder.Execute();
            RobotResponseAnalize.FailureUser(response);
        }

        [TestMethod]
        public void Failure_Process_For_Deserialize_Initial_Balance()
        {
            successDeserializeInitialBalance = false;
            IntializeMoq();            
            var response = handleSellOrder.Execute();
            RobotResponseAnalize.FailureThecnical(response);
        }

        [TestMethod]
        public void Failure_Process_For_Deserialize_Order_List()
        {
            successDeserializeOrderList = false;
            IntializeMoq();
            var response = handleSellOrder.Execute();
            RobotResponseAnalize.FailureThecnical(response);
        }

        [TestMethod]
        public void Sucess_Process_With_Execution_Business_Error()
        {
            successExecutionSellOrder = false;
            IntializeMoq();
            var response = handleSellOrder.Execute();
            RobotResponseAnalize.Succesfull(response);
        }
        [TestMethod]
        public void Sucess_Process()
        {            
            IntializeMoq();
            var response = handleSellOrder.Execute();
            RobotResponseAnalize.Succesfull(response);
        }
        private void IntializeMoq()
        {
            fileManager.Setup(x => x.GetFiles(It.IsAny<string>()))
                .Returns(
                BuildOperationResultForTest<string>.OperationGetList
                     (SettingToHandle.InitializeFileNames,emptyGetFiles, successGetFiles));

            fileManager.Setup(x => x.MoveFile(It.IsAny<string>(),It.IsAny<string>()))
                .Returns(
                successMoveFiles ? new OperationResult(true) : new OperationResult(ErrorDto.BuildTechnical("ERROR")));

            fileManager.Setup(x => x.ReadLines(It.IsAny<string>()))
                .Returns(
                BuildOperationResultForTest<string>.OperationGetList
                     (SettingToHandle.InitializeReadLines, emptyReadLines, successReadLines));

            serialize.Setup(x => x.Execute<IEnumerable<ErrorDto>>(It.IsAny<IEnumerable<ErrorDto>>()))
                .Returns(BuildOperationResultForTest<string>.OperationGetItem
                     (SettingToHandle.InitializeLineErrorList, false, successLineErrorList));

            fileManager.Setup(x => x.CreateFile(It.IsAny<string>(), It.IsAny<IEnumerable<string>>()))
                .Returns(
                new OperationResult(true));

            deserialize.Setup(x => x.Execute<InitialBalanceDto> (It.IsAny<string>()))
                .Returns(BuildOperationResultForTest<InitialBalanceDto>.OperationGetItemThecnical
                     (SettingToHandle.InitializeInitialBalance,  successDeserializeInitialBalance));

            deserialize.Setup(x => x.Execute<IEnumerable<OrderDto>>(It.IsAny<string>()))
                .Returns(BuildOperationResultForTest<OrderDto>.OperationGetList
                     (SettingToHandle.InitializeOrderList, false, successDeserializeOrderList));

            executionSellOrder.Setup(x => x.Execute(It.IsAny<SellOrdersRequestDto>()))
                .Returns(successExecutionSellOrder ? SettingToHandle.InitializeSellOrderResponse : SettingToHandle.InitializeSellOrderWithErrorResponse);

            serialize.Setup(x => x.Execute<SellOrderResponseDto>(It.IsAny<SellOrderResponseDto>()))
                .Returns(BuildOperationResultForTest<string>.OperationGetItem
                     (SettingToHandle.InitializeSerializeResponse, false, successSerializeResponse));

        }

    }
}
