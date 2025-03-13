using System.Net;

namespace ChatWP_P2P.Helpers
{
    public class NetworkHelper
    {

        public static string GetBroadcastAddress(IPAddress ip, IPAddress subnetMask)
        {

            var ipBytes = ip.GetAddressBytes();
            var maskBytes = subnetMask.GetAddressBytes();
            var broadcastBytes = new byte[ipBytes.Length];

            for (var i = 0; i < ipBytes.Length; i++)
            {
                broadcastBytes[i] = (byte)(ipBytes[i] | (maskBytes[i] ^ 255));
            }

            return new IPAddress(broadcastBytes).ToString();
        }
    }
}
