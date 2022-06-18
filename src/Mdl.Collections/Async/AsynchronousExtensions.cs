namespace Mdl.Collections.Async;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public static class AsynchronousExtensions
{
    public static async Task<bool> AnyAsParallelAsync<TSource>(
        this IEnumerable<TSource> source,
        Func<TSource, Task<bool>> predicate)
    {
        return await Task.Run(() => source.AsParallel().Any(s => predicate(s).Result));
    }

    public static async Task<bool> AllAsParallelAsync<TSource>(
        this IEnumerable<TSource> source,
        Func<TSource, Task<bool>> predicate)
    {
        return await Task.Run(() => source.AsParallel().All(s => predicate(s).Result));
    }
}
