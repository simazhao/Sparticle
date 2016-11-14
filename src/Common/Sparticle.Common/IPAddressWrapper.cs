using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace Sparticle.Common
{
    public class IPAddressWrapper : IPAddress
    {
        public IPAddressWrapper(byte[] address)
            : base(address)
        {
            IpV4AddressLong = GetIpV4AddressLong(address);
        }

        public IPAddressWrapper(IPAddress address)
            : base(address.GetAddressBytes())
        {
            IpV4AddressLong = GetIpV4AddressLong(GetAddressBytes());
        }

        public long IpV4AddressLong { get; private set; }

        private long GetIpV4AddressLong(byte[] bytes)
        {
            long sum = bytes[3];
            long rate = 255;

            for (int i = 2; i >= 0; --i)
            {
                sum += rate * bytes[i];
                rate *= 255;
            }

            return sum;
        }
    }

    public class IPAddressRange
    {
        private IPAddressWrapper _begin;
        private IPAddressWrapper _end;

        public IPAddressRange(IPAddress begin, IPAddress end)
        {
            _begin = new IPAddressWrapper(begin);
            _end = new IPAddressWrapper(end);
        }

        public IPAddressRange(string begin, string end)
        {
            _begin = new IPAddressWrapper(IPAddress.Parse(begin));
            _end = new IPAddressWrapper(IPAddress.Parse(end));
        }

        public IPAddressWrapper Begin
        {
            get { return _begin; }
        }

        public IPAddressWrapper End
        {
            get { return _end; }
        }

        public bool IsInIpV4Range(IPAddressWrapper mid)
        {
            return Begin.IpV4AddressLong < mid.IpV4AddressLong && mid.IpV4AddressLong < End.IpV4AddressLong;
        }

        private static IPAddressRange _aPrivateClass = new IPAddressRange("10.0.0.0", "10.255.255.255");
        public static IPAddressRange APrivateClass { get { return _aPrivateClass; } }

        private static IPAddressRange _aReservedClass = new IPAddressRange("127.0.0.0", "127.255.255.255");
        public static IPAddressRange AReservClass { get { return _aReservedClass; } }

        private static IPAddressRange _bPrivateClass = new IPAddressRange("172.16.0.0", "172.31.255.255");
        public static IPAddressRange BPrivateClass { get { return _bPrivateClass; } }

        private static IPAddressRange _bReservedClass = new IPAddressRange("169.254.0.0", "169.254.255.255");
        public static IPAddressRange BReservClass { get { return _bReservedClass; } }

        private static IPAddressRange _cPrivateClass = new IPAddressRange("192.168.0.0", "192.168.255.255");
        public static IPAddressRange CPrivateClass { get { return _cPrivateClass; } }
    }
}
