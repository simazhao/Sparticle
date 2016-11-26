using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sparticle.ServiceKeeper.Wcf
{
    internal class BucketScan
    {
        private ServiceProbe _probe = new ServiceProbe();

        public void Scan(ServiceAddressBucket bucket)
        {
            ScanInUse(bucket);

            ScanNotUse(bucket);
        }

        private void ScanInUse(ServiceAddressBucket bucket)
        {
            ScanInternal(bucket.Avaliable, _probe.IsNotAlive, bucket.Unuse);
        }

        private void ScanNotUse(ServiceAddressBucket bucket)
        {
            ScanInternal(bucket.Unavaliable, _probe.IsAlive, bucket.Reuse);
        }

        private void ScanInternal(IEnumerable<ServiceAddressNode> nodes, Predicate<ServiceAddressNode> check, Func<ServiceAddressNode, bool> handle)
        {
            var nodesToHandle = new List<ServiceAddressNode>();

            foreach (var node in nodes)
            {
                if (check(node))
                {
                    nodesToHandle.Add(node);
                }
            }

            foreach (var node in nodesToHandle)
            {
                handle(node);
            }
        }
    }
}