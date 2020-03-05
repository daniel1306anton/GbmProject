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
