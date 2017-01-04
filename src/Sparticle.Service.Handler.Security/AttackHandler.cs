using Sparticle.Common;
using Sparticle.Request;
using Sparticle.Result;
using Sparticle.Security.Attack;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparticle.Service.Handler.Security
{
    [Export(typeof(IRequestHandler))]
    public class AttackHandler : RequestHandlerBase
    {
        public override void AfterHandle<TRequest>(InspectContext<TRequest> inspectCxt, Sparticle.Result.ApiResult result)
        {
            
        }

        public override void BeforeHandle<TRequest>(InspectContext<TRequest> inspectCxt)
        {
            inspectCxt.Trace.StepTrace.AddStep("begin detecting attack");
        }

        protected override ApiResult HandleImpl<TRequest>(InspectContext<TRequest> inspectCxt,
                Func<TRequest, IFullTrace, ApiResult> finalFunc)
        {
            var request = inspectCxt.Request as WithTokenRequest;
            if (request != null)
            {
                var algoName = SelectAlgo(request.SelectANo);

                var attackDefender = new ReplayAttackDefender(new AuthKeyContainer());

                var raParam = new ReplayAttackParam();
                string error;
                if (!attackDefender.Defend(raParam, algoName, out error))
                {
                    return ApiResult.MakeFailedResult(error);
                }
            }

            return base.HandleImpl(inspectCxt, finalFunc);
        }

        private String SelectAlgo(string algoNo)
        {
            throw new NotImplementedException();
        }
    }
}
