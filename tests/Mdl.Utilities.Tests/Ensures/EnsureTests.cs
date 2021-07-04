using System;
using Mdl.Utilities.Ensures;
using Xunit;

namespace Mdl.Utilities.Tests.Ensures
{
    public class EnsureTests
    {
        [Fact]
        public void NotNull_ShouldThrow_WhenValueIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => Ensure.NotNull(null));
        }

        [Fact]
        public void NotNull_ShouldNotThrow_WhenValueIsNotNull()
        {
            Ensure.NotNull("");
            Ensure.NotNull(new object());
        }

        [Fact]
        public void NotNullOrWhiteSpace_ShouldThrow_WhenValueIsNull()
        {
            const string paramName = "nullValueParam";
            const string message = "nullValueMessage";

            var exception = Assert.Throws<ArgumentNullException>(() =>
                Ensure.NotNullOrWhiteSpace(null, paramName, message)
            );

            Assert.Equal(paramName, exception.ParamName);
            Assert.StartsWith(message, exception.Message);
        }

        [Fact]
        public void NotNullOrWhiteSpace_ShouldThrow_WhenValueIsAnEmptyString()
        {
            const string paramName = "nullValueParam";
            const string message = "nullValueMessage";

            var exception = Assert.Throws<ArgumentNullException>(() =>
                Ensure.NotNullOrWhiteSpace(string.Empty, paramName, message)
            );

            Assert.Equal(paramName, exception.ParamName);
            Assert.StartsWith(message, exception.Message);
        }

        [Fact]
        public void NotNullOrWhiteSpace_ShouldThrow_WhenValueIsAnWhiteSpaceString()
        {
            const string paramName = "nullValueParam";
            const string message = "nullValueMessage";

            var exception = Assert.Throws<ArgumentNullException>(() =>
                Ensure.NotNullOrWhiteSpace("  	\t\v", paramName, message)
            );

            Assert.Equal(paramName, exception.ParamName);
            Assert.StartsWith(message, exception.Message);
        }

        [Fact]
        public void NotNullOrWhiteSpace_ShouldNotThrow_WhenValueIsSet()
        {
            Ensure.NotNullOrWhiteSpace("Bob", "");
        }

        [Fact]
        public void NotNullOrWhiteSpace_ShouldThrow_WhenValueIsNullAndNoMessageIsSet()
        {
            const string paramName = "nullValueParam";

            var exception = Assert.Throws<ArgumentNullException>(() =>
                Ensure.NotNullOrWhiteSpace("", paramName)
            );

            Assert.Equal(paramName, exception.ParamName);
        }
    }
}