using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Auth.Common.Extensions;

namespace Auth.Chap.Models
{
    /// <summary>Chap <see cref="Server"/>.</summary>
    public class Server
    {
        /// <summary>User this server currently authenticates.</summary>
        private Credentials _currentUser;

        /// <summary>Nonce sent to the user currently authenticating.</summary>
        private string _currentNonce;

        /// <summary>Server's known users' credentials.</summary>
        private readonly List<Credentials> _knownUsers;

        /// <summary>Initialize a new server.</summary>
        /// <param name="knownUsers">List of known users' credentials.</param>
        public Server(List<Credentials> knownUsers) =>
            _knownUsers = knownUsers;

        /// <summary>Authenticate user provided it's data.</summary>
        /// <param name="data">User's data.</param>
        /// <returns>True if authentication is successful.</returns>
        public bool Authenticate(byte[] receivedDigest)
        {
            using var sha1 = SHA1.Create();

            var computedDigest = sha1.ComputeHash(Encoding.ASCII.GetBytes(
                _currentNonce + _currentUser.Password));

            return computedDigest.EqualsTo(receivedDigest);
        }

        /// <summary>
        /// Generates and returns nonce connected to the user given.
        /// </summary>
        /// <param name="credentials">
        /// <see cref="Credentials"/> of a user to authenticate.
        /// </param>
        /// <returns>Generated nonce.</returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if there's no given user in the database.
        /// </exception>
        public string SendAuthenticationRequest(Credentials credentials)
        {
            if (!_knownUsers.Contains(credentials))
                throw new InvalidOperationException(
                    $"No such user in the database.");

            _currentUser = credentials;

            _currentNonce = RandomNumberGenerator
                .GetInt32(int.MaxValue).ToString();

            return _currentNonce;
        }
    }
}
