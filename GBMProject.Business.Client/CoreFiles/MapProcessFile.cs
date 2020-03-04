using GBMProject.Business.Contracts;
using GBMProject.Business.Contracts.Repository;
using GBMProject.Business.Contracts.Values;
using GBMProject.Entities.Common;
using GBMProject.Entities.GbmDto;
using GBMProject.Entities.Request;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBMProject.Business.Client.CoreFiles
{
    public class MapProcessFile
    {
        private readonly IDirectoryPathConfig directoryPathConfig;
        private readonly IFileManager fileManager;
        private readonly IDeserialize deserialize;
        internal const string FAILURE_READ_FILE = "Ocurrio un error en lectura del archivo.";
        public MapProcessFile(IDirectoryPathConfig directoryPathConfig, IFileManager fileManager, IDeserialize deserialize)
        {
            this.directoryPathConfig = directoryPathConfig;
            this.fileManager = fileManager;
            this.deserialize = deserialize;
        }
        internal OperationResult<FileNameDto> ProcessFile(FileNameDto processingFile)
        {
            var linesResult = fileManager.ReadLines(Path.Combine(directoryPathConfig.BackUpFilesRequestPath, processingFile.FileInfoName));
            if (linesResult.Failure)
            {
                return new OperationResult<FileNameDto>(ErrorDto.BuildTechnical(FAILURE_READ_FILE));
            }
            var mapAndValidateProcess = MapAndValidateFileStructure(linesResult.Result);
            if (mapAndValidateProcess.Failure)
            {
                return new OperationResult<FileNameDto>(mapAndValidateProcess.ErrorList);
            }
            processingFile.SellOrdersRequest = mapAndValidateProcess.Result;
            return new OperationResult<FileNameDto>(processingFile);
        }
        private OperationResult<SellOrdersRequestDto> MapAndValidateFileStructure(IEnumerable<string> fileLines)
        {
            if(fileLines == null || fileLines.Count() != 2)
            {
                return new OperationResult<SellOrdersRequestDto>(ErrorDto.BuildUser("Structure file {0}, is incorrect"));
            }

            var lineInitialBalance = fileLines.ToArray()[0];
            var iineOrders = fileLines.ToArray()[1];

            var processInitiBalalance = deserialize.Execute<InitialBalanceDto>(lineInitialBalance);
            if (processInitiBalalance.Failure)
            {
                return new OperationResult<SellOrdersRequestDto>(processInitiBalalance.ErrorList);
            }

            var processOrderList = deserialize.Execute<IEnumerable<OrderDto>>(iineOrders);
            if (processOrderList.Failure)
            {
                return new OperationResult<SellOrdersRequestDto>(processOrderList.ErrorList);
            }

            var response = new SellOrdersRequestDto()
            {
                InitialBalance = processInitiBalalance.Result,
                OrderList = processOrderList.Result

            };
            return new OperationResult<SellOrdersRequestDto>(response);

        }
    }
}
