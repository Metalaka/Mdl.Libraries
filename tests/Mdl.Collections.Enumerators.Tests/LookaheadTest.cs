using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace Mdl.Collections.Enumerators.Tests
{
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
            var list = new List<int>
            {
                1, 2
            };
            var expected = new int[]
            {
                2, 0
            };
            var result = new List<int>();
            var sut = new Lookahead<int>(list);

            foreach (var data in sut)
            {
                result.Add(data.Next);
            }

            Assert.Equal(expected, result);
        }
        
        [Fact]
        public void IEnumerableGetEnumerator_ShouldReturnTheCurrentValueOfInnerEnumerable()
        {
            var list = new List<int>
            {
                1, 2, 3
            };
            var expected = new int[]
            {
                1, 2, 3
            };
            var result = new List<int>();
            IEnumerable sut = new Lookahead<int>(list);

            foreach (var data in sut)
            {
                result.Add(((Lookahead<int>.Data) data).Current);
            }

            Assert.Equal(expected, result);
        }
        
        [Fact]
        public void GetEnumerator_ShouldReturnWhetherTheInnerEnumerableHasPreviousValue()
        {
            var list = new List<int>
            {
                1, 2, 3
            };
            var expected = new bool[]
            {
                true, true, false
            };
            var result = new List<bool>();
            var sut = new Lookahead<int>(list);

            foreach (var data in sut)
            {
                result.Add(data.HasNext);
            }

            Assert.Equal(expected, result);
        }
    }
}