using System;
using System.Collections.Generic;
using System.Linq;

namespace Mdl.Collections
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(
            this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector)
        {
            return source
                .GroupBy(keySelector)
                .Select(group => group.First());
        }

        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (T item in source)
            {
                action(item);

                yield return item;
            }
        }
    }
}