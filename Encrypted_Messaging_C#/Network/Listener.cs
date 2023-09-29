using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Encrypted_Messaging_C_.Network
{
    public class Listener : IDisposable, Messenger
    {
        public static bool clientConnected = false;
        private TcpClient _client;
        internal NetworkStream _stream { get; private set; }
        private MessageHandler _messageHandler { get; set; }

        public async Task ListenAsync(string port)
        {
       
            TcpListener listener = new TcpListener(IPAddress.Any, int.Parse(port));
            listener.Start();

            _client = await listener.AcceptTcpClientAsync();
            _stream = _client.GetStream();
            _messageHandler = new MessageHandler(_stream);
            clientConnected = true;
            listener.Stop();
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
