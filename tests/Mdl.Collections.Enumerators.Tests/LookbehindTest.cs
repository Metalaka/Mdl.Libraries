namespace Mdl.Collections.Enumerators.Tests;

using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;

public class LookbehindTest
{
    [Fact]
    public void Constructor_ShouldThrow_WhenNullEnumerablesProvided()
    {
        Assert.Throws<ArgumentNullException>(() => new Lookbehind<int>(null));
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
            0, 1,
        };
        List<int> result = new();
        Lookbehind<int> sut = new(list);

        foreach (Lookbehind<int>.Data data in sut)
        {
            result.Add(data.Previous);
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
        IEnumerable sut = new Lookbehind<int>(list);

        foreach (object data in sut)
        {
            result.Add(((Lookbehind<int>.Data) data).Current);
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
            false, true, true,
        };
        List<bool> result = new();
        Lookbehind<int> sut = new(list);

        foreach (Lookbehind<int>.Data data in sut)
        {
            result.Add(data.HasPrevious);
        }

        Assert.Equal(expected, result);
    }
}
