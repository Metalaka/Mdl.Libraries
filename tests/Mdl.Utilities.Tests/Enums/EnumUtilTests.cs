namespace Mdl.Utilities.Tests.Enums;

using System.Collections.Generic;
using Mdl.Utilities.Enums;
using Xunit;

public class EnumUtilTests
{
    [Fact]
    public void GetValuesNames_ShouldReturnValueAndNames_WhenCalled()
    {
        (TestData, string)[] expected =
        {
            (TestData.One, "One"),
            (TestData.Two, "Two"),
            (TestData.Zero, "Zero"),
        };

        IEnumerable<(TestData Value, string Name)> result = EnumUtil.GetValuesNames<TestData>();

        Assert.Equal(expected, result);
    }

    private enum TestData
    {
        One,
        Two,
        Zero,
    }
}
