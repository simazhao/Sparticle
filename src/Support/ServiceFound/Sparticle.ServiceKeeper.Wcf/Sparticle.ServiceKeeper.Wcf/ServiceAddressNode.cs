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

        public ServiceAddressNode()
        {

        }

        public ServiceAddressNode(ServiceAddress address)
        {
            this.Address = address.Address;
            this.PropertyList = address.PropertyList;
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