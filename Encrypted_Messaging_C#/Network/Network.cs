using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Encrypted_Messaging_C_.Network
{
    internal static class Network
    {
        
        internal static async Task Listen(string port)
        {
            Listener listener = new Listener();
            Task listenTask = listener.ListenAsync(port);
            Console.WriteLine("Started listening...");

            while (!Listener.clientConnected)
            {
                await Task.Delay(500);
            }

            await listenTask;
            Console.WriteLine("Connected. Enter 'exit' to exit.");
            await listener.RunListener();
            Console.WriteLine("Disconnected.");
        }

        internal static async Task Connect(string ip, string port)
        {
            Client client = new Client(ip, port);
            Console.WriteLine("Connected. Enter 'exit' to exit.");
            await client.RunClient();
            Console.WriteLine("Disconnected.");
        }
    }
}
