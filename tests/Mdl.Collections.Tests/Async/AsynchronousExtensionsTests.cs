namespace Mdl.Collections.Tests.Async;

using System.Collections.Generic;
using System.Threading.Tasks;
using Mdl.Collections.Async;
using Xunit;

public class AsynchronousExtensionsTests
{
    [Fact]
    public async Task AnyAsParallelAsync_ShouldReturnTrue_IfAnItemMatchThePredicate()
    {
        const bool expected = true;
        List<bool> list = new()
        {
            false,
            false,
            true,
        };

        bool result = await list.AnyAsParallelAsync(b => Task.FromResult(b));

        Assert.Equal(expected, result);
    }

    [Fact]
    public async Task AllAsParallelAsync_ShouldReturnTrue_IfAllItemMatchThePredicate()
    {
        const bool expected = true;
        List<bool> list = new()
        {
            true,
            true,
            true,
        };

        bool result = await list.AllAsParallelAsync(b => Task.FromResult(b));

        Assert.Equal(expected, result);
    }

    [Fact]
    public async Task AllAsParallelAsync_ShouldReturnFalse_IfAnItemDoesNotMatchThePredicate()
    {
        const bool expected = false;
        List<bool> list = new()
        {
            true,
            false,
            true,
        };

        bool result = await list.AllAsParallelAsync(b => Task.FromResult(b));

        Assert.Equal(expected, result);
    }
}
