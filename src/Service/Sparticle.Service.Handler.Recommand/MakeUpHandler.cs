using Sparticle.Common;
using Sparticle.Request.Context;
using Sparticle.Result;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Sparticle.Service.Handler.Recommand
{
    [Export(typeof(IRequestHandler))]
    class MakeUpHandler : RequestHandlerBase
    {
        public override void BeforeHandle<TRequest>(InspectContext<TRequest> inspectCxt)
        {
            if (inspectCxt.RequestContext == null)
            {
                inspectCxt.RequestContext = new RequestContext();

                inspectCxt.Trace.RequestContext = inspectCxt.RequestContext;
            }

            if (inspectCxt.RequestContext.Device == null)
            {
                inspectCxt.RequestContext.Device = new RequestDevice();
            }

            if (inspectCxt.RequestContext.Channel == null)
            {
                inspectCxt.RequestContext.Channel = new RequestChannel();
            }

            inspectCxt.RequestContext.Device.Ip = NetWorkHelper.GetClientIP();
            inspectCxt.RequestContext.Channel.UserAgent = HttpContext.Current.Request.UserAgent;

            do
            {
                if (string.IsNullOrEmpty(inspectCxt.RequestContext.Device.DeviceType))
                {
                    inspectCxt.RequestContext.Device.DeviceType = "Andriod";

                    if (!string.IsNullOrEmpty(inspectCxt.RequestContext.Channel.UserAgent) &&
                        inspectCxt.RequestContext.Channel.UserAgent.IndexOf("iPhone", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        inspectCxt.RequestContext.Device.DeviceType = "IPhone";
                    }

                    break;
                }

                if (inspectCxt.RequestContext.Device.DeviceType.IndexOf("ios", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    inspectCxt.RequestContext.Device.DeviceType = "IPhone";
                    break;
                }
            } while (false);
        }

        public override void AfterHandle<TRequest>(InspectContext<TRequest> inspectCxt, ApiResult result)
        {

        }
    }
}
