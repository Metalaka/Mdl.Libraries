namespace Mdl.Collections.Enumerators.Tests;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xunit;

public class MultipleMaxTest
{
    [Fact]
    public void Count_ShouldReturnTheMaxCountOfInnerEnumerables_WithEnumerable()
    {
        List<string> list = new()
        {
            "aze", "rty",
        };
        List<int> list2 = new()
        {
            123,
        };
        int expected = new[]
        {
            list.Count,
            list2.Count,
        }.Max();
        MultipleMax sut = new(list, list2);

        int result = sut.Count();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void Count_ShouldReturnTheMaxCountOfInnerEnumerables_WithYieldEnumerable()
    {
        IEnumerable Data()
        {
            yield return "test";
            yield return string.Empty;
        }

        IEnumerable Data2()
        {
            yield return 123;
        }

        IEnumerable list = Data();
        IEnumerable list2 = Data2();
        MultipleMax sut = new(list, list2);

        int result = sut.Count();

        Assert.Equal(2, result);
    }

    [Fact]
    public void GetEnumerator_ShouldReturnTheMaxCountOfInnerEnumerables_WithEnumerable()
    {
        List<int> list = new()
        {
            1, 2,
        };
        List<int> list2 = new()
        {
            123,
        };
        int[] expected =
        {
            124,
            125,
        };
        List<int> result = new();
        IEnumerable sut = new MultipleMax(list, list2);

        foreach (IEnumerable enumerable in sut)
        {
            result.Add(enumerable.OfType<int>().Sum());
        }

        Assert.Equal(expected, result);
    }

    [Fact]
    public void Constructor_ShouldThrow_WhenNoEnumerableProvided()
    {
        Assert.Throws<ArgumentException>(() => new MultipleMax());
    }

    [Fact]
    public void Constructor_ShouldThrow_WhenNullEnumerableProvided()
    {
        Assert.Throws<ArgumentNullException>(() => new MultipleMax(null));
    }

    [Fact]
    public void Constructor_ShouldThrow_WhenNullEnumerablesProvided()
    {
        Assert.Throws<ArgumentNullException>(() => new MultipleMax(null, null));
    }

    [Fact(Skip = "No use case available to test this edge case.")]
    public void Constructor_ShouldThrow_WhenEnumerableCantBeReset()
    {
        throw new NotImplementedException("No use case available to test this edge case.");
    }
}
