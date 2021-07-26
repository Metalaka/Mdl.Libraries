using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Mdl.Collections.Enumerators;

namespace Mdl.Tests.Xunit
{
    /// <summary>
    /// Utility class of <see cref="Xunit.ClassData"/>
    /// </summary>
    public static class ClassDataUtil
    {
        /// <summary>
        /// Build an enumerator from several enumerable
        /// </summary>
        public static IEnumerator<object[]> BuildEnumerator(params IEnumerable[] enumerable)
        {
            var multipleMax = new MultipleMax(enumerable);
            
            foreach (var data in multipleMax)
            {
                yield return data.Cast<object[]>().Select(e => e[0]).ToArray();
            }
        }
    }
}
