using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace Sparticle.ServiceKeeper.Wcf
{
    internal class ServiceAddressNodeList 
    {
        private List<ServiceAddressNode> _nodes = new List<ServiceAddressNode>();

        private ReaderWriterLockSlim _locker = new ReaderWriterLockSlim();

        public bool TryAdd(ServiceAddressNode node)
        {
            var ret = false;
            return ret;
        }

        public bool TryRemove(ServiceAddressNode node)
        {
            return false;
        }

        public bool TryGet(int index, out ServiceAddressNode node)
        {
            node = null;

            return false;
        }

        public int IndexOf(ServiceAddressNode node)
        {
            return -1;
        }

        public ServiceAddressNode Find(Func<ServiceAddressNode, bool> judge)
        {
            return null;
        }

        public int Count
        {
            get
            {
                return _nodes.Count;
            }
        }
    }
}