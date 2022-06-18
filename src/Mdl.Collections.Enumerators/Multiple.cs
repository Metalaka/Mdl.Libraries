namespace Mdl.Collections.Enumerators;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Toolkit.Diagnostics;

/// <summary>
/// Sequentially iterates over all attached enumerators until the first reach it's end.
/// </summary>
/// <typeparam name="TValue">Type of enumerables values.</typeparam>
public class Multiple<TValue> : IEnumerable<IEnumerable<TValue>>
{
    private readonly Lazy<IEnumerable<IEnumerable<TValue>>> _enumerable;

    public Multiple(params IEnumerable<TValue>[] enumerables) : this(false, enumerables)
    {
    }

    public Multiple(bool checkLength, params IEnumerable<TValue>[] enumerables)
    {
        Guard.IsNotNull(enumerables, nameof(enumerables));
        Guard.IsNotEmpty(enumerables, nameof(enumerables));

        if (enumerables.Any(e => e is null))
        {
            throw new ArgumentNullException(nameof(enumerables));
        }

        if (checkLength && !HasSameCount(enumerables))
        {
            throw new InvalidOperationException();
        }

        _enumerable = new Lazy<IEnumerable<IEnumerable<TValue>>>(() => BuildData(enumerables));
    }

    public IEnumerator<IEnumerable<TValue>> GetEnumerator()
    {
        return _enumerable.Value.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable) _enumerable.Value).GetEnumerator();
    }

    private static bool HasSameCount(IEnumerable<IEnumerable<TValue>> enumerables)
    {
        try
        {
            return enumerables
                .Cast<ICollection>()
                .Select(collection => collection.Count)
                .Distinct()
                .Count() == 1;
        }
        catch (Exception)
        {
            return false;
        }
    }

    private static IEnumerable<IEnumerable<TValue>> BuildData(IEnumerable<IEnumerable<TValue>> enumerables)
    {
        IEnumerable<IEnumerator<TValue>> enumerators = enumerables.Select(e => e.GetEnumerator()).ToList();

        while (enumerators.All(e => e.MoveNext()))
        {
            yield return enumerators.Select(e => e.Current).ToArray();
        }
    }
}