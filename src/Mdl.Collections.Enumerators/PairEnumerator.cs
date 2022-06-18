using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Mdl.Collections.Enumerators
{
    using Microsoft.Toolkit.Diagnostics;

    /// <summary>
    /// Iterates over two enumerator sequentially.
    /// </summary>
    /// <typeparam name="TFirst">Type of the first enumerable values.</typeparam>
    /// <typeparam name="TSecond">Type of the second enumerable values.</typeparam>
    public class PairEnumerator<TFirst, TSecond> : IEnumerable<(TFirst First, TSecond Second)>
    {
        private readonly Lazy<IEnumerable<(TFirst First, TSecond Second)>> _enumerable;

        public PairEnumerator(IEnumerable<TFirst> firstEnumerable, IEnumerable<TSecond> secondEnumerable)
        {
            Guard.IsNotNull(firstEnumerable, nameof(firstEnumerable));
            Guard.IsNotNull(secondEnumerable, nameof(secondEnumerable));

            if (firstEnumerable.Count() != secondEnumerable.Count())
            {
                throw new InvalidOperationException();
            }

            _enumerable = new Lazy<IEnumerable<(TFirst First, TSecond Second)>>(() =>
                BuildData(firstEnumerable, secondEnumerable)
            );
        }

        private static IEnumerable<(TFirst First, TSecond Second)> BuildData(
            IEnumerable<TFirst> firstEnumerable,
            IEnumerable<TSecond> secondEnumerable)
        {
            using IEnumerator<TFirst> firstEnumerator = firstEnumerable.GetEnumerator();
            using IEnumerator<TSecond> secondEnumerator = secondEnumerable.GetEnumerator();

            while (firstEnumerator.MoveNext() && secondEnumerator.MoveNext())
            {
                yield return (firstEnumerator.Current, secondEnumerator.Current);
            }
        }

        public IEnumerator<(TFirst First, TSecond Second)> GetEnumerator()
        {
            return _enumerable.Value.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable) _enumerable.Value).GetEnumerator();
        }
    }
}