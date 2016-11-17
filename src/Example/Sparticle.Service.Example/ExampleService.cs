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
using Sparticle.DTO.Example;
using Sparticle.SAL.Example;
using Sparticle.Request;

namespace Sparticle.Service.Example
{
    public class ExampleService : BaseService, IExampleService
    {
        private ExampleAccess access = new ExampleAccess();

        public ExampleService(string apiName, string hostType)
            :base(apiName, hostType)
        {
            Domain = "Example";
        }

        public ApiResult Echo(string msg, RequestContext requsetContext)
        {
            return HandleRequest(nameof(Echo).ToLower(), msg, requsetContext, EchoImpl);
        }

        public ApiResult<string> EchoImpl(string msg, IFullTrace trace)
        {
            trace.StepTrace.AddStep("do example");

            return new ApiResult<string> { Data = "ECHO: " + msg };
        }

        public ApiResult Add(AddRequest request, RequestContext requsetContext)
        {
            return HandleRequest(nameof(Add).ToLower(), request, requsetContext, AddImpl);
        }

        public ApiResult<int> AddImpl(AddRequest request, IFullTrace trace)
        {
            var result = access.Add(request.Num1, request.Num2);

            return result;
        }

        public ApiResult Div(DivRequest request, RequestContext requsetContext)
        {
            return HandleRequest(nameof(Add).ToLower(), request, requsetContext, DivImpl);
        }

        public ApiResult<int> DivImpl(DivRequest request, IFullTrace trace)
        {
            var result = access.Div(request.Divider, request.Dividend);

            return result;
        }

        protected override ActionAccessConfig RetrieveActionConfig(string actionName)
        {
            // just hard code to only save text log, but read from config in product.
            return new ActionAccessConfig() {
                LogOption = string.Format("{0}", ((int)TraceLevel.All).ToString()),

                ServiceDebug = true,
            };
        }

        public ApiResult MakeRandom(MakeRandomIntRequest request, RequestContext requsetContext)
        {
            return HandleRequest(nameof(MakeRandom).ToLower(), request, requsetContext, StoreImpl);
        }

        public ApiResult<int> StoreImpl(MakeRandomIntRequest request, IFullTrace trace)
        {
            return ApiResult<int>.MakeSuccessResult().Copy(CachedData.MakeRandom(request.WhoAreYou, request.Seconds));
        }
    }
}
