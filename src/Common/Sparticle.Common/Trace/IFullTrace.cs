using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparticle.Common
{
    public interface IFullTrace : ITimeTrace, IRequestTrace, IServiceTrace, IResponseTrace, IExceptionTrace, IDisposable
    {
        string Id { get; set; }
    }
}
