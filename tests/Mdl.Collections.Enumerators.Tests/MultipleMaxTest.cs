using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Mdl.Collections.Enumerators.Tests
{
    public class MultipleMaxTest
    {
        [Fact]
        public void Count_ShouldReturnTheMaxCountOfInnerEnumerables_WithEnumerable()
        {
            var list = new List<string>
            {
                "aze", "rty"
            };
            var list2 = new List<int>
            {
                123
            };
            var expected = new int[]
            {
                list.Count,
                list2.Count,
            }.Max();
            var sut = new MultipleMax(list, list2);

            var result = sut.Count();

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

            var list = Data();
            var list2 = Data2();
            var sut = new MultipleMax(list, list2);

            var result = sut.Count();

            Assert.Equal(2, result);
        }

        [Fact]
        public void GetEnumerator_ShouldReturnTheMaxCountOfInnerEnumerables_WithEnumerable()
        {
            var list = new List<int>
            {
                1, 2
            };
            var list2 = new List<int>
            {
                123
            };
            var expected = new int[]
            {
                124,
                125,
            };
            var result = new List<int>();
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
}