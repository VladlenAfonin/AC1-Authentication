using System;

namespace Auth.Common
{
    /// <summary>
    /// Channel implementation. It holds only one message at a time.
    /// </summary>
    public class Channel : IChannel
    {
        /// <summary>Channel's stored message.</summary>
        public string StoredMessage { get; private set; }

        /// <summary>Initialize a new channel.</summary>
        /// <param name="storedMessage">Message to store.</param>
        public Channel(string storedMessage = null) =>
            StoredMessage = storedMessage;

        public void Send(string message)
        {
            if (StoredMessage != null)
                throw new InvalidOperationException(
                    $"Channel is not empty.");

            StoredMessage = message;
        }

        public string Retrieve()
        {
            if (StoredMessage == null)
                throw new InvalidOperationException(
                    $"Channel is empty.");

            var temp = new string(StoredMessage);

            StoredMessage = null;

            return temp;
        }
    }
}
