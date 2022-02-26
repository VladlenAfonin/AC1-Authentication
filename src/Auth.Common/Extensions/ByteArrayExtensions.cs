namespace Auth.Common.Extensions
{
    /// <summary><see cref="byte"/> array extensions.</summary>
    public static class ByteArrayExtensions
    {
        /// <summary>Compares two <see cref="byte"/> arrays.</summary>
        /// <param name="value">This <see cref="byte"/> array.</param>
        /// <param name="anotherArray">
        /// <see cref="byte"/> array to compare against.
        /// </param>
        /// <returns>True if two arrays are equal.</returns>
        public static bool EqualsTo(this byte[] value, byte[] anotherArray)
        {
            var length = value.Length;

            if (length != anotherArray.Length)
                return false;

            int result = 0;

            // This way it's constant time for a given length.
            for (int i = 0; i < length; i++)
                result |= value[i] ^ anotherArray[i];

            return result == 0;
        }
    }
}
