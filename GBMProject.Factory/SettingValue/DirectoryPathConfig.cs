using GBMProject.Business.Contracts.Values;
using System.Configuration;
using System.IO;

namespace GBMProject.Factory.SettingValue
{
    public class DirectoryPathConfig : IDirectoryPathConfig
    {
        public string FilesRequestInPath { get { return GetDirectory("FilesRequestIn"); } }
        public string BackUpFilesRequestPath { get { return GetDirectory("BackUpFilesRequestPath"); } }
        public string FileResponseInPath { get { return GetDirectory("FilesResponseIn"); } }
        public string FileProcess { get { return GetDirectory("FileProcess"); } }

        private string GetDirectory(string parameter)
        {
            var directory = new DirectoryInfo(ConfigurationManager.AppSettings[parameter]);
            return directory.Exists ? directory.FullName : string.Empty;

        }
        public bool IsValidDirectories()
        {
            return !string.IsNullOrWhiteSpace(FilesRequestInPath) && !string.IsNullOrWhiteSpace(BackUpFilesRequestPath)
                        && !string.IsNullOrWhiteSpace(FilesRequestInPath) && !string.IsNullOrWhiteSpace(FileProcess);
        }
    }

}
