using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sparticle.Common;
using Sparticle.Result;

namespace Sparticle.Service.Handler
{
    internal class TailRequstHandler : RequestHandlerBase
    {
        public override void BeforeHandle<TRequest>(InspectContext<TRequest> inspectCxt)
        {

        }

        public override void AfterHandle<TRequest>(InspectContext<TRequest> inspectCxt, ApiResult result)
        {

        }

        protected override ApiResult HandleImpl<TRequest>(InspectContext<TRequest> inspectCxt, Func<TRequest, IFullTrace, ApiResult> finalFunc)
        {
            return finalFunc(inspectCxt.Request, inspectCxt.Trace);
        }

        public override IRequestHandler Next
        {
            get { return null; }
            set { }
        }
    }
}
