using System.Collections.Generic;

namespace Auth.Pap.Models
{
    public class Server
    {
        /// <summary>Server's known users' credentials.</summary>
        private readonly List<Credentials> _knownUsers;

        /// <summary>Initialize a new server.</summary>
        /// <param name="knownUsers">List of known users' credentials.</param>
        public Server(List<Credentials> knownUsers) =>
            _knownUsers = knownUsers;

        /// <summary>Authenticate user provided it's data.</summary>
        /// <param name="data">User's data.</param>
        /// <returns>True if authentication is successful.</returns>
        public bool Authenticate(Credentials data) =>
            _knownUsers.Contains(data);
    }
}
