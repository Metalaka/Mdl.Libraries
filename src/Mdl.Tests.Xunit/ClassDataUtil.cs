namespace Mdl.Tests.Xunit;

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Mdl.Collections.Enumerators;

/// <summary>
/// Utility class of <see cref="Xunit.ClassData" />
/// </summary>
public static class ClassDataUtil
{
    /// <summary>
    /// Build an enumerator from several enumerable
    /// </summary>
    public static IEnumerator<object[]> BuildEnumerator(params IEnumerable[] enumerable)
    {
        MultipleMax multipleMax = new(enumerable);

        foreach (IEnumerable? data in multipleMax)
        {
            yield return data.Cast<object[]>().Select(e => e[0]).ToArray();
        }
    }
}
