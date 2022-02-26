using System;

namespace Auth.Pap.Models
{
    /// <summary>Pap data model.</summary>
    public class Credentials : IEquatable<Credentials>
    {
        /// <summary>Username.</summary>
        public string Username { get; set; }

        /// <summary>Password.</summary>
        public string Password { get; set; }

        /// <summary>Initialize empty <see cref="Credentials"/>.</summary>
        public Credentials()
        {
        }

        /// <summary>Initialize new data, cloning the given one.</summary>
        /// <param name="data">
        /// <see cref="Credentials"/> to copy values from.
        /// </param>
        public Credentials(Credentials data)
        {
            Username = data.Username;
            Password = data.Password;
        }

        /// <summary>
        /// Checks if this <see cref="Credentials"/> is the same as the given
        /// one.
        /// </summary>
        /// <param name="other">
        /// <see cref="Credentials"/> to compare against.
        /// </param>
        /// <returns>True if two instances contain the same fields.</returns>
        public bool Equals(Credentials other) =>
            Username == other.Username && Password == other.Password;
    }
}
