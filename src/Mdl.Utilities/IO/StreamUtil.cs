namespace Mdl.Utilities.IO;

using System.IO;
using System.Text;

public static class StreamUtil
{
    /// <summary>
    /// Read the <see cref="Stream" /> and return the resulting string.
    /// </summary>
    public static string AsString(Stream stream)
    {
        return AsString(stream, Encoding.UTF8);
    }

    /// <summary>
    /// Read the <see cref="Stream" /> and return the resulting string.
    /// </summary>
    public static string AsString(Stream stream, Encoding encoding)
    {
        stream.Seek(0, SeekOrigin.Begin);

        using StreamReader reader = new(stream, encoding);

        return reader.ReadToEnd();
    }
}
