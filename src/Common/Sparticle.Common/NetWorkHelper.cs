using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Sparticle.Common
{
    public static class NetWorkHelper
    {
        public static string GetLocalIp()
        {
            IPHostEntry ipEntry = Dns.GetHostEntry(Dns.GetHostName());
            string ip = string.Empty;
            string first = string.Empty;
            foreach (IPAddress ipa in ipEntry.AddressList)
            {
                if (ipa.AddressFamily == AddressFamily.InterNetwork)
                {
                    // get first ipv4 address
                    if (string.IsNullOrEmpty(first))
                    {
                        first = ipa.ToString();
                    }

                    // ignore local address
                    if (!IsLocalAddress(ipa))
                    {
                        ip = ipa.ToString();
                        break;
                    }
                }
            }

            // if not, get first address
            if (string.IsNullOrEmpty(first))
            {
                first = ipEntry.AddressList[0].ToString();
            }

            return string.IsNullOrEmpty(ip) ? first : ip;
        }

        public static bool IsLocalAddress(IPAddress address)
        {
            var addressW = new IPAddressWrapper(address);

            return IPAddressRange.APrivateClass.IsInIpV4Range(addressW) ||
                   IPAddressRange.AReservClass.IsInIpV4Range(addressW) ||
                   IPAddressRange.BPrivateClass.IsInIpV4Range(addressW) ||
                   IPAddressRange.BReservClass.IsInIpV4Range(addressW) ||
                   IPAddressRange.CPrivateClass.IsInIpV4Range(addressW);
        }
    }
}
