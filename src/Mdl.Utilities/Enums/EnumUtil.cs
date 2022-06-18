namespace Mdl.Utilities.Enums;

using System;
using System.Collections.Generic;
using System.Linq;

public static class EnumUtil
{
    /// <summary>
    /// Return the values and affected label of a <see cref="TEnum" /> enum.
    /// </summary>
    /// <typeparam name="TEnum">An enum type.</typeparam>
    public static IEnumerable<(TEnum Value, string Name)> GetValuesNames<TEnum>() where TEnum : struct, Enum
    {
#if NETSTANDARD
        return ((TEnum[]) Enum.GetValues(typeof(TEnum))).Select(value => (value, value.ToString()));
#else
        return Enum.GetValues<TEnum>().Select(value => (value, value.ToString()));
#endif
    }
}
