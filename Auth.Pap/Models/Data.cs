﻿using System;

namespace Auth.Pap.Models
{
    /// <summary>Pap data model.</summary>
    public class Data : IEquatable<Data>
    {
        /// <summary>Username.</summary>
        public string Username { get; set; }

        /// <summary>Password.</summary>
        public string Password { get; set; }

        /// <summary>Initialize empty data.</summary>
        public Data()
        {
        }

        /// <summary>Initialize new data, cloning the given one.</summary>
        /// <param name="data">Data to copy values from.</param>
        public Data(Data data)
        {
            Username = data.Username;
            Password = data.Password;
        }

        /// <summary>
        /// Checks if this <see cref="Data"/> is the same as the given one.
        /// </summary>
        /// <param name="other"><see cref="Data"/> to compare against.</param>
        /// <returns>True if two instances contain the same fields.</returns>
        public bool Equals(Data other) =>
            Username == other.Username && Password == other.Password;
    }
}
