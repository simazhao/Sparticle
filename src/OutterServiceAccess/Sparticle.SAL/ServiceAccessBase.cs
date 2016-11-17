using Sparticle.Common;
using Sparticle.Common.Trace;
using Sparticle.Config.MessageCollection;
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
        [ThreadStatic]
        public static IFullTrace Trace;

        private void ExceptionFail<TResult>(ApiResult<TResult> result, Exception ex, CallStep callStep)
        {
            result.ErrorCode = -1;
            result.Error = ErrorMessageCollection.SystemException;

            callStep.Exception = ex.Message;
            Trace.InnerError = ex.Message;
        }
    }
}
