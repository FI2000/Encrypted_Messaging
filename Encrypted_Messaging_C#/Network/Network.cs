using System.Net;

namespace Encrypted_Messaging_C_.Network
{
    public static class Network
    {
        
        public static bool IsValidIp(string ip)
        {
            return IPAddress.TryParse(ip, out _) || ip == "localhost";
        }

        public static bool IsValidPort(string portString)
        {
            return int.TryParse(portString, out int port) && port >= 0 && port <= 65535;
        }
    }
}
