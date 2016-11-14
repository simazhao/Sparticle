using Sparticle.Request.Context;
using Sparticle.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparticle.Service.Interface.Example
{
    public interface IExampleService
    {
        ApiResult Echo(string msg, RequestContext requsetContext);
    }
}
