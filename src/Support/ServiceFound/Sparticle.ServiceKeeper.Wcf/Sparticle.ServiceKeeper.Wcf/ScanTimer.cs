using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace Sparticle.ServiceKeeper.Wcf
{
    internal class ScanTimer
    {
        private Timer _timer;
        private TimeSpan dueTime;
        private TimeSpan period;

        public ScanTimer(TimeSpan dueTime, TimeSpan period)
        {
            _timer = new Timer(ScanOnTime, ServiceAddressPool.Instance, dueTime, period);
        }

        private void ScanOnTime(object state)
        {
            var pool = state as ServiceAddressPool;

            if (pool == null)
                return;

            _timer.Change(int.MaxValue, int.MaxValue);

            var scan = new PoolScan();

            scan.Scan(pool);

            _timer.Change(period, period);
        }
    }
}