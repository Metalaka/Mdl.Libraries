using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Mdl.Collections.Enumerators.Tests
{
    public class PairEnumeratorTest
    {
        [Fact]
        public void Constructor_ShouldThrow_WhenEnumerableLengthDiffer()
        {
            var list = new List<int>
            {
                1, 2
            };
            var list2 = new List<int>
            {
                123
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
            var sut = new PairEnumerator<int, int>(list, list2);

            foreach (var (fist, second) in sut)
            {
                result.Add(fist + second);
            }

            Assert.Equal(expected, result);
        }
    }
}