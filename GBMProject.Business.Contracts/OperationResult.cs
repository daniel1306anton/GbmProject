using GBMProject.Entities.Common;
using System.Collections.Generic;

namespace GBMProject.Business.Contracts
{
    public class OperationResult
    {
        public bool Failure { get; }
        public bool Sucess { get; }
        public IEnumerable<ErrorDto> ErrorList { get; set; }
        public OperationResult(bool isSuccess)
        {
            Sucess = isSuccess;
            Failure = !isSuccess;
        }
        public OperationResult(ErrorDto error)
        {
            Sucess = false;
            Failure = true;
            ErrorList = new List<ErrorDto>() { error };
        }
        public OperationResult(IEnumerable<ErrorDto> errorList)
        {
            Sucess = false;
            Failure = true;
            ErrorList = errorList;
        }
        public OperationResult(IEnumerable<ErrorDto> errorList,bool isSuccess)
        {
            Sucess = isSuccess;
            Failure = !isSuccess;
            ErrorList = errorList;
        }
        public OperationResult()
        {
        }
    }

    public class OperationResult<T> : OperationResult
    {
        public OperationResult(T result) : base(result != null)
        {
            Result = result;
        }

        public OperationResult(T result, bool isSuccess) : base(isSuccess)
        {
            Result = result;
        }

        
        public OperationResult(ErrorDto error)
           : base(error)
        {
        }
        
        public OperationResult(T result, IEnumerable<ErrorDto> errorList,bool isSucess)
            : base(errorList,isSucess)
        {
            Result = result;
        }

        public OperationResult(IEnumerable<ErrorDto> errorList)
           : base(errorList)
        {
        }        

        public T Result { get; }
    }       

}
