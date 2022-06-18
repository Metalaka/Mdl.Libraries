namespace Mdl.Collections.Enumerators;

using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Toolkit.Diagnostics;

/// <summary>
/// Look behind enumerator.
/// </summary>
/// <typeparam name="TValue">Type of the enumerable values.</typeparam>
public class Lookbehind<TValue> : IEnumerable<Lookbehind<TValue>.Data>
{
    private readonly Lazy<IEnumerable<Data>> _enumerable;

    public Lookbehind(IEnumerable<TValue> enumerable)
    {
        Guard.IsNotNull(enumerable, nameof(enumerable));

        _enumerable = new Lazy<IEnumerable<Data>>(() => BuildData(enumerable));
    }

    public IEnumerator<Data> GetEnumerator()
    {
        return _enumerable.Value.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable) _enumerable.Value).GetEnumerator();
    }

    private static IEnumerable<Data> BuildData(IEnumerable<TValue> enumerable)
    {
        using IEnumerator<TValue> enumerator = enumerable.GetEnumerator();
        TValue? previous = default;
        bool first = true;

        while (enumerator.MoveNext())
        {
            TValue current = enumerator.Current;

            if (first)
            {
                first = false;

                yield return new Data(current);

                previous = current;
                continue;
            }

            yield return new Data(current, previous);
            previous = current;
        }
    }

    public readonly struct Data
    {
        public TValue Current { get; }
        public TValue? Previous { get; }
        public bool HasPrevious { get; }

        internal Data(TValue current)
        {
            Current = current;
            Previous = default;
            HasPrevious = false;
        }

        internal Data(TValue current, TValue? previous)
        {
            Current = current;
            Previous = previous;
            HasPrevious = true;
        }
    }
}
