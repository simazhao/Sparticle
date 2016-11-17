using Sparticle.Common;
using Sparticle.Config.MessageCollection;
using Sparticle.Result;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparticle.Service.Handler.Recommand
{
    [Export(typeof(IRequestHandler))]
    public class ExceptionHandler : IRequestHandler
    {
        public IRequestHandler Next { get; set; }

        public ApiResult Handle<TRequest>(InspectContext<TRequest> inspectCxt, Func<TRequest, IFullTrace, ApiResult> finalFunc)
        {
            if (Next != null)
            {
                try
                {
                    return Next.Handle(inspectCxt, finalFunc);
                }
                catch (Exception ex)
                {
                    inspectCxt.Trace.SetException(ex);

                    return ApiResult.MakeFailedResult(ErrorMessageCollection.SystemException);
                }
            }

            return ApiResult.MakeFailedResult(ErrorMessageCollection.SystemInnerError);
        }
    }
}
