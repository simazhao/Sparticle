using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparticle.Support.NoSql.Redis
{
    class RedisConfig
    {
        public static string[] Hosts = new[] { ConfigurationManager.AppSettings["RedisHosts"] };
    }
}
