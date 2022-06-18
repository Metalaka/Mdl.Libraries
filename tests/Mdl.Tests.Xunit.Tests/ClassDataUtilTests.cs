namespace Mdl.Tests.Xunit.Tests;

using System.Collections.Generic;
using global::Xunit;

public class ClassDataUtilTests
{
    [Fact]
    public void BuildEnumerator_ShouldReturnValuesForEachParametersAndRows()
    {
        List<object[]> list = new()
        {
            new object[] {1},
            new object[] {2},
        };
        List<object[]> list2 = new()
        {
            new object[] {123},
        };
        object[][] expected =
        {
            new object[] {1, 123},
            new object[] {2, 123},
        };
        List<object[]> result = new();
        using IEnumerator<object[]> sut = ClassDataUtil.BuildEnumerator(list, list2);

        while (sut.MoveNext())
        {
            result.Add(sut.Current);
        }

        Assert.Equal(expected, result);
    }
}
