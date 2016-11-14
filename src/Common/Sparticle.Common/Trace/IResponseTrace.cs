using Sparticle.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparticle.Common
{
    public interface IResponseTrace
    {
        string Token { get; set; }

        ApiResult Result { get; set; }
    }
}
