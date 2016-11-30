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
        public string ServiceIdentity { get; private set; }

        private readonly ServiceAddressNodeList avaliableSerivces;

        private readonly ServiceAddressNodeList unAvaliableSerivces;

        public int Head;

        public IEnumerable<ServiceAddressNode> Avaliable
        {
            get
            {
                return avaliableSerivces;
            }
        }

        public IEnumerable<ServiceAddressNode> Unavaliable
        {
            get
            {
                return unAvaliableSerivces;
            }
        }

        public ServiceAddressBucket(string serviceIdentity)
        {
            ServiceIdentity = serviceIdentity;

            avaliableSerivces = new ServiceAddressNodeList();

            unAvaliableSerivces = new ServiceAddressNodeList();

            Head = 0;
        }

        public int AvaliableCount
        {
            get
            {
                return avaliableSerivces.Count;
            }
        }

        public int UnavaliableCount
        {
            get
            {
                return unAvaliableSerivces.Count;
            }
        }

        public bool Get(int index, out ServiceAddressNode node)
        {
            return this.avaliableSerivces.TryGet(index, out node);
        }

        public bool Exists(ServiceAddress address)
        {
            return avaliableSerivces.Find(addr => addr.Address == address.Address) != null;
        }

        public bool Add(ServiceAddress address, string ip)
        {
            return this.avaliableSerivces.TryAdd(new ServiceAddressNode(address, ip));
        }

        public bool Remove(ServiceAddressNode node)
        {
            var removed = this.avaliableSerivces.TryRemove(node);

            return removed;
        }

        public bool Remove(string ip, string address)
        {
            var removed = this.avaliableSerivces.TryRemove(node => node.MatchIp(ip) && node.MatchAddress(address));

            return removed != null;
        }

        public bool Unuse(ServiceAddressNode node)
        {
            if (Remove(node))
            {
                node.AccessCount = 0;
                return this.unAvaliableSerivces.TryAdd(node);
            }

            return false;
        }

        public bool Reuse(ServiceAddressNode node)
        {
            if (this.unAvaliableSerivces.TryRemove(node))
            {
                node.AccessCount = 0;
                return this.avaliableSerivces.TryAdd(node);
            }

            return false;
        }

        public void ClearAccess()
        {
            this.avaliableSerivces.ClearCount();
        }

        public ServiceAddressBucketModel ToModel()
        {
            var model = new ServiceAddressBucketModel();
            model.AvaliableSerivces = new List<ServiceAddressNodeModel>();
            model.UnAvaliableSerivces = new List<ServiceAddressNodeModel>();

            foreach(var node in avaliableSerivces)
            {
                model.AvaliableSerivces.Add(node.ToModel());
            }

            foreach (var node in unAvaliableSerivces)
            {
                model.UnAvaliableSerivces.Add(node.ToModel());
            }

            return model;
        }

        public void FromModel(ServiceAddressBucketModel model)
        {
            this.avaliableSerivces.FromModel(model.AvaliableSerivces);

            this.unAvaliableSerivces.FromModel(model.UnAvaliableSerivces);
        }
    }
}