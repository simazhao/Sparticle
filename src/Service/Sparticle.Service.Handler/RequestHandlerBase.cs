using Sparticle.Common;
using Sparticle.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparticle.Service.Handler
{
    public abstract class RequestHandlerBase : IRequestHandler
    {
        public abstract void BeforeHandle<TRequest>(InspectContext<TRequest> inspectCxt);

        public ApiResult Handle<TRequest>(InspectContext<TRequest> inspectCxt, Func<TRequest, IFullTrace, ApiResult> finalFunc)
        {
            BeforeHandle(inspectCxt);

            var result = HandleImpl(inspectCxt, finalFunc);

            AfterHandle(inspectCxt, result);

            return result;
        }

        private static readonly string SystemInnerError = "system inner error, no proper handler";
        protected virtual ApiResult HandleImpl<TRequest>(InspectContext<TRequest> inspectCxt, Func<TRequest, IFullTrace, ApiResult> finalFunc)
        {
            if (Next != null)
            {
                return Next.Handle(inspectCxt, finalFunc);
            }

            return ApiResult.MakeFailedResult(SystemInnerError);
        }

        public abstract void AfterHandle<TRequest>(InspectContext<TRequest> inspectCxt, ApiResult result);

        public virtual IRequestHandler Next { get; set; }
    }
}
