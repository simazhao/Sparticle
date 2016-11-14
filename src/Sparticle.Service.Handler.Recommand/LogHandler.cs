using Sparticle.Common;
using Sparticle.Result;
using Sparticle.Service.Handler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sparticle.Config.Types;

namespace Sparticle.Service.Handler.Recommand
{
    public class LogHandler : RequestHandlerBase
    {
        private readonly IDictionary<SaveLogOptionPosition, TraceSaver> _traceSavers = new Dictionary<SaveLogOptionPosition, TraceSaver>();

        public LogHandler()
        {
            InitSavers();
        }

        private void InitSavers()
        {
            _traceSavers[SaveLogOptionPosition.Text] = new LogTraceSaver();
            _traceSavers[SaveLogOptionPosition.MongoDb] = new MongoTraceSaver();
        }

        public override void BeforeHandle<TRequest>(InspectContext<TRequest> inspectCxt)
        {
            inspectCxt.Trace.Start();

            inspectCxt.Trace.LocalIp = NetWorkHelper.GetLocalIp();
            inspectCxt.Trace.Parameters = JsonHelper.ToJson(inspectCxt.Request);
        }

        public override void AfterHandle<TRequest>(InspectContext<TRequest> inspectCxt, ApiResult result)
        {
            inspectCxt.Trace.Result = result;

            inspectCxt.Trace.Stop();

            var traceLevel = GetTraceLevel(inspectCxt.Trace, result.Success);

            var option = inspectCxt.ActionConfig == null ? ActionAccessConfig.DefaultLogOption : inspectCxt.ActionConfig.LogOption;

            SaveTrace(inspectCxt.Trace, traceLevel, option, inspectCxt.Domain);
        }

        private TraceLevel GetTraceLevel(IFullTrace Trace, bool success)
        {
            if (!string.IsNullOrEmpty(Trace.Exception))
            {
                return TraceLevel.Error;
            }

            return success ? TraceLevel.Info : TraceLevel.Warn;
        }

        private static readonly char SaveOptionDelimeter = '|';
        private void SaveTrace(IFullTrace trace, TraceLevel traceLevel, string saveOption, string domain)
        {
            var options = saveOption.Split(SaveOptionDelimeter);

            for (int i = 0; i < options.Length; ++i)
            {
                SaveLogOptionPosition position = (SaveLogOptionPosition)i;

                if (_traceSavers.ContainsKey(position))
                {
                    int allowLevel = 0;
                    if (!int.TryParse(options[i], out allowLevel))
                    {
                        allowLevel = (int)TraceLevel.All;
                    }

                    _traceSavers[position].Save(trace, traceLevel, allowLevel, domain);
                }
            }
        }
    }
}
