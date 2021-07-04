using System;

namespace Mdl.Utilities.Ensures
{
    public static class Ensure
    {
        /// <summary>
        /// Throw an <see cref="ArgumentNullException"/> if <see cref="value"/> is null, empty or consists only of white-space characters.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        public static void NotNullOrWhiteSpace(object? value, string paramName, string? message = null)
        {
            var s = value as string;

            if (value is null || (value is string && string.IsNullOrWhiteSpace(s)))
            {
                if (string.IsNullOrWhiteSpace(message))
                {
                    throw new ArgumentNullException(paramName);
                }

                throw new ArgumentNullException(paramName, message);
            }
        }

        /// <summary>
        /// Throw an <see cref="ArgumentNullException"/> if <see cref="value"/> is null.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        public static void NotNull(object? value)
        {
            if (value is null)
            {
                throw new ArgumentNullException();
            }
        }
    }
}