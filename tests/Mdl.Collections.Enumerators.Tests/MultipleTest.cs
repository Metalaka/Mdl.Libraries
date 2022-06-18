namespace Mdl.Collections.Enumerators.Tests;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xunit;

public class MultipleTest
{
    [Fact]
    public void Count_ShouldReturnTheMaxCountOfInnerEnumerables_WithEnumerable()
    {
        List<int> list = new()
        {
            1, 2, 3, 4,
        };
        List<int> list2 = new()
        {
            123, 21,
        };
        int expected = new[]
        {
            list.Count,
            list2.Count,
        }.Min();
        Multiple<int> sut = new(list, list2);

        int result = sut.Count();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void Count_ShouldReturnTheMaxCountOfInnerEnumerables_WithYieldEnumerable()
    {
        IEnumerable<int> Data()
        {
            yield return 1;
            yield return 2;
        }

        IEnumerable<int> Data2()
        {
            yield return 123;
        }

        IEnumerable<int> list = Data();
        IEnumerable<int> list2 = Data2();
        Multiple<int> sut = new(list, list2);

        int result = sut.Count();

        Assert.Equal(1, result);
    }

    [Fact]
    public void GetEnumerator_ShouldReturnTheCountOfInnerEnumerables_WithEnumerable()
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
        };
        List<int> result = new();
        IEnumerable sut = new Multiple<int>(list, list2);

        foreach (IEnumerable enumerable in sut)
        {
            result.Add(enumerable.OfType<int>().Sum());
        }

        Assert.Equal(expected, result);
    }

    [Fact]
    public void Constructor_ShouldThrow_WhenNoEnumerableProvided()
    {
        Assert.Throws<ArgumentException>(() => new Multiple<int>());
    }

    [Fact]
    public void Constructor_ShouldThrow_WhenNullEnumerableProvided()
    {
        Assert.Throws<ArgumentNullException>(() => new Multiple<int>(null));
    }

    [Fact]
    public void Constructor_ShouldThrow_WhenNullEnumerablesProvided()
    {
        Assert.Throws<ArgumentNullException>(() => new Multiple<int>(null, null));
    }

    [Fact(Skip = "No use case available to test this edge case.")]
    public void Constructor_ShouldThrow_WhenEnumerableCantBeReset()
    {
        throw new NotImplementedException("No use case available to test this edge case.");
    }

    [Fact]
    public void Constructor_ShouldThrow_WhenLengthCheckAndEnumerableLengthDiffer()
    {
        List<int> list = new()
        {
            1, 2,
        };
        List<int> list2 = new()
        {
            123,
        };

        Assert.Throws<InvalidOperationException>(() => new Multiple<int>(true, list, list2));
    }

    [Fact]
    public void GetEnumerator_ShouldReturnTheCountOfInnerEnumerables_WithLengthCheck()
    {
        List<int> list = new()
        {
            1, 2,
        };
        List<int> list2 = new()
        {
            123, 456,
        };
        int[] expected =
        {
            124, 458,
        };
        List<int> result = new();
        IEnumerable sut = new Multiple<int>(true, list, list2);

        foreach (IEnumerable enumerable in sut)
        {
            result.Add(enumerable.OfType<int>().Sum());
        }

        Assert.Equal(expected, result);
    }
}
