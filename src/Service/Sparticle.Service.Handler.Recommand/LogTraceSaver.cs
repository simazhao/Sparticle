using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sparticle.Common;
using Sparticle.Support.Logger;

namespace Sparticle.Service.Handler.Recommand
{
    class LogTraceSaver : TraceSaver
    {
        public override void SaveError(string domain, IFullTrace trace)
        {
            LoggerInstance.Log4net[domain, string.Empty].Error(trace);
        }

        public override void SaveInfo(string domain, IFullTrace trace)
        {
            LoggerInstance.Log4net[domain, string.Empty].Info(trace);
        }

        public override void SaveSlow(string domain, IFullTrace trace)
        {
            LoggerInstance.Log4net[domain, string.Empty].Slow(trace);
        }

        public override void SaveWarn(string domain, IFullTrace trace)
        {
            LoggerInstance.Log4net[domain, string.Empty].Warn(trace);
        }
    }
}
