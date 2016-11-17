using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparticle.Config.LocalSetting
{
    public class LocalConfig
    {
        public static readonly string HandlerPluginDir = ConfigurationManager.AppSettings["HandlersDir"];

    }
}
