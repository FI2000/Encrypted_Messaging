using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Encrypted_Messaging_C_
{
    internal class MessageHandler
    {

        private readonly NetworkStream _stream;

        public MessageHandler(NetworkStream stream)
        {
            _stream = stream ?? throw new ArgumentNullException(nameof(stream));
        }

        public async Task SendMessageAsync(string message)
        {
            if (string.IsNullOrEmpty(message)) return;

            byte[] messageBytes = Encoding.UTF8.GetBytes(message);
            await _stream.WriteAsync(messageBytes, 0, messageBytes.Length);
        }

        public async Task<string> ReceiveMessageAsync(int bufferSize)
        {
            byte[] buffer = new byte[bufferSize];
            int bytesRead = await _stream.ReadAsync(buffer, 0, bufferSize);

            return Encoding.UTF8.GetString(buffer, 0, bytesRead);
        }

    }
}
