using FluentAssertions;
using GBMProject.Business.Contracts;
using System.Linq;

namespace GBMProject.UnitTest
{
    public class OperationResultAnalize
    {
        private const string codeThechnicalError = "TE001";
        private const string codeUserError = "BE001";
        
        internal static void FailureThecnical<T>(OperationResult<T> operationResult)
        {
            operationResult.Failure.Should().BeTrue();
            operationResult.ErrorList.Should().NotBeNull();
            operationResult.ErrorList.Any(x => x.Code.Should().Equals(codeThechnicalError));
        }
        internal static void FailureUser<T>(OperationResult<T> operationResult)
        {
            operationResult.Failure.Should().BeTrue();
            operationResult.ErrorList.Should().NotBeNull();
            operationResult.ErrorList.Any(x => x.Code.Should().Equals(codeUserError));

        }
        internal static void Succesfull<T>(OperationResult<T> operationResult, T resultHope)
        {
            operationResult.Failure.Should().BeFalse();
            operationResult.ErrorList.Should().BeNull();
            operationResult.ErrorList.Any(x => x.Equals(resultHope));
        }
        internal static void Succesfull<T>(OperationResult<T> operationResult)
        {
            operationResult.Failure.Should().BeFalse();
            operationResult.ErrorList.Should().BeNull();
        }
        internal static void FailureSimple(OperationResult operationResult)
        {
            operationResult.Failure.Should().BeTrue();
            operationResult.ErrorList.Should().NotBeNull();
        }
        internal static void SuccesfullSimple(OperationResult operationResult)
        {
            operationResult.Failure.Should().BeFalse();
            operationResult.ErrorList.Should().BeNull();
        }
    }

}
