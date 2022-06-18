namespace Mdl.Utilities.Tests.IO;

using System.IO;
using System.Text;
using Mdl.Utilities.IO;
using Xunit;

public class StreamUtilTests
{
    [Fact]
    public void AsString_ShouldReturnAString_WhenStreamIsEmpty()
    {
        MemoryStream stream = new();

        string result = StreamUtil.AsString(stream);

        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void AsString_ShouldReturnTheWholeContentOfTheStream_WhenCalled()
    {
        const string value = "Bob";
        MemoryStream stream = new(Encoding.UTF8.GetBytes(value));

        string result = StreamUtil.AsString(stream, Encoding.UTF8);

        Assert.Equal(value, result);
    }

    [Fact]
    public void AsString_ShouldReturnTheWholeContentOfTheStream_WhenTheStreamIsNotAtTheStart()
    {
        const string value = "Bob\n\0End";
        MemoryStream stream = new(Encoding.UTF8.GetBytes(value));
        stream.ReadByte();
        stream.ReadByte();

        string result = StreamUtil.AsString(stream, Encoding.UTF8);

        Assert.Equal(value, result);
    }
}
