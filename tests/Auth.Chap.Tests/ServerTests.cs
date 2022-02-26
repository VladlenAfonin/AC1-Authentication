using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Auth.Chap.Models;
using Xunit;

namespace Auth.Chap.Tests
{
    public class ServerTests
    {
        [Fact]
        public void Authenticate_ValidCredentials_ReturnsTrue()
        {
            var data = new Credentials
            {
                Username = "username",
                Password = "password"
            };

            var server = new Server(new List<Credentials> { data });

            var nonce = server.SendAuthenticationRequest(data);

            using var sha1 = SHA1.Create();

            var hash = sha1.ComputeHash(Encoding.ASCII.GetBytes(
                nonce + data.Password));

            var result = server.Authenticate(hash);

            Assert.True(result);
        }

        [Fact]
        public void Authenticate_InValidCredentials_ReturnsFalse()
        {
            var data = new Credentials
            {
                Username = "username",
                Password = "password"
            };

            var incorrectPassword = "incorrectPassword";

            var server = new Server(new List<Credentials> { data });

            var nonce = server.SendAuthenticationRequest(data);

            using var sha1 = SHA1.Create();

            var hash = sha1.ComputeHash(Encoding.ASCII.GetBytes(
                nonce + incorrectPassword));

            var result = server.Authenticate(hash);

            Assert.False(result);
        }
    }
}
