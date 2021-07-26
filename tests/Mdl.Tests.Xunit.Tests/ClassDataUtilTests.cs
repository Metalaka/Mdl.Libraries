using System.Collections.Generic;
using Xunit;

namespace Mdl.Tests.Xunit.Tests
{
    public class ClassDataUtilTests
    {
        [Fact]
        public void BuildEnumerator_ShouldReturnValuesForEachParametersAndRows()
        {
            var list = new List<object[]>
            {
                new object[] {1},
                new object[] {2},
            };
            var list2 = new List<object[]>
            {
                new object[] {123},
            };
            var expected = new object[][]
            {
                new object[] {1, 123,},
                new object[] {2, 123,},
            };
            var result = new List<object[]>();
            using IEnumerator<object[]> sut = ClassDataUtil.BuildEnumerator(list, list2);

            while (sut.MoveNext())
            {
                result.Add(sut.Current);
            }

            Assert.Equal(expected, result);
        }
    }
}