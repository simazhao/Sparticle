using Sparticle.Common;
using Sparticle.Config.Types;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparticle.Service.Handler.Recommand
{
    public abstract class TraceSaver
    {
        public TimeSpan ResponseTimeThreshold
        {
            get
            {
                TimeSpan threshold;
                if (!TimeSpan.TryParse(ConfigurationManager.AppSettings ["ResponseTimeThreshold"], out threshold))
                {
                    threshold = new TimeSpan(0, 0, 0, 0, 500);
                }

                return threshold;
            }
        }

        public void Save(IFullTrace trace, TraceLevel traceLevel, int allowLevel, string domain)
        {
            var saveLevel = (int)traceLevel & allowLevel;

            switch ((TraceLevel)saveLevel)
            {
                case TraceLevel.Info:
                    SaveInfo(domain, trace);
                    break;
                case TraceLevel.Error:
                    SaveError(domain, trace);
                    break;
                case TraceLevel.Warn:
                    SaveWarn(domain, trace);
                    break;
                default:
                    break;
            }

            if (trace.Elapsed > ResponseTimeThreshold)
            {
                SaveSlow(domain, trace);
            }
        }

        public abstract void SaveInfo(string domain, IFullTrace trace);

        public abstract void SaveError(string domain, IFullTrace trace);

        public abstract void SaveWarn(string domain, IFullTrace trace);

        public abstract void SaveSlow(string domain, IFullTrace trace);
    }
}
