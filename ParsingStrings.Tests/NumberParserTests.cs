using NUnit.Framework;

namespace ParsingStrings.Tests
{
    [TestFixture]
    public sealed class NumberParserTests
    {
        [TestCase(null)]
        [TestCase("")]
        [TestCase("abc")]
        [TestCase("-2147483649")]
        [TestCase("2147483648")]
        public void TryParseInteger_StrIsInvalid_ReturnsFalse(string? str)
        {
            // Act
            bool actualResult = NumberParser.TryParseInteger(str, out _);

            // Assert
            Assert.IsFalse(actualResult);
        }

        [TestCase("0", ExpectedResult = 0)]
        [TestCase("-2147483648", ExpectedResult = -2_147_483_648)]
        [TestCase("2147483647", ExpectedResult = 2_147_483_647)]
        [TestCase("  -2147483648  ", ExpectedResult = -2_147_483_648)]
        public int TryParseInteger_StrIsValid_ReturnsResult(string str)
        {
            // Act
            bool actualResult = NumberParser.TryParseInteger(str, out int result);

            // Assert
            Assert.IsTrue(actualResult);
            return result;
        }

        [Test]
        public void ParseInteger_StrIsNull_ThrowsArgumentNullException()
        {
            // Act
            Assert.Throws<ArgumentNullException>(() => NumberParser.ParseInteger(null));
        }

        [TestCase("", ExpectedResult = 0)]
        [TestCase("abc", ExpectedResult = 0)]
        [TestCase("-2147483649", ExpectedResult = -1)]
        [TestCase("2147483648", ExpectedResult = -1)]
        [TestCase("0", ExpectedResult = 0)]
        [TestCase("-2147483648", ExpectedResult = -2_147_483_648)]
        [TestCase("2147483647", ExpectedResult = 2_147_483_647)]
        [TestCase("  -2147483648  ", ExpectedResult = -2_147_483_648)]
        public int ParseInteger_StrIsValid_ReturnsResult(string str)
        {
            // Act
            return NumberParser.ParseInteger(str);
        }

        [TestCase("")]
        [TestCase("abc")]
        [TestCase("-1")]
        [TestCase("4294967296")]
        public void TryParseUnsignedInteger_StrIsInvalid_ReturnsFalse(string str)
        {
            // Act
            bool actualResult = NumberParser.TryParseUnsignedInteger(str, out _);

            // Assert
            Assert.IsFalse(actualResult);
        }

        [TestCase("0", ExpectedResult = 0u)]
        [TestCase("4294967295", ExpectedResult = 4_294_967_295)]
        [TestCase("  4294967295  ", ExpectedResult = 4_294_967_295)]
        public uint TryParseUnsignedInteger_StrIsValid_ReturnsResult(string str)
        {
            // Act
            bool actualResult = NumberParser.TryParseUnsignedInteger(str, out uint result);

            // Assert
            Assert.IsTrue(actualResult);
            return result;
        }

        [Test]
        public void ParseUnsignedInteger_StrIsNull_ThrowsArgumentNullException()
        {
            // Act
            Assert.Throws<ArgumentNullException>(() => NumberParser.ParseUnsignedInteger(null));
        }

        [TestCase("", ExpectedResult = uint.MinValue)]
        [TestCase("abc", ExpectedResult = uint.MinValue)]
        [TestCase("-1", ExpectedResult = uint.MaxValue)]
        [TestCase("4294967296", ExpectedResult = uint.MaxValue)]
        [TestCase("0", ExpectedResult = 0u)]
        [TestCase("4294967295", ExpectedResult = 4_294_967_295)]
        [TestCase("  4294967295  ", ExpectedResult = 4_294_967_295)]
        public uint ParseUnsignedInteger_StrIsValid_ReturnsResult(string str)
        {
            // Act
            return NumberParser.ParseUnsignedInteger(str);
        }

        [TestCase("")]
        [TestCase("abc")]
        [TestCase("-1")]
        [TestCase("256")]
        public void TryParseByte_StrIsInvalid_ReturnsFalse(string str)
        {
            // Act
            bool actualResult = NumberParser.TryParseByte(str, out _);

            // Assert
            Assert.IsFalse(actualResult);
        }

        [TestCase("0", ExpectedResult = 0)]
        [TestCase("255", ExpectedResult = 255)]
        [TestCase("  255  ", ExpectedResult = 255)]
        public byte TryParseByte_StrIsValid_ReturnsResult(string str)
        {
            // Act
            bool actualResult = NumberParser.TryParseByte(str, out byte result);

            // Assert
            Assert.IsTrue(actualResult);
            return result;
        }

        [Test]
        public void ParseByte_StrIsNull_ThrowsArgumentNullException()
        {
            // Act
            Assert.Throws<ArgumentNullException>(() => NumberParser.ParseByte(null));
        }

