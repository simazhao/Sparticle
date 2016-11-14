using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sparticle.Request.Context;
using Sparticle.Result;
using Sparticle.Config.Types;
using Sparticle.Common;
using Sparticle.Service.Interface.Example;

namespace Sparticle.Service.Example
{
    public class ExampleService : BaseService, IExampleService
    {
        public ExampleService(string apiName, string hostType)
            :base(apiName, hostType)
        {

        }

        public ApiResult Echo(string msg, RequestContext requsetContext)
        {
            return HandleRequest(nameof(Echo).ToLower(), msg, requsetContext, ExampleImpl);
        }

        public ApiResult<string> ExampleImpl(string msg, IFullTrace trace)
        {
            trace.StepTrace.AddStep("do example");

            return new ApiResult<string> { Data = "ECHO: " + msg };
        }

        protected override ActionAccessConfig RetrieveActionConfig(string actionName)
        {
            return new ActionAccessConfig() { };
        }
    }
}
