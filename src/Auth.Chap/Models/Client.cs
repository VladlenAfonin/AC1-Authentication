using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Auth.Common.Extensions;

namespace Auth.Chap.Models
{
    /// <summary>Chap protocol client.</summary>
    public class Client
    {
        /// <summary>Server's nonce.</summary>
        public string Nonce { private get; set; }

        /// <summary>Digest used for authentication.</summary>
        public byte[] Digest
        {
            get
            {
                // Check if there is nonce accepted from the server.
                if (Nonce == null)
                    throw new InvalidOperationException($"No nonce provided.");

                using var sha1 = SHA1.Create();

                // Compute hash.
                var hash = sha1.ComputeHash(Encoding.ASCII.GetBytes(
                    Nonce + _credentials.Password));

                return hash;
            }
        }

        #region Two-way authentication

        /// <summary>User this server currently authenticates.</summary>
        private Credentials _currentUser;

        /// <summary>Nonce sent to the user currently authenticating.</summary>
        private string _currentNonce;

        /// <summary>Server's known users' credentials.</summary>
        private readonly List<Credentials> _knownUsers;

        #endregion

        /// <summary>Client's credentials.</summary>
        private Credentials _credentials;

        /// <summary>Initialize a new client.</summary>
        /// <param name="credentials">
        /// Client's <see cref="Credentials"/>.
        /// </param>
        public Client(Credentials credentials) =>
            _credentials = credentials;

        #region Two-way authentication

        /// <summary>
        /// Initialize a new <see cref="Client"/> for two-way authentication.
        /// </summary>
        /// <param name="credentials">
        /// This <see cref="Client"/>'s credentials.
        /// </param>
        /// <param name="knownUsers">List of known users.</param>
        public Client(Credentials credentials, List<Credentials> knownUsers)
            : this(credentials) => _knownUsers = knownUsers;

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

        #endregion
    }
}