        [TestCase("", ExpectedResult = byte.MaxValue)]
        [TestCase("abc", ExpectedResult = byte.MaxValue)]
        [TestCase("-1", ExpectedResult = byte.MinValue)]
        [TestCase("256", ExpectedResult = byte.MinValue)]
        [TestCase("0", ExpectedResult = 0)]
        [TestCase("255", ExpectedResult = 255)]
        [TestCase("  255  ", ExpectedResult = 255)]
        public byte ParseByte_StrIsValid_ReturnsResult(string str)
        {
            // Act
            return NumberParser.ParseByte(str);
        }

        [TestCase("")]
        [TestCase("abc")]
        [TestCase("-129")]
        [TestCase("128")]
        public void TrySignedByte_StrIsInvalid_ReturnsFalse(string str)
        {
            // Act
            bool actualResult = NumberParser.TrySignedByte(str, out _);

            // Assert
            Assert.IsFalse(actualResult);
        }

        [TestCase("0", ExpectedResult = 0)]
        [TestCase("-128", ExpectedResult = -128)]
        [TestCase("127", ExpectedResult = 127)]
        [TestCase("  -128  ", ExpectedResult = -128)]
        public sbyte TrySignedByte_StrIsValid_ReturnsResult(string str)
        {
            // Act
            bool actualResult = NumberParser.TrySignedByte(str, out sbyte result);

            // Assert
            Assert.IsTrue(actualResult);
            return result;
        }

        [Test]
        public void ParseSignedByte_StrIsNull_ThrowsArgumentNullException()
        {
            // Act
            Assert.Throws<ArgumentNullException>(() => NumberParser.ParseSignedByte(null));
        }

        [TestCase("-129")]
        [TestCase("128")]
        public void ParseSignedByte_StrIsNull_ThrowsException(string str)
        {
            // Act
            Assert.Throws<OverflowException>(() => NumberParser.ParseSignedByte(str));
        }

        [TestCase("", ExpectedResult = sbyte.MaxValue)]
        [TestCase("abc", ExpectedResult = sbyte.MaxValue)]
        [TestCase("0", ExpectedResult = 0)]
        [TestCase("-128", ExpectedResult = -128)]
        [TestCase("127", ExpectedResult = 127)]
        [TestCase("  -128  ", ExpectedResult = -128)]
        public sbyte ParseSignedByte_StrIsValid_ReturnsResult(string str)
        {
            // Act
            return NumberParser.ParseSignedByte(str);
        }

        [TestCase("")]
        [TestCase("abc")]
        [TestCase("-32769")]
        [TestCase("32768")]
        public void TryParseShort_StrIsInvalid_ReturnsFalse(string str)
        {
            // Act
            bool actualResult = NumberParser.TryParseShort(str, out _);

            // Assert
            Assert.IsFalse(actualResult);
        }

        [TestCase("0", ExpectedResult = 0)]
        [TestCase("-32768", ExpectedResult = -32_768)]
        [TestCase("32767", ExpectedResult = 32_767)]
        [TestCase("  -32768  ", ExpectedResult = -32_768)]
        public short TryParseShort_StrIsValid_ReturnsResult(string str)
        {
            // Act
            bool actualResult = NumberParser.TryParseShort(str, out short result);

            // Assert
            Assert.IsTrue(actualResult);
            return result;
        }

        [Test]
        public void ParseShort_StrIsNull_ThrowsArgumentNullException()
        {
            // Act
            Assert.Throws<ArgumentNullException>(() => NumberParser.ParseShort(null));
        }

        [TestCase("")]
        [TestCase("abc")]
        public void ParseShort_StrIsNull_ThrowsException(string str)
        {
            // Act
            Assert.Throws<FormatException>(() => NumberParser.ParseShort(str));
        }

        [TestCase("0", ExpectedResult = 0)]
        [TestCase("-32768", ExpectedResult = -32_768)]
        [TestCase("32767", ExpectedResult = 32_767)]
        [TestCase("  -32768  ", ExpectedResult = -32_768)]
        public short ParseShort_StrIsValid_ReturnsResult(string str)
        {
            // Act
            return NumberParser.ParseShort(str);
        }

        [TestCase("")]
        [TestCase("abc")]
        [TestCase("-1")]
        [TestCase("65536")]
        public void TryParseUnsignedShort_StrIsInvalid_ReturnsFalse(string str)
        {
            // Act
            bool actualResult = NumberParser.TryParseUnsignedShort(str, out _);

            // Assert
            Assert.IsFalse(actualResult);
        }

        [TestCase("0", ExpectedResult = 0u)]
        [TestCase("65535", ExpectedResult = 65_535u)]
        [TestCase("  65535  ", ExpectedResult = 65_535u)]
        public ushort TryParseUnsignedShort_StrIsValid_ReturnsResult(string str)
        {
            // Act
            bool actualResult = NumberParser.TryParseUnsignedShort(str, out ushort result);

            // Assert
            Assert.IsTrue(actualResult);
            return result;
        }

