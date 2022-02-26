using Xunit;

namespace Auth.Common.Extensions.Tests
{
    public class ByteArrayExtensionsTests
    {
        [Theory]
        [InlineData(
            new byte[] { 0x00, 0x00, 0x00 },
            new byte[] { 0x00, 0x00, 0x00 },
            true)]
        [InlineData(
            new byte[] { 0x00, 0x00, 0x00 },
            new byte[] { 0x00, 0x01, 0x00 },
            false)]
        [InlineData(
            new byte[] { 0x00, 0x00 },
            new byte[] { 0x00, 0x00, 0x00 },
            false)]
        public void EqualsTo_VariousArrays_ReturnsExpected(
            byte[] array, byte[] anotherArray, bool expected)
        {
            var result = array.EqualsTo(anotherArray);

            Assert.Equal(expected, result);
        }
    }
}
