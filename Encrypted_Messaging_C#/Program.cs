using System.Net;
using System.Runtime.CompilerServices;
using Encrypted_Messaging_C_.Network;
namespace Encrypted_Messaging_C_
{
    internal class Program
    {

        static async Task Main(string[] args)
        {
            await CheckRole(args);
        }

        private static async Task CheckRole(string[] args)
        {
            if (args.Length > 0 && args[0] == "listener")
            {
                Console.WriteLine("Enter port ");
                string? port = Console.ReadLine();

                await Network.Network.Listen(port);
            }
            else if (args.Length > 0 && args[0] == "client")
            {
                Console.WriteLine("Enter IP ");
                string? ipInput = Console.ReadLine();

                Console.WriteLine("Enter port ");
                string? port = Console.ReadLine();

                Network.Network.Connect(ipInput, port);
            }
        }
    }
}