using GBMProject.Business.Contracts.Values;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBMProject.Factory.SettingValue
{
    public class DirectoryPathConfig : IDirectoryPathConfig
    {
        public string FilesRequestInPath { get { return GetDirectory("FilesRequestIn"); } }
        public string BackUpFilesRequestPath { get { return GetDirectory("BackUpFilesRequestPath"); } }
        public string FileResponseInPath { get { return GetDirectory("FilesResponseIn"); } }
        public string FileProcess { get { return GetDirectory("FilesResponseIn"); } }

        private string GetDirectory(string parameter)
        {
            var directory = new DirectoryInfo(ConfigurationManager.AppSettings[parameter]);
            return directory.Exists ? directory.FullName : string.Empty;

        }
        private string GetValue(string parameter)
        {
            return ConfigurationManager.AppSettings[parameter].ToString();
        }
        public bool IsValidDirectories()
        {
            return !string.IsNullOrWhiteSpace(FilesRequestInPath) && !string.IsNullOrWhiteSpace(BackUpFilesRequestPath)
                        && !string.IsNullOrWhiteSpace(FilesRequestInPath) && !string.IsNullOrWhiteSpace(FileProcess);
        }
    }

}
