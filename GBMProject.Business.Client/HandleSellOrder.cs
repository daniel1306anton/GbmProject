using GBMProject.Business.Client.CoreFiles;
using GBMProject.Entities.Common;
using GBMProject.Entities.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBMProject.Business.Client
{
    public class HandleSellOrder
    {
        private readonly ProcessorFilesRequest processorFilesRequest;
        private readonly MapProcessFile mapProcessFile;
        private readonly ExecutionSellOrder executionSellOrder;
        public HandleSellOrder(ProcessorFilesRequest processorFilesRequest, MapProcessFile mapProcessFile, ExecutionSellOrder executionSellOrder)
        {
            this.processorFilesRequest = processorFilesRequest;
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
                    response.AddErrorList(fileOperation.ErrorList?.ToList());
                    continue;
                }
                var fileNameRequest = fileOperation.Result;
                fileNameRequest.SellOrderResponse = executionSellOrder.Execute(fileNameRequest.SellOrdersRequest);
            }
          
            return response;
        }
    }
}
