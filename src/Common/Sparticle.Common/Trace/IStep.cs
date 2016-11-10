using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparticle.Common
{
    interface IStep
    {
        DateTime BeginTime { get; set; }

        string Message { get; set; }
    }

    interface ICallStep : IStep
    {
        DateTime EndTime { get; set; }

        TimeSpan Elapsed { get;}

        void Start();

        void Stop();
    }
}
