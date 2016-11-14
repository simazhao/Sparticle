using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparticle.Result
{
    public class CachedResult<TData> : ApiResult<TData>
    {
        public bool HitCache { get; set; }

        public new static CachedResult<TData> MakeSucessResult()
        {
            return new CachedResult<TData>() { ErrorCode = 0, HitCache = true };
        }

        public new static CachedResult<TData> MakeFailedResult(string error = null)
        {
            return new CachedResult<TData>() { ErrorCode = -1, Error = error, HitCache = true };
        }

        public CachedResult<TData> Copy(ApiResult<TData> other)
        {
            this.Data = other.Data;
            this.Error = other.Error;
            this.ErrorCode = other.ErrorCode;

            return this;
        }
    }
}
