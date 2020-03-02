namespace GBMProject.Business.Contracts.Repository
{
    public interface ISerialize
    {
        OperationResult<string> Execute<T>(T objectDto);
    }
}
