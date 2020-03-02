namespace GBMProject.Business.Contracts.Repository
{
    public interface IDeserialize
    {
        OperationResult<T> Execute<T>(string value);        
    }
}
