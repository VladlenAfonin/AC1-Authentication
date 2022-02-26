using System;
using System.Collections.Generic;
using System.Text.Json;
using Auth.Common;
using Auth.Pap.Models;

namespace Auth.Pap
{
    /// <summary>Protocol implementation demonstration.</summary>
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

            // Serialize data and send to channel.
            channel.Send(JsonSerializer.Serialize(client.Credentials));

            // Retrieve data from the channel and give it to the server.
            var result = server.Authenticate(
                JsonSerializer.Deserialize<Credentials>(channel.Retrieve()));

            // Print the result.
            Console.WriteLine($"Authentication result: {result}.");

            #endregion
        }
    }
}
