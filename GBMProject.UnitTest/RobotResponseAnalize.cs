using FluentAssertions;
using GBMProject.Entities.Response;
using System.Linq;

namespace GBMProject.UnitTest
{
    internal class RobotResponseAnalize
    {
        private const string codeThechnicalError = "TE001";
        private const string codeUserError = "BE001";
        private const ushort typeInternalError = 1;
        private const ushort typeExternalError = 2;
        internal static void FailureThecnical(RobotResponseDto response)
        {
            response.Success.Should().BeFalse();
            response.ErrorList.Should().NotBeNull();
            response.ErrorList.Any(x => x.Code == codeThechnicalError);
            response.ErrorList.Any(x => x.Type == typeInternalError);
        }

        

        internal static void FailureUser(RobotResponseDto response)
        {
            response.Success.Should().BeFalse();
            response.ErrorList.Should().NotBeNull();
            response.ErrorList.Any(x => x.Code == codeUserError);
            response.ErrorList.Any(x => x.Type == typeExternalError);
        }
        internal static void Succesfull(RobotResponseDto response)
        {
            response.Success.Should().BeTrue();
        }
    }
}
