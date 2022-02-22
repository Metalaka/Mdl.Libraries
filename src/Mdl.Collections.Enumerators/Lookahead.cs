using System;
using System.Collections;
using System.Collections.Generic;
using Mdl.Utilities.Ensures;

namespace Mdl.Collections.Enumerators
{
    /// <summary>
    /// Look ahead enumerator.
    /// </summary>
    /// <typeparam name="TValue">Type of the enumerable values.</typeparam>
    public class Lookahead<TValue> : IEnumerable<Lookahead<TValue>.Data>
    {
        private readonly Lazy<IEnumerable<Data>> _enumerable;

        public Lookahead(IEnumerable<TValue> enumerable)
        {
            Ensure.NotNull(enumerable);
            
            _enumerable = new Lazy<IEnumerable<Data>>(() => BuildData(enumerable));
        }

        private static IEnumerable<Data> BuildData(IEnumerable<TValue> enumerable)
        {
            using IEnumerator<TValue> enumerator = enumerable.GetEnumerator();
            if (!enumerator.MoveNext())
            {
                yield break;
            }
            TValue previous = enumerator.Current;

            while (enumerator.MoveNext())
            {
                yield return new Data(previous, enumerator.Current);
                previous = enumerator.Current;
            }
            
            yield return new Data(previous);
        }

        public IEnumerator<Data> GetEnumerator()
        {
            return _enumerable.Value.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable) _enumerable.Value).GetEnumerator();
        }

        public readonly struct Data
        {
            public TValue Current { get; }
            public TValue? Next { get; }
            public bool HasNext { get; }

            internal Data(TValue current)
            {
                Current = current;
                Next = default;
                HasNext = false;
            }
            
            internal Data(TValue current, TValue next)
            {
                Current = current;
                Next = next;
                HasNext = true;
            }
        }
    }
}