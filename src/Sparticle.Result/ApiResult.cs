using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparticle.Result
{
    public class ApiResult
    {
        public int ErrorCode { get; set; }

        public bool Success
        {
            get
            {
                return ErrorCode == 0;
            }
        }

        public string Error { get; set; }

        public void Fail(string error)
        {
            Fail(-1, error);
        }

        public void Fail(int errorCode, string error)
        {
            this.ErrorCode = ErrorCode;
            this.Error = error;
        }

        public void FailFrom(ApiResult another)
        {
            this.ErrorCode = another.ErrorCode;
            this.Error = another.Error;
        }

        public static ApiResult MakeSuccessResult()
        {
            return new ApiResult() { ErrorCode = 0 };
        }

        public static ApiResult MakeFailedResult(string error=null)
        {
            return new ApiResult { ErrorCode = -1, Error = error };
        }
    }
}
