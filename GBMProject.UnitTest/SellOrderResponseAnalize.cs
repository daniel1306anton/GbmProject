using FluentAssertions;
using GBMProject.Entities.Response;
using System.Linq;

namespace GBMProject.UnitTest
{
    internal class SellOrderResponseAnalize
    {     
        internal static void FailureUser(SellOrderResponseDto response)
        {
            response.Success.Should().BeTrue();
            response.BusinessErrorList.Should().NotBeNull();
            response.BusinessErrorList.Any().Should().BeTrue();
        }
        internal static void Succesfull(SellOrderResponseDto response)
        {
            response.Success.Should().BeTrue();
        }
    }
}
