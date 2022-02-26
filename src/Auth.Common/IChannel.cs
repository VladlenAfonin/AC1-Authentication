namespace Auth.Common
{
    /// <summary>Channel abstraction.</summary>
    public interface IChannel
    {
        /// <summary>Send data to the channel.</summary>
        /// <param name="message">Message to send.</param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if channel is not empty when pushing to it.
        /// </exception>
        public void Send(string message);

        /// <summary>Retrieve information from the channel.</summary>
        /// <returns>Data inside channel.</returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if channel is empty when retrieving from it.
        /// </exception>
        public string Retrieve();
    }
}
