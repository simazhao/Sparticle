using Sparticle.ServiceKeeper.Interface;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace Sparticle.ServiceKeeper.Wcf
{
    internal class ServiceAddressPool
    {
        private readonly ConcurrentDictionary<string, ServiceAddressBucket> _buckets = new ConcurrentDictionary<string, ServiceAddressBucket>();

        public static ServiceAddressPool Instance = new ServiceAddressPool();

        public IEnumerator<KeyValuePair<string, ServiceAddressBucket>> GetEnumerator()
        {
            return _buckets.GetEnumerator();
        } 

        public bool Add(ServiceAddress address, string serviceIdentity, string ip)
        {
            var bucket = _buckets.GetOrAdd(serviceIdentity, (sid) => {
                return new ServiceAddressBucket(sid);
            });

            return bucket.Add(address, ip);
        }

        public bool Remove(string serviceIndentity, string ip, string address)
        {
            ServiceAddressBucket bucket;
            if (!_buckets.TryGetValue(serviceIndentity, out bucket))
                return false;

            return bucket.Remove(ip, address);
        }

        public ServiceAddressNode GetOne(string serviceIdentity)
        {
            if (string.IsNullOrWhiteSpace(serviceIdentity))
                return null;

            ServiceAddressBucket bucket;
            if (!_buckets.TryGetValue(serviceIdentity, out bucket))
                return null;

            var head = Volatile.Read(ref bucket.Head);

            ServiceAddressNode node = null;
            while (true)
            {
                if (!bucket.Get(head, out node))
                    break;

                if (node.TryAccess())
                    break;

                node.EnsureNextAccess();

                var count = bucket.AvaliableCount;

                var nexthead = (head + 1) % count;

                if (IncrementHead(ref bucket.Head, head, nexthead))
                {
                    head = bucket.Head;
                }
                else
                {
                    head = Volatile.Read(ref bucket.Head);
                }
            }

            return node;
        }

        private bool IncreaseWaterMark(ref int waterMark, int oldWaterMark, int add)
        {
            return (Interlocked.CompareExchange(ref waterMark, oldWaterMark + add, oldWaterMark) == oldWaterMark);
        }

        private bool IncrementHead(ref int head, int oldhead, int nexthead)
        {
            return (Interlocked.CompareExchange(ref head, nexthead, oldhead) == oldhead);
        }

        public ServiceAddressPoolModel ToModel()
        {
            var model = new ServiceAddressPoolModel();
            model.Buckets = new Dictionary<string, ServiceAddressBucketModel>();

            foreach (var kv in this._buckets)
            {
                var bucket = kv.Value.ToModel();

                model.Buckets.Add(kv.Key, bucket);
            }

            return model;
        }

        public void FromModel(ServiceAddressPoolModel model)
        {
            foreach(var kv in model.Buckets)
            {
                var bucket = new ServiceAddressBucket(kv.Key);
                bucket.FromModel(kv.Value);
                this._buckets.TryAdd(kv.Key, bucket);
            }
        }
    }
}