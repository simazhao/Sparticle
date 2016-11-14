using Sparticle.Common;
using Sparticle.Config.Types;
using Sparticle.Request.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparticle.Service.Handler
{
    public class InspectContext
    {
        public RequestContext RequestContext { get; set; }

        public IFullTrace Trace { get; set; }

        public AccessLevel AccessLevel { get; set; }

        public string ApiName { get; set; }

        public string Domain { get; set; }

        public ActionAccessConfig ActionConfig { get; set; }
    }

    public class InspectContext<TRequest> : InspectContext
    {
        public TRequest Request { get; set; }
    }
}
