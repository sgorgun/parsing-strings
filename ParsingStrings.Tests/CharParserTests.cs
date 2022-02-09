using NUnit.Framework;

namespace ParsingStrings.Tests
{
    [TestFixture]
    public class CharParserTests
    {
        [TestCase(null)]
        [TestCase("")]
        [TestCase("ab")]
        public void TryParseChar_StrIsInvalid_ReturnsFalse(string? str)
        {
            // Act
            bool actualResult = CharParser.TryParseChar(str, out _);

            // Assert
            Assert.IsFalse(actualResult);
        }

        [TestCase(" ", ExpectedResult = ' ')]
        [TestCase("a", ExpectedResult = 'a')]
        [TestCase("A", ExpectedResult = 'A')]
        [TestCase("+", ExpectedResult = '+')]
        public char TryParseChar_StrIsValid_ReturnsResult(string str)
        {
            // Act
            bool actualResult = CharParser.TryParseChar(str, out char result);

            // Assert
            Assert.IsTrue(actualResult);
            return result;
        }

        [Test]
        public void ParseChar_StrIsNull_ThrowsArgumentNullException()
        {
            // Act
            Assert.Throws<ArgumentNullException>(() => CharParser.ParseChar(null));
        }

        [TestCase("", ExpectedResult = ' ')]
        [TestCase(" ", ExpectedResult = ' ')]
        [TestCase("a", ExpectedResult = 'a')]
        [TestCase("A", ExpectedResult = 'A')]
        [TestCase("ab", ExpectedResult = ' ')]
        public char ParseChar_StrIsValid_ReturnsResult(string str)
        {
            // Act
            return CharParser.ParseChar(str);
        }
    }
}
