using System.Collections.Generic;
using Auth.Pap.Models;
using Xunit;

namespace Auth.Pap.Tests
{
    public class ServerTests
    {
        [Fact]
        public void Authenticate_ValidCredentials_ReturnsTrue()
        {
            var data = new Data
            {
                Username = "username",
                Password = "password"
            };

            var anotherData = new Data(data);

            var server = new Server(new List<Data> { data });

            var result = server.Authenticate(anotherData);

            Assert.True(result);
        }

        [Fact]
        public void Authenticate_InvalidCredentials_ReturnsFalse()
        {
            var data = new Data
            {
                Username = "username",
                Password = "password"
            };

            var anotherData = new Data
            {
                Username = "anotherUsername",
                Password = "anotherPassword"
            };

            var server = new Server(new List<Data> { anotherData });

            var result = server.Authenticate(data);

            Assert.False(result);
        }
    }
}
