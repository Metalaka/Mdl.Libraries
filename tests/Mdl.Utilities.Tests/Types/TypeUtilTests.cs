namespace Mdl.Utilities.Tests.Types;

using Mdl.Utilities.Types;
using Xunit;

public class TypeUtilTests
{
    [Fact]
    public void GetCurrentMethodName_ShouldReturnTheCallerMethodName()
    {
        string methodName = TypeUtil.GetCurrentMethodName();

        Assert.Equal(nameof(GetCurrentMethodName_ShouldReturnTheCallerMethodName), methodName);
    }
}
