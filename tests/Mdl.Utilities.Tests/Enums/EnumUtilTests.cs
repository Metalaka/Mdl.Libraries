using System.Collections.Generic;
using System.Linq;
using Mdl.Utilities.Enums;
using Xunit;

namespace Mdl.Utilities.Tests.Enums
{
    public class EnumUtilTests
    {
        private enum TestData
        {
            One,
            Two,
            Zero
        }

        [Fact]
        public void GetValuesNames_ShouldReturnValueAndNames_WhenCalled()
        {
            var expected = new[]
            {
                (TestData.One, "One"),
                (TestData.Two, "Two"),
                (TestData.Zero, "Zero"),
            };

            var result = EnumUtil.GetValuesNames<TestData>();

            Assert.Equal(expected, result);
        }
    }
}