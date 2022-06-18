namespace Mdl.Collections.Enumerators;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Toolkit.Diagnostics;

/// <summary>
/// Iterates over all attached enumerators until everyone are consumed.
/// </summary>
public class MultipleMax : IEnumerable<IEnumerable>
{
    private readonly Lazy<IEnumerable<IEnumerable>> _enumerable;

    public MultipleMax(params IEnumerable[] enumerables)
    {
        Guard.IsNotNull(enumerables, nameof(enumerables));
        Guard.IsNotEmpty(enumerables, nameof(enumerables));

        if (enumerables.Any(e => e is null))
        {
            throw new ArgumentNullException(nameof(enumerables));
        }

        _enumerable = new Lazy<IEnumerable<IEnumerable>>(() => BuildData(enumerables));
    }

    public IEnumerator<IEnumerable> GetEnumerator()
    {
        return _enumerable.Value.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable) _enumerable.Value).GetEnumerator();
    }

    private static IEnumerable<IEnumerable> BuildData(IEnumerable<IEnumerable> enumerables)
    {
        IEnumerable<ConsumableEnumerator> enumerators = enumerables
            .Select(e => new ConsumableEnumerator(e))
            .ToList();

        while (true)
        {
            object?[] bucket = enumerators.ForEach(MoveNextOrReset)
                .Select(e => e.Current)
                .ToArray();

            if (enumerators.All(e => e.Consumed))
            {
                yield break;
            }

            yield return bucket;
        }

        static void MoveNextOrReset(ConsumableEnumerator enumerator)
        {
            if (enumerator.MoveNext())
            {
                return;
            }

            enumerator.SetConsumed();
            enumerator.Reset();
            if (!enumerator.MoveNext())
            {
                throw new InvalidOperationException(@"Invalid state. Empty enumerable!");
            }
        }
    }

    private sealed class ConsumableEnumerator : IEnumerator
    {
        private readonly IEnumerable _enumerable;
        private IEnumerator? _enumerator;

        public ConsumableEnumerator(IEnumerable enumerable, bool consumed = false)
        {
            _enumerable = enumerable;
            Consumed = consumed;

            Reset();
        }

        public bool Consumed { get; private set; }

        public bool MoveNext()
        {
            return _enumerator?.MoveNext() ?? false;
        }

        public void Reset()
        {
            _enumerator = _enumerable.GetEnumerator();
        }

        public object? Current => _enumerator?.Current;

        public void SetConsumed()
        {
            Consumed = true;
        }
    }
}
