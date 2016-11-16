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
    partial class ServiceAccessBase
    {
        public ApiResult<TResult> TryCallV3<TResult>(Func<TResult> func)
        {
            return TryCallV3<int, int, int, int, int, int, int, int, int, TResult>(
                (t01, t02, t03, t04, t05, t06, t07, t08, t09) => { return func(); },
                 1, 1, 1, 1, 1, 1, 1, 1, 1, func.Method.Name);
        }

        public ApiResult<TResult> TryCallV3<T1, TResult>(Func<T1, TResult> func, T1 t1)
        {
            return TryCallV3<T1, int, int, int, int, int, int, int, int, TResult>(
                (t01, t02, t03, t04, t05, t06, t07, t08, t09) => { return func(t01); },
                 t1, 1, 1, 1, 1, 1, 1, 1, 1, func.Method.Name);
        }

        public ApiResult<TResult> TryCallV3<T1, T2, TResult>(Func<T1, T2, TResult> func, T1 t1, T2 t2)
        {
            return TryCallV3<T1, T2, int, int, int, int, int, int, int, TResult>(
                (t01, t02, t03, t04, t05, t06, t07, t08, t09) => { return func(t01, t02); },
                 t1, t2, 1, 1, 1, 1, 1, 1, 1, func.Method.Name);
        }

        public ApiResult<TResult> TryCallV3<T1, T2, T3, TResult>(Func<T1, T2, T3, TResult> func, T1 t1, T2 t2, T3 t3)
        {
            return TryCallV3<T1, T2, T3, int, int, int, int, int, int, TResult>(
                (t01, t02, t03, t04, t05, t06, t07, t08, t09) => { return func(t01, t02, t03); },
                 t1, t2, t3, 1, 1, 1, 1, 1, 1, func.Method.Name);
        }

        public ApiResult<TResult> TryCallV3<T1, T2, T3, T4, TResult>(Func<T1, T2, T3, T4, TResult> func, T1 t1, T2 t2, T3 t3, T4 t4)
        {
            return TryCallV3<T1, T2, T3, T4, int, int, int, int, int, TResult>(
                (t01, t02, t03, t04, t05, t06, t07, t08, t09) => { return func(t01, t02, t03, t04); },
                 t1, t2, t3, t4, 1, 1, 1, 1, 1, func.Method.Name);
        }

        public ApiResult<TResult> TryCallV3<T1, T2, T3, T4, T5, TResult>(Func<T1, T2, T3, T4, T5, TResult> func, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5)
        {
            return TryCallV3<T1, T2, T3, T4, T5, int, int, int, int, TResult>(
                (t01, t02, t03, t04, t05, t06, t07, t08, t09) => { return func(t01, t02, t03, t04, t05); },
                 t1, t2, t3, t4, t5, 1, 1, 1, 1, func.Method.Name);
        }

        public ApiResult<TResult> TryCallV3<T1, T2, T3, T4, T5, T6, TResult>(Func<T1, T2, T3, T4, T5, T6, TResult> func, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6)
        {
            return TryCallV3<T1, T2, T3, T4, T5, T6, int, int, int, TResult>(
                (t01, t02, t03, t04, t05, t06, t07, t08, t09) => { return func(t01, t02, t03, t04, t05, t06); },
                 t1, t2, t3, t4, t5, t6, 1, 1, 1, func.Method.Name);
        }

        public ApiResult<TResult> TryCallV3<T1, T2, T3, T4, T5, T6, T7, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, TResult> func
            , T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7)
        {
            return TryCallV3<T1, T2, T3, T4, T5, T6, T7, int, int, TResult>(
                (t01, t02, t03, t04, t05, t06, t07, t08, t09) => { return func(t01, t02, t03, t04, t05, t06, t07); },
                 t1, t2, t3, t4, t5, t6, t7, 1, 1, func.Method.Name);
        }

        public ApiResult<TResult> TryCallV3<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> func
            , T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8)
        {
            return TryCallV3<T1, T2, T3, T4, T5, T6, T7, T8, int, TResult>(
                (t01, t02, t03, t04, t05, t06, t07, t08, t09) => { return func(t01, t02, t03, t04, t05, t06, t07, t08); },
                 t1, t2, t3, t4, t5, t6, t7, t8, 1, func.Method.Name);
        }

        public ApiResult<TResult> TryCallV3<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> func
         , T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9)
        {
            return TryCallV3<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(
                (t01, t02, t03, t04, t05, t06, t07, t08, t09) => { return func(t01, t02, t03, t04, t05, t06, t07, t08, t09); },
                 t1, t2, t3, t4, t5, t6, t7, t8, t9, func.Method.Name);
        }

        public ApiResult<TResult> TryCallV3<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> func,
            T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, string methodName)
        {
            var callStep = new CallStep();
            callStep.Parameters = StringHelper.GetArgsInfo(t1, t2, t3, t4, t5, t6, t7, t8, t9, 0);
            callStep.Start(methodName);

            var apiResult = ApiResult<TResult>.MakeSucessResult();
            try
            {
                TResult result = func(t1, t2, t3, t4, t5, t6, t7, t8, t9);
                apiResult.Data = result;
            }
            catch (Exception ex)
            {
                ExceptionFail(apiResult, ex, callStep);
            }

            callStep.Stop(apiResult, Trace.StepTrace.SerializeResult);

            if (Trace != null)
            {
                Trace.StepTrace.AddStep(callStep);
            }

            return apiResult;
        }
    }
}
