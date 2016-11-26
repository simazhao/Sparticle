using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace Sparticle.ServiceKeeper.Wcf
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            var dueTime = new TimeSpan(0, 5, 0);
            var period = new TimeSpan(0, 30, 0);

            var scanTimer = new ScanTimer(dueTime, period);

            dueTime = new TimeSpan(0, 5, 0);
            period = new TimeSpan(0, 30, 0);

            var serializeTimer = new SerializeTimer(dueTime, period);

            new ServiceAddressPoolLoader().Load();
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}