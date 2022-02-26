namespace Auth.Pap.Models
{
    public class Client
    {
        /// <summary>Client's credentials.</summary>
        public Credentials Credentials { get; }

        /// <summary>Initialize a new client.</summary>
        /// <param name="credentials">Client's credentials.</param>
        public Client(Credentials credentials) =>
            Credentials = credentials;
    }
}
