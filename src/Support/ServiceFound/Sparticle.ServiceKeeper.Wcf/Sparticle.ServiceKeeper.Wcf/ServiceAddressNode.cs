using Sparticle.ServiceKeeper.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sparticle.ServiceKeeper.Wcf
{
    internal class ServiceAddressNode : ServiceAddress
    {
        public int AccessCount = 0;

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

        public bool Match(string ip)
        {
            return string.Compare(this.ClientIp, ip) == 0;
        }

        public bool MatchIp(ServiceAddressNode another)
        {
            return Match(another.ClientIp);
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