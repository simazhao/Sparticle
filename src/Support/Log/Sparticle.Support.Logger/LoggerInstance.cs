using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparticle.Support.Logger
{
    public static class LoggerInstance
    {
        public static ILoggerPolicyFactory Log4net { get { return LoggerBuilder.Instance["Log4net"]; } }

        public static ILoggerPolicyFactory Mongo { get { return LoggerBuilder.Instance["Mongo"]; } }

        public class LoggerCollection
        {
            private static IDictionary<string, ILoggerPolicyFactory> _allLogger = new Dictionary<string, ILoggerPolicyFactory>();
            private static void InitAllCache()
            {
                _allLogger["Log4net"] = Log4net;
                _allLogger["Mongo"] = Mongo;
            }

            public ILoggerPolicyFactory this[string name]
            {
                get
                {
                    if (!_allLogger.Any())
                    {
                        lock (_allLogger)
                        {
                            if (!_allLogger.Any())
                            {
                                InitAllCache();
                            }
                        }
                    }

                    return _allLogger[name];
                }
            }
        }

        public static LoggerCollection Collection = new LoggerCollection();
    }
}
