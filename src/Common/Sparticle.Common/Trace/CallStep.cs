using Sparticle.Result;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparticle.Common.Trace
{
    public class CallStep : StepBase
    {
        private readonly Stopwatch _stopwatch = new Stopwatch();

        DateTime EndTime { get; set; }

        TimeSpan Elapsed { get; set; }

        public string Method { get; set; }

        public string Parameters { get; set; }

        public bool Cached { get; set; }

        public string Result { get; set; }

        public void Start(string method)
        {
            Method = method;
            BeginTime = DateTime.Now;

            _stopwatch.Restart();
        }

        public void Stop<TData>(ApiResult<TData> result, bool serializeResult = false)
        {
            bool cached = false;
            if (result is CachedResult<TData>)
            {
                cached = (result as CachedResult<TData>).HitCache;
            }

            Stop(result, cached, serializeResult);
        }

        private void Stop<TData>(ApiResult<TData> result, bool cached, bool serializeResult = false)
        {
            _stopwatch.Stop();
            EndTime = DateTime.Now;
            Elapsed = _stopwatch.Elapsed;

            if (result.Success)
            {
                Message = "Success";
            }
            else
            {
                Message = string.Format("[failed：{0}]", result.Error);
            }

            Cached = cached;

            if (serializeResult)
            {
                Result = JsonHelper.ToJson(result);
            }
            else
            {
                Result = "--";
            }
        }

        public override string ToString()
        {
            return string.Format("{0} - {1}, Call:{2}, Time:{3}, Op：{4}, Params:{5}, Result：{6}, Cached: {7}。",
                BeginTime.ToString("yyyy-MM-dd HH:mm:ss.fff"), EndTime.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                Method, Elapsed, Message, Parameters, Result, Cached);
        }
    }
}
