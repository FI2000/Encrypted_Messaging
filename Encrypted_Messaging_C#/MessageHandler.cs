using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Encrypted_Messaging_C_
{
    public interface IMessageHandler
    {
        Task SendMessageWithLengthAsync(string message);
        Task<string> ReceiveMessageWithLengthAsync();
    }

    public class MessageHandler : IMessageHandler
    {

        private readonly NetworkStream _stream;

        public MessageHandler(NetworkStream stream)
        {
            _stream = stream ?? throw new ArgumentNullException(nameof(stream));
        }

        public async Task SendMessageWithLengthAsync(string message)
        {
            byte[] messageBytes = Encoding.UTF8.GetBytes(message);
            byte[] lengthBytes = BitConverter.GetBytes(messageBytes.Length);

            await _stream.WriteAsync(lengthBytes, 0, 4);
            await _stream.WriteAsync(messageBytes, 0, messageBytes.Length);
        }

        public async Task<string> ReceiveMessageWithLengthAsync()
        {
            byte[] lengthBytes = new byte[4];
            await _stream.ReadAsync(lengthBytes, 0, 4);
            int messageLength = BitConverter.ToInt32(lengthBytes, 0);

            byte[] messageBytes = new byte[messageLength];
            await _stream.ReadAsync(messageBytes, 0, messageLength);

            return Encoding.UTF8.GetString(messageBytes);
        }

    }
}
