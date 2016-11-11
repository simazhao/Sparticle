using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparticle.Common
{
    public interface IStep
    {
        DateTime BeginTime { get; set; }

        string Message { get; set; }
    }

    public interface ICallStep : IStep
    {
        DateTime EndTime { get; set; }

        TimeSpan Elapsed { get;}

        void Start();

        void Stop();
    }
}
