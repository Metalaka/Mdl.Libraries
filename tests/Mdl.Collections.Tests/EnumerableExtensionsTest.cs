namespace Mdl.Collections.Tests;

using System.Collections.Generic;
using System.Linq;
using Xunit;

public class EnumerableExtensionsTest
{
    [Fact]
    public void DistinctBy_ShouldReturnTheFirstMatchingOccurenceOfEachKey()
    {
        List<(string, int)> dictionary = new()
        {
            ("A", 1),
            ("A", 2),
            ("B", 3),
        };
        List<(string, int)> expected = new()
        {
            ("A", 1),
            ("B", 3),
        };

        IEnumerable<(string, int)> result = EnumerableExtensions.DistinctBy(dictionary, tuple => tuple.Item1);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void ForEach_ShouldExecutePredicateOnEveryItems()
    {
        int result = 0;
        const int expected = 4;
        int[] numbers = {1, 3};

        List<int> enumerable = Enumerable.ToList(numbers.ForEach(n => result += n));

        Assert.Equal(expected, result);
        Assert.Equal(numbers, enumerable);
    }
}
