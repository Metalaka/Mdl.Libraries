using System;
using Mdl.Utilities.Types;
using Xunit;

namespace Mdl.Utilities.Tests.Types
{
    public class TypeExtensionsTests
    {
        [Fact]
        public void IsAnonymousType_ShouldReturnTrue_WhenAnAnonymousTypeIsGiven()
        {
            var sut = new { };

            bool result = sut.GetType().IsAnonymousType();

            Assert.True(result);
        }

        [Fact]
        public void IsAnonymousType_ShouldReturnFalse_WhenATupleIsGiven()
        {
            (int, string) sut = (0, "");

            bool result = sut.GetType().IsAnonymousType();

            Assert.False(result);
        }
    }
}