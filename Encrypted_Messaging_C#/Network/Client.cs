using System.Net;
using System.Net.Sockets;

namespace Encrypted_Messaging_C_.Network
{
    internal class Client : IDisposable
    {

        private TcpClient _client;
        internal NetworkStream _stream { get; private set; }
        private MessageHandler _messageHandler {  get;  set; }

        public Client(string host, string port)
        {

            if (!IsValidPort(port) || !IsValidIp(host))
            {
                Console.WriteLine("Invalid IP or port.");
                Environment.Exit(0);
            }

            _client = new TcpClient(host, int.Parse(port));
            _stream = _client.GetStream();
            _messageHandler = new MessageHandler(_stream);
        }

        public async Task RunClient()
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
                if (receivedMessage == "exit") {
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
                if (messageToSend == "exit")
                {
                    break;
                }
            }
        }

        public void Dispose()
        {
            _stream?.Dispose();
            _client?.Dispose();
        }

        private static bool IsValidIp(string ip)
        {
            return IPAddress.TryParse(ip, out _) || ip == "localhost";
        }

        private static bool IsValidPort(string portString)
        {
            return int.TryParse(portString, out int port) && port >= 0 && port <= 65535;
        }

    }
}
