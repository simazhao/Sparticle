using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sparticle.ServiceKeeper.Wcf
{
    internal class PoolScan
    {
        private BucketScan _scan = new BucketScan();

        public void Scan(ServiceAddressPool pool)
        {
            foreach (var kv in pool)
            {
                _scan.Scan(kv.Value);
            }
        }
    }
}