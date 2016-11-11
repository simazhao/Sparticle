using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparticle.Result
{
    public class ApiResult<TData> : ApiResult
    {
        public TData Data { get; set; }

        public static ApiResult<TData> MakeSucessResult()
        {
            return new ApiResult<TData>() { ErrorCode = 0 };
        }
            
        public new static ApiResult<TData> MakeFailedResult(string Message = null)
        {
            return new ApiResult<TData>() { ErrorCode = -1, Error = Message };
        }
    }
}
