using NUnit.Framework;

namespace ParsingStrings.Tests
{
    [TestFixture]
    public sealed class BooleanParserTests
    {
        [TestCase(null)]
        [TestCase("")]
        [TestCase("abc")]
        public void TryParseBoolean_StrIsInvalid_ReturnsFalse(string? str)
        {
            // Act
            bool actualResult = BooleanParser.TryParseBoolean(str, out _);

            // Assert
            Assert.IsFalse(actualResult);
        }

        [TestCase("false", ExpectedResult = false)]
        [TestCase("true", ExpectedResult = true)]
        public bool TryParseBoolean_StrIsValid_ReturnsFalse(string str)
        {
            // Act
            bool actualResult = BooleanParser.TryParseBoolean(str, out bool result);

            // Assert
            Assert.IsTrue(actualResult);
            return result;
        }

        [Test]
        public void ParseBoolean_StrIsNull_ThrowsArgumentNullException()
        {
            // Act
            Assert.Throws<ArgumentNullException>(() => BooleanParser.ParseBoolean(null));
        }

        [TestCase("", ExpectedResult = false)]
        public bool ParseBoolean_StrIsInvalid_ReturnsResult(string str)
        {
            // Act
            return BooleanParser.ParseBoolean(str);
        }

        [TestCase("true", ExpectedResult = true)]
        [TestCase("false", ExpectedResult = false)]
        public bool ParseBoolean_StrIsValid_ReturnsResult(string str)
        {
            // Act
            return BooleanParser.ParseBoolean(str);
        }
    }
}
