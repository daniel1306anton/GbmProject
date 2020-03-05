using GBMProject.Business.Client.CoreFiles;
using GBMProject.Business.Contracts;
using GBMProject.Entities.Common;
using GBMProject.Entities.Response;
using System.Collections.Generic;
using System.Linq;

namespace GBMProject.Business.Client
{
    public class HandleSellOrder
    {
        private readonly ProcessorFilesRequest processorFilesRequest;
        private readonly ProcessorFilesResponse processorFilesResponse;
        private readonly MapProcessFile mapProcessFile;
        private readonly IExecutionSellOrder executionSellOrder;
        public HandleSellOrder(ProcessorFilesRequest processorFilesRequest, ProcessorFilesResponse processorFilesResponse, MapProcessFile mapProcessFile, IExecutionSellOrder executionSellOrder)
        {
            this.processorFilesRequest = processorFilesRequest;
            this.processorFilesResponse = processorFilesResponse;
            this.mapProcessFile = mapProcessFile;
            this.executionSellOrder = executionSellOrder;
        }
        public RobotResponseDto Execute()
        {
            var validateFileOperation = processorFilesRequest.Execute();
            if (validateFileOperation.Failure)
            {
                return new RobotResponseDto(validateFileOperation.ErrorList.ToList());
            }

            return ProcessFilesNames(validateFileOperation.Result);
        }
        private RobotResponseDto ProcessFilesNames(List<FileNameDto> fileNameList)
        {
            var response = new RobotResponseDto();

            foreach (var fileName in fileNameList)
            {

                var fileOperation = mapProcessFile.ProcessFile(fileName);
                if (fileOperation.Failure)
                {
                    processorFilesResponse.GenerateResponseWithError(fileName, fileOperation.ErrorList?.ToList());
                    response.AddErrorList(fileOperation.ErrorList?.ToList());                    
                    continue;
                }
                var fileNameRequest = fileOperation.Result;
                fileNameRequest.SellOrderResponse = executionSellOrder.Execute(fileNameRequest.SellOrdersRequest);
                processorFilesResponse.GenerateResponse(fileName);
            }
          
            return response;
        }
    }
}
