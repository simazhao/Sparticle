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
            using (_locker.WriteLock())
            {
                if (node == null)
                    return false;

                if (_nodes.Find(item => item.Address == node.Address) != null)
                    return false;

                _nodes.Add(node);
                return true;
            }
        }

        public bool TryRemove(ServiceAddressNode node)
        {
            using (_locker.WriteLock())
            {
                return _nodes.Remove(node);
            }
        }

        public bool TryGet(int index, out ServiceAddressNode node)
        {
            node = null;

            using (_locker.ReadLock())
            {
                if (index < 0 || index >= _nodes.Count)
                    return false;

                node = _nodes[index];

                return true;
            }
        }

        public int IndexOf(ServiceAddressNode node)
        {
            using (_locker.ReadLock())
            {
                return _nodes.IndexOf(node);
            }
        }

        public ServiceAddressNode Find(Predicate<ServiceAddressNode> match)
        {
            using (_locker.ReadLock())
            {
                return _nodes.Find(match);
            }
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