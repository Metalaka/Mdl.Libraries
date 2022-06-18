namespace Mdl.Utilities.Types;

using System.Diagnostics;
using System.Runtime.CompilerServices;

public static class TypeUtil
{
    /// <summary>
    /// Return the method name.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static string GetCurrentMethodName()
    {
        StackTrace stackTrace = new();
        StackFrame frame = stackTrace.GetFrame(1)!;

        return frame.GetMethod()?.Name ?? "";
    }
}
