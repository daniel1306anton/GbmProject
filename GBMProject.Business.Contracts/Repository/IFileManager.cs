using System.Collections.Generic;

namespace GBMProject.Business.Contracts.Repository
{
    public interface IFileManager
    {
        OperationResult CreateFile(string destinationDirectory, IEnumerable<string> contents);
        OperationResult MoveFile(string sourceDirectory, string destinationDirectory);
        OperationResult<IEnumerable<string>> GetFiles(string directory);
        OperationResult<IEnumerable<string>> ReadLines(string filePath);
    }
}
