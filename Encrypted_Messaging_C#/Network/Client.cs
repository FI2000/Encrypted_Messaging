using System.Net;
using System.Net.Sockets;

namespace Encrypted_Messaging_C_.Network
{
    public class Client : IDisposable, Messenger
    {

        private TcpClient _client;
        internal NetworkStream _stream { get; private set; }
        private MessageHandler _messageHandler {  get;  set; }

        public Client(string host, string port)
        {
            _client = new TcpClient(host, int.Parse(port));
            _stream = _client.GetStream();
            _messageHandler = new MessageHandler(_stream);
        }
        public IMessageHandler GetMessageHandler()
        {
            return _messageHandler;
        }

       

        public void Dispose()
        {
            _stream?.Dispose();
            _client?.Dispose();
        }

    }
}
