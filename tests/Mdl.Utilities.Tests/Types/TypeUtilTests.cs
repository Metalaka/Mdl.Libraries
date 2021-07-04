using System;
using Mdl.Utilities.Types;
using Xunit;

namespace Mdl.Utilities.Tests.Types
{
    public class TypeUtilTests
    {
        [Fact]
        public void GetCurrentMethodName_ShouldReturnTheCallerMethodName()
        {
            string methodName = TypeUtil.GetCurrentMethodName();

            Assert.Equal(nameof(GetCurrentMethodName_ShouldReturnTheCallerMethodName), methodName);
        }
    }
}