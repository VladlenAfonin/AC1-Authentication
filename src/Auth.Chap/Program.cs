using System;
using System.Collections.Generic;
using System.Text.Json;
using Auth.Chap.Models;
using Auth.Common;

namespace Auth.Chap
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Preparations

            // Create a channel able to store one message at a time.
            var channel = new Channel();

            // Create valid and invalid data.
            var validData = new Credentials
            {
                Username = "vladlen",
                Password = "vladlensPassword"
            };

            var invalidData = new Credentials
            {
                Username = "notVladlen",
                Password = "notVladlensPassword"
            };

            // Initialize server with valid data as known data.
            var server = new Server(new List<Credentials> { validData });

            // Initialize client. We may change it's parameters for
            // demonstration.
            var client = new Client(validData);

            #endregion

            #region Protocol execution

            // Server sends the request. No need to serialize a string.
            channel.Send(server.SendAuthenticationRequest(validData));

            // Client retrieves.
            client.Nonce = channel.Retrieve();

            // Client send's it's digest.
            channel.Send(JsonSerializer.Serialize(client.Digest));

            // Server authenticates.
            var result = server.Authenticate(
                JsonSerializer.Deserialize<byte[]>(channel.Retrieve()));

            Console.WriteLine($"Authentication result: {result}.");

            #endregion
        }
    }
}
