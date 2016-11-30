using Sparticle.ServiceKeeper.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace Sparticle.ServiceKeeper.Wcf
{
    internal class ServiceAddressNode : ServiceAddress
    {
        private const int waterMarkStep = 20;
        private const int userHighWaterMark = waterMarkStep * 10000000;
        private volatile int userLowWaterMark = waterMarkStep;

        public volatile int AccessCount = 0;

        private string ClientIp;

        public ServiceAddressNode()
        {

        }

        public ServiceAddressNode(ServiceAddress address, string ip)
        {
            this.Address = address.Address;
            this.PropertyList = address.PropertyList;
            this.ClientIp = ip;
        }

        public ServiceAddress CopyBase()
        {
            return new ServiceAddress()
            {
                Address = this.Address,
                PropertyList = this.PropertyList,
            };
        }

        public bool MatchIp(string ip)
        {
            return string.Compare(this.ClientIp, ip) == 0;
        }

        public bool MatchAddress(string address)
        {
            return string.Compare(Address, address, true) == 0;
        }

        public bool Match(ServiceAddressNode another)
        {
            return MatchAddress(another.Address);
            //return MatchIp(another.ClientIp) || MatchAddress(another.Address);
        }

        public ServiceAddressNodeModel ToModel()
        {
            return new ServiceAddressNodeModel()
            {
                Address = this.Address,
                ClientIp = this.ClientIp,
                PropertyList = this.PropertyList,
            };
        }

        public void FromModel(ServiceAddressNodeModel model)
        {
            this.Address = model.Address;
            this.ClientIp = model.ClientIp;
            this.PropertyList = model.PropertyList;
        }

        public bool TryAccess()
        {
            int waterMark = userLowWaterMark;
            var accesscount = AccessCount;

            if ((accesscount + 1) % (waterMark + 1) != 0)
            {
                return Interlocked.CompareExchange(ref AccessCount, accesscount + 1, accesscount) == accesscount;
            }

            return false;
        }

        public void EnsureNextAccess()
        {
            int waterMark = userLowWaterMark;

            do
            {
                if ((userLowWaterMark + waterMarkStep) > userHighWaterMark)
                {
                    userLowWaterMark = waterMarkStep;
                    AccessCount = 0;
                    break;
                }

                Interlocked.CompareExchange(ref userLowWaterMark, waterMark + waterMarkStep, waterMark);
                waterMark = userLowWaterMark;
            } while (false);
        }
    }

    internal class ServiceAddressNodeEqualityComparer : IEqualityComparer<ServiceAddressNode>
    {
        bool IEqualityComparer<ServiceAddressNode>.Equals(ServiceAddressNode x, ServiceAddressNode y)
        {
            if (x.Equals(y))
                return true;

            return string.Compare(GetKey(x), GetKey(y)) == 0;
        }

        int IEqualityComparer<ServiceAddressNode>.GetHashCode(ServiceAddressNode obj)
        {
            return GetKey(obj).GetHashCode();
        }

        string GetKey(ServiceAddressNode x)
        {
            return x.Address;
        }
    }
}