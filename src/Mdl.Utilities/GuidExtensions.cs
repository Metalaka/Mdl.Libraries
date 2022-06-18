namespace Mdl.Utilities;

using System;

public static class GuidExtensions
{
    /// <summary>
    /// Indicate whether the Guid value is all zero.
    /// </summary>
    public static bool IsEmpty(this Guid guid)
    {
        return guid == Guid.Empty;
    }
}
