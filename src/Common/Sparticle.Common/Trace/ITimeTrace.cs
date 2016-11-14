using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparticle.Common
{
    public interface ITimeTrace
    {
        DateTime BeginTime { get; }

        DateTime EndTime { get;  }

        TimeSpan Elapsed { get; }

        void Start();

        void Stop();
    }
}
