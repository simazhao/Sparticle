using Sparticle.Common;
using Sparticle.Request;
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
    public class RequestCheckHandler : RequestHandlerBase
    {
        public override void BeforeHandle<TRequest>(InspectContext<TRequest> inspectCxt)
        {
            inspectCxt.Trace.StepTrace.AddStep("begin check param");
        }

        public override void AfterHandle<TRequest>(InspectContext<TRequest> inspectCxt, ApiResult result)
        {
            // do nothing
        }

        protected override ApiResult HandleImpl<TRequest>(InspectContext<TRequest> inspectCxt,
            Func<TRequest, IFullTrace, ApiResult> finalFunc)
        {
            string error;
            bool pass = CheckRequest(inspectCxt.Request, out error);

            inspectCxt.Trace.StepTrace.AddStep(string.Format("end check param [{0}] {1}", pass ? "passed" : "not pass", error));

            if (!pass)
            {
                return ApiResult.MakeFailedResult(error);
            }

            return base.HandleImpl(inspectCxt, finalFunc);
        }

        protected bool CheckRequest<TRequest>(TRequest request, out string error)
        {
            error = string.Empty;

            var selfCheck = request as ISelfCheck;
            if (selfCheck != null)
            {
                return selfCheck.Check(out error);
            }

            return true;
        }
    }
}
