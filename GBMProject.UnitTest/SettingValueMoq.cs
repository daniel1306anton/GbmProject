using GBMProject.Business.Contracts.Values;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBMProject.UnitTest
{
    internal static class SettingValueMoq
    {
        internal static Mock<IDirectoryPathConfig> DirectoryPathMoq()
        {
            var response = new Mock<IDirectoryPathConfig>();
            response.Setup(x => x.BackUpFilesRequestPath).Returns("BackUpFilesRequestPath");
            response.Setup(x => x.FileProcess).Returns("FileProcess");
            response.Setup(x => x.FileResponseInPath).Returns("FileResponseInPath");
            response.Setup(x => x.FilesRequestInPath).Returns("FilesRequestInPath");
            return response;
        }
    }
}
