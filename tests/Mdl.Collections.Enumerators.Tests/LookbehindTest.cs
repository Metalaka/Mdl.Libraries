using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Mdl.Collections.Enumerators.Tests
{
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
            var list = new List<int>
            {
                1, 2
            };
            var expected = new int[]
            {
                0, 1
            };
            var result = new List<int>();
            var sut = new Lookbehind<int>(list);

            foreach (var data in sut)
            {
                result.Add(data.Previous);
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
            IEnumerable sut = new Lookbehind<int>(list);

            foreach (var data in sut)
            {
                result.Add(((Lookbehind<int>.Data) data).Current);
            }

            Assert.Equal(expected, result);
        }
        
        [Fact]
        public void GetEnumerator_ShouldReturnTheWhetherTheInnerEnumerableHasPreviousValue()
        {
            var list = new List<int>
            {
                1, 2, 3
            };
            var expected = new bool[]
            {
                false, true, true
            };
            var result = new List<bool>();
            var sut = new Lookbehind<int>(list);

            foreach (var data in sut)
            {
                result.Add(data.HasPrevious);
            }

            Assert.Equal(expected, result);
        }
    }
}