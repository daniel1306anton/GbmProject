using GBMProject.Business.Contracts;
using GBMProject.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBMProject.UnitTest
{
    internal static class BuildOperationResultForTest<T>
    {
        internal static OperationResult<IEnumerable<T>> OperationGetList(IEnumerable<T> result, bool emptyGetList, bool succesfullGetList)
        {
            return succesfullGetList ? new OperationResult<IEnumerable<T>>(emptyGetList ? new List<T>() : result, succesfullGetList) : new OperationResult<IEnumerable<T>>(ErrorDto.BuildTechnical("Hola"));            
        }
        internal static OperationResult<T> OperationGetItem(T result, bool nullGetItem, bool succesfullGetItem)
        {
            return new OperationResult<T>(nullGetItem ? default(T) : result, succesfullGetItem);
        }
        internal static OperationResult<T> OperationGetItemThecnical(T result, bool succesfullGetItem)
        {
            return succesfullGetItem ? new OperationResult<T>(result) : new OperationResult<T>(ErrorDto.BuildTechnical("Hola"));
        }
        

    }
    internal static class BuildOperationResultForTest
    {
        internal static OperationResult<ulong> OperationCreate(bool succesfullGetList)
        {
            return new OperationResult<ulong>(succesfullGetList ? (ulong)5 : ulong.MinValue, succesfullGetList);
        }
        internal static OperationResult<ulong> OperationCreateError(bool succesfullGetList)
        {
            return succesfullGetList ? new OperationResult<ulong>((ulong)5) : new OperationResult<ulong>(ErrorDto.BuildTechnical("ERROR"));
        }        
    }
}
