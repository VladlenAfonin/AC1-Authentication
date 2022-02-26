using System.Collections.Generic;

namespace Auth.Pap.Models
{
    public class Server
    {
        private readonly List<Data> _knownUsers;

        /// <summary>Initialize a new server.</summary>
        /// <param name="knownUsers">List of known users' credentials.</param>
        public Server(List<Data> knownUsers) =>
            _knownUsers = knownUsers;

        /// <summary>Authenticate user provided it's data.</summary>
        /// <param name="data">User's data.</param>
        /// <returns>True if authentication is successful.</returns>
        public bool Authenticate(Data data) =>
            _knownUsers.Contains(data);
    }
}
