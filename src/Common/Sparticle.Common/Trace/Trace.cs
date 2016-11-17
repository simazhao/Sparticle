using Sparticle.Request.Context;
using Sparticle.Result;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparticle.Common.Trace
{
    public class Trace : IFullTrace
    {
        #region field
        private DateTime beginTime;
        private DateTime endTime;
        private Stopwatch stopWatch = new Stopwatch();
        private string exception;
        private string _id;
        #endregion

        public string Id
        {
            get
            {
                return _id ?? (_id = DateTime.Now.ToUniversalTime().Ticks
                                + "-" + new Random().Next(1000000));
            }

            set { _id = value; }
        }

        DateTime ITimeTrace.BeginTime { get { return beginTime; } }

        DateTime ITimeTrace.EndTime { get { return endTime; } }

        TimeSpan ITimeTrace.Elapsed { get { return stopWatch.Elapsed; } }


        public void Start()
        {
            beginTime = DateTime.Now;
            stopWatch.Start();
        }

        public void Stop()
        {
            stopWatch.Stop();
            endTime = DateTime.Now;            
        }

        public RequestContext RequestContext { get; set; }

        public IRequestWithTokenTrace RequestToken { get; set; }

        public string User { get; set; }

        public string Method { get; set; }

        public string Parameters { get; set; }

        public string ApiName { get; set; }

        public string ServiceType { get; set; }

        public string LocalIp { get; set; }

        private IStepTrace steptrace = new StepTrace();
        IStepTrace IServiceTrace.StepTrace { get { return steptrace; } set { steptrace = value; } }

        public string Token { get; set; }

        public ApiResult Result { get; set; }

        public string InnerError { get; set; }

        public string Exception { get { return exception; } }

        void IExceptionTrace.SetException(Exception exp)
        {
            exception = exp.DetailMessage();
        }

        public override string ToString()
        {
            var sb = StringBuilderFactory.Instance.GetStringBuilder();

            sb.AppendFormat("Id:{0}\r\n", Id);
            sb.AppendFormat("Time: {0} - {1}\r\n", beginTime.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                endTime.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            sb.AppendFormat("Elapsed: {0}\r\n", stopWatch.Elapsed.ToString());

            var device = string.Empty;
            if (RequestContext != null && this.RequestContext.Device != null)
            {
                device = JsonHelper.ToJson(this.RequestContext.Device);
            }
            sb.AppendFormat("Source:{0}\r\n", device);

            var channel = string.Empty;
            if (RequestContext != null && this.RequestContext.Channel != null)
            {
                channel = JsonHelper.ToJson(this.RequestContext.Channel);
            }
            sb.AppendFormat("Channel: {0}\r\n", channel);

            sb.AppendFormat("User: {0}\r\n", User);
            sb.AppendFormat("Token: {0}\r\n", Token);
            sb.AppendFormat("Method: {0} on {1}\r\n", Method, User);
            sb.AppendFormat("Parameters: {0}\r\n", Parameters);

            sb.AppendFormat("Result: {0}\r\n", Result != null ? JsonHelper.ToJson(Result) : null);

            if (!string.IsNullOrEmpty(Exception))
            {
                sb.AppendFormat("Exception: {0}\r\n", Exception);
            }

            if (!string.IsNullOrEmpty(InnerError))
            {
                sb.AppendFormat("InnerError: {0}\r\n", InnerError);
            }

            var steps = this.steptrace.Steps;

            if (steps.Count > 0)
            {
                sb.AppendFormat("Step: {0}", steps.First().ToString());

                foreach (var traceStepInfo in steps.Skip(1))
                {
                    sb.AppendFormat("\r\nStep: {0}", traceStepInfo.ToString());
                }
            }

            return StringBuilderFactory.Instance.GetStringAndReleaseBuilder(sb);
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    steptrace = null;
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        ~Trace()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(false);
        }

        // This code added to correctly implement the disposable pattern.
        void IDisposable.Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
