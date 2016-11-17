using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

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

        public static string GetClientIP()
        {
            string result = String.Empty;

            result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (StringHelper.IsNullOrEmtpyString(result))
            {
                result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }

            if (StringHelper.IsNullOrEmtpyString(result))
            {
                result = HttpContext.Current.Request.UserHostAddress;
            }

            if (StringHelper.IsNullOrEmtpyString(result) || !IsIP(result))
            {
                return "127.0.0.1";
            }

            return result;
        }

        public static bool IsIP(string ip)
        {
            return Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }
    }
}
