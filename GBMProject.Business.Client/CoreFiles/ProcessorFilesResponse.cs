using GBMProject.Business.Contracts.Repository;
using GBMProject.Business.Contracts.Values;
using GBMProject.Entities.Common;
using GBMProject.Entities.Response;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GBMProject.Business.Client.CoreFiles
{
    public class ProcessorFilesResponse 
    {
        private readonly IDirectoryPathConfig directoryPath;
        private readonly IFileManager fileManager;
        private readonly ISerialize serialize;
        public ProcessorFilesResponse(IDirectoryPathConfig directoryPath, IFileManager fileManager, ISerialize serialize)
        {
            this.directoryPath = directoryPath;
            this.fileManager = fileManager;
            this.serialize = serialize;
        }
        internal void GenerateResponseWithError(FileNameDto file, IEnumerable<ErrorDto> errorList)
        {
            var fileInformation = new List<string>();
            var serializeError = serialize.Execute(errorList);
            if (serializeError.Failure)
            {
                fileInformation.Add(serializeError.ErrorList.FirstOrDefault().Message);
            }
            fileInformation.Add(serializeError.Result);
            
            GenerateFile(fileInformation, file.BuildNameErrorResponse());
            MoveFileReqProcessed(file);
        }
        internal void GenerateResponse(FileNameDto file)
        {
            var fileInformation = new List<string>();
            var serializeResponse = serialize.Execute<SellOrderResponseDto>(file.SellOrderResponse);
            if (serializeResponse.Failure)
            {
                fileInformation.Add(serializeResponse.ErrorList.FirstOrDefault().Message);
            }
            fileInformation.Add(serializeResponse.Result);           

            fileManager.CreateFile(
                          Path.Combine(directoryPath.FileResponseInPath, serializeResponse.Failure ? file.BuildNameErrorResponse() : file.BuildNameResponse()),
                               fileInformation);

            MoveFileReqProcessed(file);
        }
        private void GenerateFile(IEnumerable<string> fileInformation, string fileName)
        {
            fileManager.CreateFile(
                          Path.Combine(directoryPath.FileResponseInPath, fileName),
                               fileInformation);
        }

        private void MoveFileReqProcessed(FileNameDto file)
        {
            var moveFileToBackUp = fileManager.MoveFile(Path.Combine(directoryPath.FileProcess, file.FileInfoName), Path.Combine(directoryPath.BackUpFilesRequestPath, file.FileInfoName));            
        }
        
    }
}
