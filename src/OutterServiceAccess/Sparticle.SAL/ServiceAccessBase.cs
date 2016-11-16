using Sparticle.Common;
using Sparticle.Common.Trace;
using Sparticle.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparticle.SAL
{
    public abstract partial class ServiceAccessBase
    {
        public static readonly string ServiceHasError = "服务器开小差了";

        [ThreadStatic]
        public static IFullTrace Trace;

        private void ExceptionFail<TResult>(ApiResult<TResult> result, Exception ex, CallStep callStep)
        {
            result.ErrorCode = -1;
            result.Error = ServiceHasError;

            callStep.Exception = ex.Message;
        }
    }
}
