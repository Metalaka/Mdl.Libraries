using System.IO;
using System.Text;

namespace Mdl.Utilities.IO
{
    public static class StreamUtil
    {
        /// <summary>
        /// Read the <see cref="Stream"/> and return the resulting string.
        /// </summary>
        public static string AsString(Stream stream) => AsString(stream, Encoding.UTF8);

        /// <summary>
        /// Read the <see cref="Stream"/> and return the resulting string.
        /// </summary>
        public static string AsString(Stream stream, Encoding encoding)
        {
            stream.Seek(0, SeekOrigin.Begin);

            using var reader = new StreamReader(stream, encoding);

            return reader.ReadToEnd();
        }
    }
}