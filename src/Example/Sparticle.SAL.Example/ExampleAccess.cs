using Sparticle.OutterService.Wcf.Example;
using Sparticle.OutterService.Wcf.Example.Interface;
using Sparticle.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparticle.SAL.Example
{
    public class ExampleAccess : ServiceAccessBase
    {
        public ApiResult<int> Add(int num1, int num2)
        {
            using (var svc = new ExampleServiceWrapper().GetTService<ICaculator>())
            {
                var calcResult = TryCall(svc.Svc.Add, num1, num2);

                if (!calcResult.Success)
                {
                    return ApiResult<int>.MakeFailedResult(calcResult.Error);
                }

                return ApiResult<int>.MakeSuccessResult(calcResult.Data.Number);
            }
        }

        public ApiResult<int> Div(int num1, int num2)
        {
            using (var svc = new ExampleServiceWrapper().GetTService<ICaculator>())
            {
                var calcResult = TryCall(svc.Svc.Div, num1, num2);

                if (!calcResult.Success)
                {
                    return ApiResult<int>.MakeFailedResult(calcResult.Error);
                }

                return ApiResult<int>.MakeSuccessResult(calcResult.Data.Number);
            }
        }
    }
}
