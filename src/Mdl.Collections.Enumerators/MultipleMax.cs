using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Mdl.Collections.Enumerators
{
    /// <summary>
    /// Iterates over all attached enumerators until everyone are consumed.
    /// </summary>
    public class MultipleMax : IEnumerable<IEnumerable>
    {
        private readonly Lazy<IEnumerable<IEnumerable>> _enumerable;

        public MultipleMax(params IEnumerable[] enumerables)
        {
            if (enumerables is null || enumerables.Any(e => e is null))
            {
                throw new ArgumentNullException(nameof(enumerables));
            }
            
            if (!enumerables.Any())
            {
                throw new ArgumentException("No enumerable provided.");
            }

            _enumerable = new Lazy<IEnumerable<IEnumerable>>(() => BuildData(enumerables));
        }

        private static IEnumerable<IEnumerable> BuildData(IEnumerable<IEnumerable> enumerables)
        {
            IEnumerable<ConsumableEnumerator> enumerators = enumerables
                .Select(e => new ConsumableEnumerator(e))
                .ToList();

            while (true)
            {
                var bucket = enumerators.ForEach(MoveNextOrReset)
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

        public IEnumerator<IEnumerable> GetEnumerator()
        {
            return _enumerable.Value.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable) _enumerable.Value).GetEnumerator();
        }

        private sealed class ConsumableEnumerator : IEnumerator
        {
            private readonly IEnumerable _enumerable;
            private IEnumerator _enumerator;
            public bool Consumed { get; private set; }

            public ConsumableEnumerator(IEnumerable enumerable, bool consumed = false)
            {
                _enumerable = enumerable;
                Consumed = consumed;

                Reset();
            }

            public void SetConsumed()
            {
                Consumed = true;
            }

            public bool MoveNext()
            {
                return _enumerator.MoveNext();
            }

            public void Reset()
            {
                _enumerator = _enumerable.GetEnumerator();
            }

            public object Current => _enumerator.Current;
        }
    }
}