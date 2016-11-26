using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace Sparticle.ServiceKeeper.Wcf
{
    internal class ServiceAddressNodeList : IEnumerable<ServiceAddressNode>
    {
        private readonly List<ServiceAddressNode> _nodes = new List<ServiceAddressNode>();

        private ReaderWriterLockSlim _locker = new ReaderWriterLockSlim();

        public IEnumerator<ServiceAddressNode> GetEnumerator()
        {
            using (_locker.ReadLock())
            {
                var nodes = new List<ServiceAddressNode>(_nodes.ToArray());
                return nodes.GetEnumerator();
            }
        }

        public bool TryAdd(ServiceAddressNode node)
        {
            using (_locker.WriteLock())
            {
                if (node == null)
                    return false;

                return DistinctAdd(node);
            }
        }

        private bool DistinctAdd(ServiceAddressNode node)
        {
            if (_nodes.Find(item => item.Address == node.Address) != null)
                return false;

            _nodes.Add(node);
            return true;
        }

        public bool TryRemove(ServiceAddressNode node)
        {
            using (_locker.WriteLock())
            {
                return _nodes.Remove(node);
            }
        }

        public void TryRemoveAt(int index)
        {
            using (_locker.WriteLock())
            {
                _nodes.RemoveAt(index);
            }
        }

        public ServiceAddressNode TryRemove(Predicate<ServiceAddressNode> match)
        {
            using (_locker.WriteLock())
            {
                ServiceAddressNode node = _nodes.Find(match);

                if (node != null)
                {
                    _nodes.Remove(node);
                }

                return node;
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

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<ServiceAddressNode>)_nodes).GetEnumerator();
        }

        public int Count
        {
            get
            {
                return _nodes.Count;
            }
        }

        public ServiceAddressNode this[int index]
        {
            get
            {
                using (_locker.ReadLock())
                {
                    return _nodes[index];
                }
            }
        }

        public void FromModel(List<ServiceAddressNodeModel> model)
        {
            using (_locker.WriteLock())
            {
                foreach(var node in model)
                {
                    if (_nodes.Find(item => item.Address == node.Address) != null)
                        continue;

                    var newnode = new ServiceAddressNode();
                    newnode.FromModel(node);
                    _nodes.Add(newnode);
                }
            }
        }
    }
}