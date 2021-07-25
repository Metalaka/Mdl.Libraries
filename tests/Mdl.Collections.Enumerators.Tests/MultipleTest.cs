using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Mdl.Collections.Enumerators.Tests
{
    public class MultipleTest
    {
        [Fact]
        public void Count_ShouldReturnTheMaxCountOfInnerEnumerables_WithEnumerable()
        {
            var list = new List<int>
            {
                1, 2, 3, 4
            };
            var list2 = new List<int>
            {
                123, 21
            };
            var expected = new int[]
            {
                list.Count,
                list2.Count,
            }.Min();
            var sut = new Multiple<int>(list, list2);

            var result = sut.Count();

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

            var list = Data();
            var list2 = Data2();
            var sut = new Multiple<int>(list, list2);

            var result = sut.Count();

            Assert.Equal(1, result);
        }

        [Fact]
        public void GetEnumerator_ShouldReturnTheCountOfInnerEnumerables_WithEnumerable()
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
            };
            var result = new List<int>();
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
            var list = new List<int>
            {
                1, 2
            };
            var list2 = new List<int>
            {
                123
            };
            
            Assert.Throws<InvalidOperationException>(() => new Multiple<int>(true, list, list2));
        }
        
        [Fact]
        public void GetEnumerator_ShouldReturnTheCountOfInnerEnumerables_WithLengthCheck()
        {
            var list = new List<int>
            {
                1, 2
            };
            var list2 = new List<int>
            {
                123, 456
            };
            var expected = new int[]
            {
                124, 458
            };
            var result = new List<int>();
            IEnumerable sut = new Multiple<int>(true, list, list2);

            foreach (IEnumerable enumerable in sut)
            {
                result.Add(enumerable.OfType<int>().Sum());
            }

            Assert.Equal(expected, result);
        }
    }
}