namespace Mdl.Collections.Enumerators.Tests;

using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;

public class PairEnumeratorTest
{
    [Fact]
    public void Constructor_ShouldThrow_WhenEnumerableLengthDiffer()
    {
        List<int> list = new()
        {
            1, 2,
        };
        List<int> list2 = new()
        {
            123,
        };

        Assert.Throws<InvalidOperationException>(() => new PairEnumerator<int, int>(list, list2));
    }

    [Fact]
    public void Constructor_ShouldThrow_WhenNullEnumerablesProvided()
    {
        Assert.Throws<ArgumentNullException>(() => new PairEnumerator<int, int>(new int[0], null));
        Assert.Throws<ArgumentNullException>(() => new PairEnumerator<int, int>(null, new int[0]));
        Assert.Throws<ArgumentNullException>(() => new PairEnumerator<int, int>(null, null));
    }

    [Fact]
    public void IEnumerableGetEnumerator_ShouldReturnTheCountOfInnerEnumerables()
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
        IEnumerable sut = new PairEnumerator<int, int>(list, list2);

        foreach ((int Fist, int Second) tuple in sut)
        {
            result.Add(tuple.Fist + tuple.Second);
        }

        Assert.Equal(expected, result);
    }

    [Fact]
    public void GetEnumerator_ShouldReturnTheCountOfInnerEnumerables()
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
        PairEnumerator<int, int> sut = new(list, list2);

        foreach ((int fist, int second) in sut)
        {
            result.Add(fist + second);
        }

        Assert.Equal(expected, result);
    }
}
