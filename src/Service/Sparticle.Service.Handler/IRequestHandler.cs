using Sparticle.Common;
using Sparticle.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparticle.Service.Handler
{
    public interface IRequestHandler
    {
        ApiResult Handle<TRequest>(InspectContext<TRequest> inspectCxt, Func<TRequest, IFullTrace, ApiResult> finalFunc);

        IRequestHandler Next { get; set; }
    }
}
