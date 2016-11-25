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
        private ConcurrentDictionary<string, ServiceAddressBucket> _buckets = new ConcurrentDictionary<string, ServiceAddressBucket>();

        public bool Add(ServiceAddress address, string serviceIdentity)
        {
            var bucket = _buckets.GetOrAdd(serviceIdentity, (sid) => {
                return new ServiceAddressBucket(sid);
            });

            return bucket.Add(address);
        }

        private int userHighWaterMark = 21;
        public ServiceAddress GetOne(string serviceIdentity)
        {
            ServiceAddressBucket bucket;
            if (!_buckets.TryGetValue(serviceIdentity, out bucket))
                return null;

            var head = Volatile.Read(ref bucket.Head);

            ServiceAddressNode node;
            while (true)
            {
                if (bucket.AvaliableSerivces.TryGet(head, out node))
                    return null;

                var count = bucket.AvaliableSerivces.Count;
                var end = (head + count - 1) % bucket.AvaliableSerivces.Count;

                if (head == end)
                    return node;

                if ((node.AccessCount + 1)% userHighWaterMark != 0)
                {
                    Interlocked.Increment(ref node.AccessCount);
                    return node;
                }
                else
                {
                    var nexthead = (head + 1) % count;

                    IncrementHead(ref bucket.Head, head, nexthead);

                    head = bucket.Head;
                }
            }
        }

        private void IncrementHead(ref int head, int oldhead, int nexthead)
        {
            SpinWait spinwait = new SpinWait();

            while (true)
            {
                if (Interlocked.CompareExchange(ref head, nexthead, oldhead) == head)
                    break;

                spinwait.SpinOnce();
            }
        }
    }
}