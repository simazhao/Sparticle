using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparticle.Config.LocalSetting
{
    public class GlobalCacheExpireConfig
    {
        public static readonly int ExpireMintues = Convert.ToInt32(ConfigurationManager.AppSettings["GlobalCacheExpire"]);

        public static readonly TimeSpan Expire = new TimeSpan(0, ExpireMintues, 0);
    }
}
