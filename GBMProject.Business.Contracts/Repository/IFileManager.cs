using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBMProject.Business.Contracts.Repository
{
    public interface IFileManager
    {
        OperationResult CreateFile(string destinationDirectory, IEnumerable<string> contents);
        
        OperationResult MoveFile(FileInfo file, string destinationDirectory);
        OperationResult MoveFile(string sourceDirectory, string destinationDirectory);
        
        OperationResult<IEnumerable<string>> ReadLines(FileSystemInfo file);
        OperationResult<IEnumerable<string>> GetFiles(string directory);
        OperationResult<IEnumerable<string>> ReadLines(string filePath);
    }
}
