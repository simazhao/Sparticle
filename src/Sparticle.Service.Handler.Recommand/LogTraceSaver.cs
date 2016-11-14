using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sparticle.Common;

namespace Sparticle.Service.Handler.Recommand
{
    class LogTraceSaver : TraceSaver
    {
        public override void SaveError(string domain, IFullTrace traceInfo)
        {
            throw new NotImplementedException();
        }

        public override void SaveInfo(string domain, IFullTrace traceInfo)
        {
            throw new NotImplementedException();
        }

        public override void SaveWarn(string domain, IFullTrace traceInfo)
        {
            throw new NotImplementedException();
        }
    }
}
