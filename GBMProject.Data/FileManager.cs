using GBMProject.Business.Contracts;
using GBMProject.Business.Contracts.Repository;
using GBMProject.Entities.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBMProject.Data
{
    public class FileManager : IFileManager
    {
        
        public const string DATA_RESULT = "Error en ejecución de procesamiento de archivos.";


        public OperationResult CreateFile(string destinationDirectory, IEnumerable<string> contents)
        {
            contents = contents.Where(x => !string.IsNullOrEmpty(x) || !string.IsNullOrWhiteSpace(x));
            try
            {
                File.WriteAllLines(destinationDirectory, contents);

            }
            catch (Exception ex)
            {
                return new OperationResult(ErrorDto.BuildTechnical(ex.Message));
            }
            return new OperationResult(true);
        }


        public OperationResult MoveFile(FileInfo file, string destinationDirectory)
        {
            try
            {
                file.MoveTo(destinationDirectory);

            }
            catch (Exception ex)
            {
                return new OperationResult(false);
            }
            return new OperationResult(true);
        }
        public OperationResult MoveFile(string sourceDirectory, string destinationDirectory)
        {
            try
            {
                File.Move(sourceDirectory, destinationDirectory);
            }
            catch (Exception ex)
            {

                return new OperationResult(false);
            }
            return new OperationResult(true);
        }

        public OperationResult<IEnumerable<string>> ReadLines(FileSystemInfo file)
        {
            try
            {
                return new OperationResult<IEnumerable<string>>(File.ReadLines(file.FullName));

            }
            catch (Exception ex)
            {

                return new OperationResult<IEnumerable<string>>(default(IEnumerable<string>), false);
            }
        }


        public OperationResult<IEnumerable<string>> ReadLines(string filePath)
        {
            try
            {
                return new OperationResult<IEnumerable<string>>(File.ReadLines(filePath));

            }
            catch (Exception ex)
            {

                return new OperationResult<IEnumerable<string>>(default(IEnumerable<string>), false);
            }
        }
        public OperationResult<List<string>> GetFiles(string directory)
        {
            try
            {
                var files = new DirectoryInfo(directory).GetFiles().Select(x => x.Name).ToList();
                return new OperationResult<List<string>>(files, true);
            }
            catch (Exception ex)
            {                
                return new OperationResult<List<string>>(default(List<string>), false);
            }
        }
    }
}
