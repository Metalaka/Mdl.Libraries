using System;
using System.IO;
using System.Text;
using Mdl.Utilities.IO;
using Xunit;

namespace Mdl.Utilities.Tests.IO
{
    public class StreamUtilTests
    {
        [Fact]
        public void AsString_ShouldReturnAString_WhenStreamIsEmpty()
        {
            var stream = new MemoryStream();

            string result = StreamUtil.AsString(stream);

            Assert.Equal(string.Empty, result);
        }

        [Fact]
        public void AsString_ShouldReturnTheWholeContentOfTheStream_WhenCalled()
        {
            const string value = "Bob";
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(value));

            string result = StreamUtil.AsString(stream, Encoding.UTF8);

            Assert.Equal(value, result);
        }

        [Fact]
        public void AsString_ShouldReturnTheWholeContentOfTheStream_WhenTheStreamIsNotAtTheStart()
        {
            const string value = "Bob\n\0End";
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(value));
            stream.ReadByte();
            stream.ReadByte();

            string result = StreamUtil.AsString(stream, Encoding.UTF8);

            Assert.Equal(value, result);
        }
    }
}