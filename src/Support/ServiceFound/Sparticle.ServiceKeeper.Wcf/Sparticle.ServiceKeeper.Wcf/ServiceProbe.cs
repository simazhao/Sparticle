using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sparticle.ServiceKeeper.Wcf
{
    internal class ServiceProbe
    {
        const int _1seconds = 1000;
        const int _30seconds = 30 * _1seconds;
        const int _1Minutes = 2 * _30seconds;
        public bool IsAlive(ServiceAddressNode node)
        {
            var host = GetHost(node.Address);

            var tcpProbe = new TcpProbe();

            var alive = tcpProbe.CanConnect(host.Host, host.Port, _30seconds);

            return alive;
        }

        public bool IsNotAlive(ServiceAddressNode node)
        {
            return !IsAlive(node);
        }

        private Uri GetHost(string address)
        {
            var uri = new Uri(address);

            return uri;
        }
    }
}