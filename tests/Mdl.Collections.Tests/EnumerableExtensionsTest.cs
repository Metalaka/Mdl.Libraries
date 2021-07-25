using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Mdl.Collections.Tests
{
    public class EnumerableExtensionsTest
    {
        [Fact]
        public void DistinctBy_ShouldReturnTheFirstMatchingOccurenceOfEachKey()
        {
            var dictionary = new List<(string, int)>
            {
                ("A", 1),
                ("A", 2),
                ("B", 3),
            };
            var expected = new List<(string, int)>
            {
                ("A", 1),
                ("B", 3),
            };

            var result = dictionary.DistinctBy(tuple => tuple.Item1);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ForEach_ShouldExecutePredicateOnEveryItems()
        {
            int result = 0;
            const int expected = 4;
            var numbers = new[] {1, 3};

            var enumerable = numbers.ForEach(n => result += n).ToList();

            Assert.Equal(expected, result);
            Assert.Equal(numbers, enumerable);
        }
    }
}