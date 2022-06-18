namespace Mdl.Utilities.Types;

using System;
using System.Linq;
using System.Runtime.CompilerServices;

public static class TypeExtensions
{
    /// <summary>
    /// Indicate whether a type is considered anonymous.
    /// </summary>
    public static bool IsAnonymousType(this Type type)
    {
        bool hasCompilerGeneratedAttribute = type
            .GetCustomAttributes(typeof(CompilerGeneratedAttribute), false)
            .Any();
        bool? nameContainsAnonymousType = type.FullName?.Contains(@"AnonymousType");
        bool isAnonymousType = hasCompilerGeneratedAttribute &&
                               nameContainsAnonymousType.HasValue &&
                               nameContainsAnonymousType.Value;

        return isAnonymousType;
    }
}