        [Test]
        public void ParseUnsignedShort_StrIsNull_ThrowsArgumentNullException()
        {
            // Act
            Assert.Throws<ArgumentNullException>(() => NumberParser.ParseUnsignedShort(null));
        }

        [TestCase("0", ExpectedResult = 0u)]
        [TestCase("", ExpectedResult = 0u)]
        [TestCase("abc", ExpectedResult = 0u)]
        [TestCase("-1", ExpectedResult = ushort.MaxValue)]
        [TestCase("65536", ExpectedResult = ushort.MaxValue)]
        [TestCase("65535", ExpectedResult = 65_535u)]
        [TestCase("  65535  ", ExpectedResult = 65_535u)]
        public ushort ParseUnsignedShort_StrIsValid_ReturnsResult(string str)
        {
            // Act
            return NumberParser.ParseUnsignedShort(str);
        }

        [TestCase("")]
        [TestCase("abc")]
        [TestCase("-9223372036854775809")]
        [TestCase("9223372036854775808")]
        public void TryParseLong_StrIsInvalid_ReturnsFalse(string str)
        {
            // Act
            bool actualResult = NumberParser.TryParseLong(str, out _);

            // Assert
            Assert.IsFalse(actualResult);
        }

        [TestCase("0", ExpectedResult = 0)]
        [TestCase("-9223372036854775808", ExpectedResult = -9_223_372_036_854_775_808)]
        [TestCase("9223372036854775807", ExpectedResult = 9_223_372_036_854_775_807)]
        [TestCase("  -9223372036854775808  ", ExpectedResult = -9_223_372_036_854_775_808)]
        public long TryParseLong_StrIsValid_ReturnsResult(string str)
        {
            // Act
            bool actualResult = NumberParser.TryParseLong(str, out long result);

            // Assert
            Assert.IsTrue(actualResult);
            return result;
        }

        [Test]
        public void ParseLong_StrIsNull_ThrowsArgumentNullException()
        {
            // Act
            Assert.Throws<ArgumentNullException>(() => NumberParser.ParseLong(null));
        }

        [TestCase("", ExpectedResult = long.MinValue)]
        [TestCase("abc", ExpectedResult = long.MinValue)]
        [TestCase("0", ExpectedResult = 0L)]
        [TestCase("-9223372036854775808", ExpectedResult = -9_223_372_036_854_775_808)]
        [TestCase("9223372036854775807", ExpectedResult = 9_223_372_036_854_775_807)]
        [TestCase("  -9223372036854775808  ", ExpectedResult = -9_223_372_036_854_775_808)]
        [TestCase("-9223372036854775809", ExpectedResult = -1)]
        [TestCase("9223372036854775808", ExpectedResult = -1)]
        public long ParseLong_StrIsValid_ReturnsResult(string str)
        {
            // Act
            return NumberParser.ParseLong(str);
        }

        [TestCase("")]
        [TestCase("abc")]
        [TestCase("-1")]
        [TestCase("18446744073709551616")]
        public void TryParseUnsignedLong_StrIsInvalid_ReturnsFalse(string str)
        {
            // Act
            bool actualResult = NumberParser.TryParseUnsignedLong(str, out _);

            // Assert
            Assert.IsFalse(actualResult);
        }

        [TestCase("0", ExpectedResult = 0u)]
        [TestCase("18446744073709551615", ExpectedResult = 18_446_744_073_709_551_615u)]
        [TestCase("  18446744073709551615  ", ExpectedResult = 18_446_744_073_709_551_615u)]
        public ulong TryParseUnsignedLong_StrIsValid_ReturnsResult(string str)
        {
            // Act
            bool actualResult = NumberParser.TryParseUnsignedLong(str, out ulong result);

            // Assert
            Assert.IsTrue(actualResult);
            return result;
        }

        [Test]
        public void TryParseUnsignedLong_StrIsNull_ThrowsArgumentNullException()
        {
            // Act
            Assert.Throws<ArgumentNullException>(() => NumberParser.ParseUnsignedLong(null));
        }

        [TestCase("")]
        [TestCase("abc")]
        public void ParseUnsignedLong_StrIsNull_ThrowsFormatException(string str)
        {
            // Act
            Assert.Throws<FormatException>(() => NumberParser.ParseUnsignedLong(str));
        }

        [TestCase("-1")]
        [TestCase("18446744073709551616")]
        public void ParseUnsignedLong_StrIsNull_ThrowsOverflowException(string str)
        {
            // Act
            Assert.Throws<OverflowException>(() => NumberParser.ParseUnsignedLong(str));
        }

        [TestCase("0", ExpectedResult = 0ul)]
        [TestCase("18446744073709551615", ExpectedResult = 18_446_744_073_709_551_615u)]
        [TestCase("  18446744073709551615  ", ExpectedResult = 18_446_744_073_709_551_615ul)]
        public ulong ParseUnsignedLong_StrIsValid_ReturnsResult(string str)
        {
            // Act
            return NumberParser.ParseUnsignedLong(str);
        }
    }
}
