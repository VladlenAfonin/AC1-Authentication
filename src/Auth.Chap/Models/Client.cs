using System;
using System.Security.Cryptography;
using System.Text;

namespace Auth.Chap.Models
{
    /// <summary>Chap Protocol client.</summary>
    public class Client
    {
        /// <summary>Client's credentials.</summary>
        private Credentials _credentials;

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

        /// <summary>Initialize a new client.</summary>
        /// <param name="credentials">
        /// Client's <see cref="Credentials"/>.
        /// </param>
        public Client(Credentials credentials) =>
            _credentials = credentials;
    }
}
