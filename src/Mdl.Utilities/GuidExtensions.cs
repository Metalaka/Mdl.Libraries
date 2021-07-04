using System;

namespace Mdl.Utilities
{
    public static class GuidExtensions
    {
        /// <summary>
        /// Indicate whether the Guid value is all zero.
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public static bool IsEmpty(this Guid guid)
        {
            return guid == Guid.Empty;
        }
    }
}