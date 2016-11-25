using Sparticle.ServiceKeeper.Interface;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace Sparticle.ServiceKeeper.Wcf
{
    internal class ServiceAddressBucket
    {
        public string ServiceIdentity { get; set; }
    
        public ServiceAddressNodeList AvaliableSerivces { get; set; }

        public ServiceAddressNodeList UnAvaliableSerivces { get; set; }

        public int Head;

        public ServiceAddressBucket(string serviceIdentity)
        {
            ServiceIdentity = serviceIdentity;

            AvaliableSerivces = new ServiceAddressNodeList();

            UnAvaliableSerivces = new ServiceAddressNodeList();

            Head = 0;
        }

        public bool Exists(ServiceAddress address)
        {
            return AvaliableSerivces.Find(addr => addr.Address == address.Address) != null;
        }

        public bool Add(ServiceAddress address)
        {
            return this.AvaliableSerivces.TryAdd(new ServiceAddressNode(address));
        }

        public bool Remove(ServiceAddressNode node)
        {
            return this.AvaliableSerivces.TryRemove(node);
        }

        public bool Unuse(ServiceAddressNode node)
        {
            if (Remove(node))
            {
                return this.UnAvaliableSerivces.TryAdd(node);
            }

            return false;
        }
    }

    



}