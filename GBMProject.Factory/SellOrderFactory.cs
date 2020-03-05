using GBMProject.Business.Client;
using GBMProject.Business.Client.CoreFiles;
using GBMProject.Business.Contracts;
using GBMProject.Business.Contracts.Repository;
using GBMProject.Business.Contracts.Values;
using GBMProject.Data;
using GBMProject.Entities.Common;
using GBMProject.Entities.Response;
using GBMProject.Factory.SettingValue;
using GBMProject.Framework.Json;
using System.Collections.Generic;

namespace GBMProject.Factory
{
    public class SellOrderFactory
    {
        private readonly IDirectoryPathConfig directoryPath;
        private readonly IFileManager fileManager;
        private readonly ISerialize serialize;
        private readonly IDeserialize deserialize;

        private readonly ProcessSellOrder processSellOrder;
        private readonly ValidateSellOrderBusinessRules validateSellOrderBusinessRules;

        private readonly ProcessorFilesRequest processorFilesRequest;

        private readonly ProcessorFilesResponse processorFilesResponse;
        private readonly MapProcessFile mapProcessFile;
        private readonly IExecutionSellOrder executionSellOrder;

        private readonly HandleSellOrder handleSellOrder;
        public SellOrderFactory()
        {
            directoryPath = new DirectoryPathConfig();
            fileManager = new FileManager();
            processorFilesRequest = new ProcessorFilesRequest(directoryPath,fileManager);

            serialize = new JsonSerialize();
            deserialize = new JsonDeserialize();
            processorFilesResponse = new ProcessorFilesResponse(directoryPath,fileManager,serialize);

            mapProcessFile = new MapProcessFile(directoryPath, fileManager, deserialize);

            processSellOrder = new ProcessSellOrder();
            validateSellOrderBusinessRules = new ValidateSellOrderBusinessRules();

            executionSellOrder = new ExecutionSellOrder(processSellOrder,validateSellOrderBusinessRules);
            handleSellOrder = new HandleSellOrder(processorFilesRequest,processorFilesResponse,mapProcessFile,executionSellOrder);
        }

        public RobotResponseDto Execute() => new DirectoryPathConfig().IsValidDirectories() ? handleSellOrder.Execute() : new RobotResponseDto(new List<ErrorDto>() { ErrorDto.BuildUser("DirectoryNotValid.") });
    }
}
