using System.Collections.Generic;
using System.Threading.Tasks;
using Mdl.Collections.Async;
using Xunit;

namespace Mdl.Collections.Tests.Async
{
    public class AsynchronousExtensionsTests
    {
        [Fact]
        public async Task AnyAsParallelAsync_ShouldReturnTrue_IfAnItemMatchThePredicate()
        {
            const bool expected = true;
            var list = new List<bool>()
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
            var list = new List<bool>()
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
            var list = new List<bool>()
            {
                true,
                false,
                true,
            };

            bool result = await list.AllAsParallelAsync(b => Task.FromResult(b));

            Assert.Equal(expected, result);
        }
    }
}