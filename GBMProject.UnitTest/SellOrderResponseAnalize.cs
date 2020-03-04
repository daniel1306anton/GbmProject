using FluentAssertions;
using GBMProject.Entities.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBMProject.UnitTest
{
    internal class SellOrderResponseAnalize
    {        
        internal static void FailureThecnical(SellOrderResponseDto response)
        {
            response.Success.Should().BeFalse();
            response.BusinessErrorList.Should().NotBeNull();
            response.BusinessErrorList.Any().Should().BeTrue();            
        }
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
