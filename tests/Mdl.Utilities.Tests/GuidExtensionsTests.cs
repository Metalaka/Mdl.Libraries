namespace Mdl.Utilities.Tests;

using System;
using System.Collections.Generic;
using Xunit;

public class GuidExtensionsTests
{
    public static IEnumerable<object[]> EmptyData =>
        new[]
        {
            new object[] {new Guid()},
            new object[] {Guid.Empty},
        };

    public static IEnumerable<object[]> ValidData =>
        new[]
        {
            new object[] {Guid.NewGuid()},
        };


    [Theory]
    [MemberData(nameof(EmptyData))]
    public void IsEmpty_ShouldReturnTrue_WhenValueIsZero(Guid sut)
    {
        Assert.True(sut.IsEmpty());
    }

    [Theory]
    [MemberData(nameof(ValidData))]
    public void IsEmpty_ShouldReturnFalse_WhenValueIsNotZero(Guid sut)
    {
        Assert.False(sut.IsEmpty());
    }
}
