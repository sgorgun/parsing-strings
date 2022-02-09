using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using NUnit.Framework;

namespace ParsingStrings.Tests
{
    [TestFixture]
    public sealed class FloatingPointParserTests
    {
        private static readonly object[][] TryParseDecimalData =
        {
            new object[] { "0", 0.0m },
            new object[] { "-79228162514264337593543950335", decimal.MinValue },
            new object[] { "79228162514264337593543950335", decimal.MaxValue },
            new object[] { "  -79228162514264337593543950335  ", decimal.MinValue },
        };

        private static readonly object[][] ParseDecimalData =
        {
            new object[] { string.Empty, -1.1m },
            new object[] { "abc", -1.1m },
            new object[] { "0", 0.0m },
            new object[] { "-79228162514264337593543950335", decimal.MinValue },
            new object[] { "79228162514264337593543950335", decimal.MaxValue },
            new object[] { "  -79228162514264337593543950335  ", decimal.MinValue },
            new object[] { "-79228162514264337593543950336", -2.2m },
            new object[] { "79228162514264337593543950336", -2.2m },
        };

        private CultureInfo? currentCulture;

        [OneTimeSetUp]
        public void SetupFixture()
        {
            this.currentCulture = CultureInfo.CurrentCulture;
            CultureInfo.CurrentCulture = new CultureInfo("en-us");
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            CultureInfo.CurrentCulture = this.currentCulture ?? CultureInfo.CurrentCulture;
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("abc")]
        public void TryParseFloat_StrIsInvalid_ReturnsFalse(string? str)
        {
            // Act
            bool actualResult = FloatingPointParser.TryParseFloat(str, out _);

            // Assert
            Assert.IsFalse(actualResult);
        }

        [TestCase("0", ExpectedResult = 0)]
        [TestCase("3.402823E+38", ExpectedResult = 3.402823E+38f)]
        [TestCase("-3.402823E+38", ExpectedResult = -3.402823E+38f)]
        [TestCase("3.402824E+38", ExpectedResult = float.PositiveInfinity)]
        [TestCase("-3.402824E+38", ExpectedResult = float.NegativeInfinity)]
        public float TryParseFloat_StrIsValid_ReturnsResult(string str)
        {
            // Act
            bool actualResult = FloatingPointParser.TryParseFloat(str, out float result);

            // Assert
            Assert.IsTrue(actualResult);
            return result;
        }

        [Test]
        public void ParseFloat_StrIsNull_ThrowsArgumentNullException()
        {
            // Act
            Assert.Throws<ArgumentNullException>(() => FloatingPointParser.ParseFloat(null));
        }

        [TestCase("", ExpectedResult = float.NaN)]
        [TestCase("abc", ExpectedResult = float.NaN)]
        [TestCase("0", ExpectedResult = 0)]
        [TestCase("3.402823E+38", ExpectedResult = 3.402823E+38f)]
        [TestCase("-3.402823E+38", ExpectedResult = -3.402823E+38f)]
        [TestCase("  -3.402823E+38  ", ExpectedResult = -3.402823E+38f)]
        [TestCase("3.402824E+38", ExpectedResult = float.PositiveInfinity)]
        [TestCase("-3.402824E+38", ExpectedResult = float.NegativeInfinity)]
        public float ParseFloat_StrIsValid_ReturnsResult(string value)
        {
            // Act
            return FloatingPointParser.ParseFloat(value);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("abc")]
        public void TryParseDouble_StrIsInvalid_ReturnsFalse(string? str)
        {
            // Act
            bool actualResult = FloatingPointParser.TryParseDouble(str, out _);

            // Assert
            Assert.IsFalse(actualResult);
        }

        [TestCase("0", ExpectedResult = 0)]
        [TestCase("-1.79769313486231E+308", ExpectedResult = -1.79769313486231E+308d)]
        [TestCase("1.79769313486231E+308", ExpectedResult = 1.79769313486231E+308d)]
        [TestCase("  -1.79769313486231E+308  ", ExpectedResult = -1.79769313486231E+308d)]
        public double TryParseDouble_StrIsValid_ReturnsResult(string str)
        {
            // Act
            bool actualResult = FloatingPointParser.TryParseDouble(str, out double result);

            // Assert
            Assert.IsTrue(actualResult);
            return result;
        }

        [Test]
        public void ParseDouble_StrIsNull_ThrowsArgumentNullException()
        {
            // Act
            Assert.Throws<ArgumentNullException>(() => FloatingPointParser.ParseDouble(null));
        }

        [TestCase("", ExpectedResult = double.Epsilon)]
        [TestCase("abc", ExpectedResult = double.Epsilon)]
        [TestCase("0", ExpectedResult = 0.0d)]
        [TestCase("-1.79769313486231E+308", ExpectedResult = -1.79769313486231E+308d)]
        [TestCase("1.79769313486231E+308", ExpectedResult = 1.79769313486231E+308d)]
        [TestCase("  -1.79769313486231E+308  ", ExpectedResult = -1.79769313486231E+308d)]
        [TestCase("-1.79769313486232E+308", ExpectedResult = double.NegativeInfinity)]
        [TestCase("1.79769313486232E+308", ExpectedResult = double.PositiveInfinity)]
        public double ParseDouble_StrIsValid_ReturnsResult(string value)
        {
            // Act
            return FloatingPointParser.ParseDouble(value);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("abc")]
        [TestCase("-79228162514264337593543950336")]
        [TestCase("79228162514264337593543950336")]
        public void TryParseDecimal_StrIsInvalid_ReturnsFalse(string? str)
        {
            // Act
            bool actualResult = FloatingPointParser.TryParseDecimal(str, out _);

            // Assert
            Assert.IsFalse(actualResult);
        }

        [TestCaseSource(nameof(TryParseDecimalData))]
        [SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "data can't be null.")]
        public void TryParseDecimal_StrIsValid_ReturnsResult(object[] data)
        {
            // Arrange
            string str = (string)data[0];
            decimal expectedResult = (decimal)data[1];

            // Act
            bool actualResult = FloatingPointParser.TryParseDecimal(str, out decimal result);

            // Assert
            Assert.IsTrue(actualResult);
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void ParseDecimal_StrIsNull_ThrowsArgumentNullException()
        {
            // Act
            Assert.Throws<ArgumentNullException>(() => FloatingPointParser.ParseDecimal(null));
        }

        [TestCaseSource(nameof(ParseDecimalData))]
        [SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "data can't be null.")]
        public void ParseDecimal_StrIsValid_ReturnsResult(object[] data)
        {
            // Arrange
            string str = (string)data[0];
            decimal expectedResult = (decimal)data[1];

            // Act
            decimal actualResult = FloatingPointParser.ParseDecimal(str);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
