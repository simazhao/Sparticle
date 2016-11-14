using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparticle.Common
{
    public interface IServiceTrace
    {
        string ServiceType { get; set; }

        string LocalIp { get; set; }

        IStepTrace StepTrace { get; set; }
    }
}
