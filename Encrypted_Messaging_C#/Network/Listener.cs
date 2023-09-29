using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Encrypted_Messaging_C_.Network
{
    internal class Listener : IDisposable
    {
        internal static bool clientConnected = false;
        private TcpClient _client;
        internal NetworkStream _stream { get; private set; }
        private MessageHandler _messageHandler { get; set; }

        public async Task ListenAsync(string port)
        {
            if (!IsValidPort(port))
            {
                Console.WriteLine("Invalid IP or port.");
                Environment.Exit(0);
            }

            TcpListener listener = new TcpListener(IPAddress.Any, int.Parse(port));
            listener.Start();

            _client = await listener.AcceptTcpClientAsync();
            _stream = _client.GetStream();
            _messageHandler = new MessageHandler(_stream);
            clientConnected = true;
            listener.Stop();
        }

        public async Task RunListener()
        {
            var receiveTask = ReceiveMessagesAsync();
            var sendTask = SendMessagesAsync();
            await Task.WhenAny(receiveTask, sendTask);
        }

        public async Task ReceiveMessagesAsync()
        {
            while (true)
            {
                string receivedMessage = await _messageHandler.ReceiveMessageAsync(16);
                Console.WriteLine(receivedMessage.Length);
                Console.WriteLine($"~{receivedMessage}");
                if (receivedMessage == "exit")
                {
                    break;
                }
            }
        }

        public async Task SendMessagesAsync()
        {
            while (true)
            {
                string messageToSend = Console.ReadLine();
                await _messageHandler.SendMessageAsync(messageToSend);
            }
        }

        public void Dispose()
        {
            _stream?.Dispose();
            _client?.Dispose();
        }

        private static bool IsValidPort(string portString)
        {
            return int.TryParse(portString, out int port) && port >= 0 && port <= 65535;
        }

    }
}
