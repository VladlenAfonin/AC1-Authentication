using System;
using Xunit;

namespace Auth.Common.Tests
{
    public class ChannelTests
    {
        [Fact]
        public void Send_NoDataInChannel_AddsMessageToChannel()
        {
            var message = "message text";
            var channel = new Channel();

            channel.Send(message);

            Assert.Equal(message, channel.StoredMessage);
        }

        [Fact]
        public void Send_AlreadyDataInChannel_ThrowsException()
        {
            var message = "message text";
            var channel = new Channel(message);

            Assert.Throws<InvalidOperationException>(
                () => channel.Send(message));
        }

        [Fact]
        public void Retrieve_AlreadyDataInChannel_GetsMessageAndDeletesItFromChannel()
        {
            var message = "message text";
            var channel = new Channel(message);

            var messageRetrieved = channel.Retrieve();

            Assert.Equal(message, messageRetrieved);
            Assert.Null(channel.StoredMessage);
        }

        [Fact]
        public void Retrieve_NoDataInChannel_ThrowsException()
        {
            var channel = new Channel();

            Assert.Throws<InvalidOperationException>(
                () => channel.Retrieve());
        }
    }
}
