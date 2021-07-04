using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Mdl.Utilities.Types
{
    public static class TypeUtil
    {
        /// <summary>
        /// Return the method name.
        /// </summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static string GetCurrentMethodName()
        {
            var stackTrace = new StackTrace();
            StackFrame frame = stackTrace.GetFrame(1)!;

            return frame.GetMethod()?.Name ?? "";
        }
    }
}