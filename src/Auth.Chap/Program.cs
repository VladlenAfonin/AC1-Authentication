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
            // Use this parameter to switch between one-way and two-way
            // authentication.
            var twoWayAuthentication = true;

            if (!twoWayAuthentication)
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

                // Client sends it's digest.
                channel.Send(JsonSerializer.Serialize(client.Digest));

                // Server authenticates.
                var result = server.Authenticate(
                JsonSerializer.Deserialize<byte[]>(channel.Retrieve()));

                // Write authentication result to the console.
                Console.WriteLine($"Authentication result: {result}.");

                #endregion
            }
            else
            {
                #region Preparations

                // Create a channel able to store one message at a time.
                var channel = new Channel();

                // Create valid and invalid data.
                var clientValid = new Credentials
                {
                    Username = "client",
                    Password = "clientsPassword"
                };

                var clientInvalid = new Credentials
                {
                    Username = "client",
                    Password = "notClientsPassword"
                };

                var serverValid = new Credentials
                {
                    Username = "server",
                    Password = "serversPassword"
                };

                var serverInvalid = new Credentials
                {
                    Username = "server",
                    Password = "notServersPassword"
                };

                // Initialize server with valid data as known data.
                var server = new Server(
                    serverValid,
                    new List<Credentials> { clientValid });

                // Initialize client. We may change it's parameters for
                // demonstration.
                var client = new Client(
                    clientValid,
                    new List<Credentials> { serverValid });

                #endregion

                #region Protocol execution

                // Server sends the request.No need to serialize a string.
                channel.Send(server.SendAuthenticationRequest(clientValid));

                // Client retrieves.
                client.Nonce = channel.Retrieve();

                // Client sends it's digest and nonce.
                channel.Send(JsonSerializer.Serialize(Tuple.Create(
                    client.Digest,
                    client.SendAuthenticationRequest(serverValid))));

                // Server authenticates client and accepts it's nonce.
                var data = JsonSerializer
                    .Deserialize<Tuple<byte[], string>>(channel.Retrieve());
                
                var resultClient = server.Authenticate(data.Item1);
                server.Nonce = data.Item2;

                channel.Send(JsonSerializer.Serialize(server.Digest));

                var resultServer = client.Authenticate(
                    JsonSerializer.Deserialize<byte[]>(channel.Retrieve()));

                Console.WriteLine(
                    $"Client authentication result: {resultClient}");
                Console.WriteLine(
                    $"Server authentication result: {resultServer}");

                #endregion
            }

        }
    }
}
