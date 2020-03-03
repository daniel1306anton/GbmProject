using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBMProject.Business.Contracts.Values
{
    public interface IDirectoryPathConfig
    {
        string FilesRequestInPath { get; }
        string BackUpFilesRequestPath { get; }
        string FileResponseInPath { get; }        
        string FileProcess { get; }
    }
}
