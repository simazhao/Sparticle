using Sparticle.Common;
using Sparticle.Config.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparticle.Service.Handler.Recommand
{
    public abstract class TraceSaver
    {
        public void Save(IFullTrace Trace, TraceLevel traceLevel, int allowLevel, string domain)
        {
            var saveLevel = (int)traceLevel & allowLevel;

            switch ((TraceLevel)saveLevel)
            {
                case TraceLevel.Info:
                    SaveInfo(domain, Trace);
                    break;
                case TraceLevel.Error:
                    SaveError(domain, Trace);
                    break;
                case TraceLevel.Warn:
                    SaveWarn(domain, Trace);
                    break;
                default:
                    break;
            }
        }

        public abstract void SaveInfo(string domain, IFullTrace traceInfo);
        public abstract void SaveError(string domain, IFullTrace traceInfo);
        public abstract void SaveWarn(string domain, IFullTrace traceInfo);
    }
}
