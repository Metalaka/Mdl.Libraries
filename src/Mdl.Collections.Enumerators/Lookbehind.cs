using System;
using System.Collections;
using System.Collections.Generic;
using Mdl.Utilities.Ensures;

namespace Mdl.Collections.Enumerators
{
    /// <summary>
    /// Look behind enumerator.
    /// </summary>
    /// <typeparam name="TValue">Type of the enumerable values.</typeparam>
    public class Lookbehind<TValue> : IEnumerable<Lookbehind<TValue>.Data>
    {
        private readonly Lazy<IEnumerable<Data>> _enumerable;

        public Lookbehind(IEnumerable<TValue> enumerable)
        {
            Ensure.NotNull(enumerable);
            
            _enumerable = new Lazy<IEnumerable<Data>>(() => BuildData(enumerable));
        }

        private static IEnumerable<Data> BuildData(IEnumerable<TValue> enumerable)
        {
            using IEnumerator<TValue> enumerator = enumerable.GetEnumerator();
            TValue previous = default(TValue);
            bool first = true;

            while (enumerator.MoveNext())
            {
                TValue current = enumerator.Current;

                if (first)
                {
                    first = false;

                    yield return new Data(current, default, hasPrevious: false);

                    previous = current;
                    continue;
                }

                yield return new Data(current, previous);
                previous = current;
            }
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
            public TValue Previous { get; }
            public bool HasPrevious { get; }

            internal Data(TValue current, TValue previous, bool hasPrevious = true)
            {
                Current = current;
                Previous = previous;
                HasPrevious = hasPrevious;
            }
        }
    }
}