namespace Mdl.Collections.Enumerators.Tests;

using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;

public class LookaheadTest
{
    [Fact]
    public void Constructor_ShouldThrow_WhenNullEnumerablesProvided()
    {
        Assert.Throws<ArgumentNullException>(() => new Lookahead<int>(null));
    }

    [Fact]
    public void GetEnumerator_ShouldReturnThePreviousValueOfInnerEnumerable()
    {
        List<int> list = new()
        {
            1, 2,
        };
        int[] expected =
        {
            2, 0,
        };
        List<int> result = new();
        Lookahead<int> sut = new(list);

        foreach (Lookahead<int>.Data data in sut)
        {
            result.Add(data.Next);
        }

        Assert.Equal(expected, result);
    }

    [Fact]
    public void IEnumerableGetEnumerator_ShouldReturnTheCurrentValueOfInnerEnumerable()
    {
        List<int> list = new()
        {
            1, 2, 3,
        };
        int[] expected =
        {
            1, 2, 3,
        };
        List<int> result = new();
        IEnumerable sut = new Lookahead<int>(list);

        foreach (object data in sut)
        {
            result.Add(((Lookahead<int>.Data) data).Current);
        }

        Assert.Equal(expected, result);
    }

    [Fact]
    public void GetEnumerator_ShouldReturnWhetherTheInnerEnumerableHasPreviousValue()
    {
        List<int> list = new()
        {
            1, 2, 3,
        };
        bool[] expected =
        {
            true, true, false,
        };
        List<bool> result = new();
        Lookahead<int> sut = new(list);

        foreach (Lookahead<int>.Data data in sut)
        {
            result.Add(data.HasNext);
        }

        Assert.Equal(expected, result);
    }
}
