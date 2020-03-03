using GBMProject.Business.Contracts;
using GBMProject.Business.Contracts.Repository;
using GBMProject.Business.Contracts.Values;
using GBMProject.Entities.Common;
using System.Collections.Generic;
using System.IO;

namespace GBMProject.Business.Client.CoreFiles
{
    public class ProcessorFilesRequest
    {
        private readonly IDirectoryPathConfig directoryPath;
        private readonly IFileManager fileManager;
        private const string FAILURE_READ_NAME_FILES = "Ocurrio un error en lectura de archivos, Para mayor información consulte el visor de eventos.";
        public ProcessorFilesRequest(IDirectoryPathConfig directoryPath, IFileManager fileManager)
        {
            this.directoryPath = directoryPath;
            this.fileManager = fileManager;
        }
        internal OperationResult<List<FileNameDto>> Execute()
        {
            var filesOperation = fileManager.GetFiles(directoryPath.FilesRequestInPath);
            if (filesOperation.Failure)
            {
                return new OperationResult<List<FileNameDto>>(ErrorDto.BuildTechnical(FAILURE_READ_NAME_FILES));
            }

            var fileNameList = new List<FileNameDto>();
            foreach (var item in filesOperation.Result)
            {
                var processMove = MoveFileProcessing(item);
                if (processMove.Failure)
                {
                    return new OperationResult<List<FileNameDto>>(processMove.ErrorList);
                }
                fileNameList.Add(processMove.Result);
            }

            return new OperationResult<List<FileNameDto>>(fileNameList);
        }
        private OperationResult<FileNameDto> MoveFileProcessing(string fileName)
        {
            var moveFileToBackUp = fileManager.MoveFile(Path.Combine(directoryPath.FilesRequestInPath, fileName), Path.Combine(directoryPath.FileProcess, fileName));
            if (moveFileToBackUp.Failure)
            {                
                return new OperationResult<FileNameDto>(moveFileToBackUp.ErrorList);
            }
            return new OperationResult<FileNameDto>(new FileNameDto(fileName));
        }
    }
}
