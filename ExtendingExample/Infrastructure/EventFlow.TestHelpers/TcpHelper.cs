using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace EventFlow.TestHelpers
{
    public class TcpHelper
    {
        private static readonly Random Random = new Random();

        public static int GetFreePort()
        {
            var ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();
            var activeTcpListeners = ipGlobalProperties.GetActiveTcpListeners();
            var ports = new HashSet<int>(activeTcpListeners.Select(p => p.Port));

            while (true)
            {
                var port = Random.Next(10000, 60000);
                if (!ports.Contains(port))
                {
                    return port;
                }
            }
        }
    }
}
