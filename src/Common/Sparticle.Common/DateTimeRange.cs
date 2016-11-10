using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparticle.Common
{
    public class DateTimeRange
    {
        private DateTime beginTime;

        private DateTime endTime;

        public DateTimeRange(DateTime beginTime, DateTime endTime)
        {
            this.beginTime = beginTime;
            this.endTime = endTime;
        }

        public bool HasBegin(DateTime time)
        {
            return beginTime <= time && time <= endTime;
        }

        public bool HasBegin(long timestamp)
        {
            return beginTime.Ticks <= timestamp && timestamp <= endTime.Ticks;
        }
    }
}
