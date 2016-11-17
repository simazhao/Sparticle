using Sparticle.Request.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Sparticle.Service
{
    class ServiceSession
    {
        public ServiceSession()
        {

        }

        [ThreadStatic]
        public static ServiceSession Current = new ServiceSession();

        public RequestContext RequestContext
        {
            get;set;
        }

        public string ActionName
        {
            get
            {
                var action = HttpContext.Current.Request.RequestContext.RouteData.Values["action"];
                return action == null ? string.Empty : action.ToString().ToLower();
            }
        }
    }
}
