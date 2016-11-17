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

        public new static ApiResult<TData> MakeSuccessResult()
        {
            return new ApiResult<TData>() { ErrorCode = 0 };
        }

        public static ApiResult<TData> MakeSuccessResult(TData data)
        {
            return new ApiResult<TData>() { ErrorCode = 0, Data = data };
        }


        public new static ApiResult<TData> MakeFailedResult(string Message = null)
        {
            return new ApiResult<TData>() { ErrorCode = -1, Error = Message };
        }
    }
}
