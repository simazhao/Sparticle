using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparticle.Support.Logger
{
    abstract class LoggerPolicyFactory : ILoggerPolicyFactory
    {
        private readonly IDictionary<string, ILoggerPolicy> _policies = new Dictionary<string, ILoggerPolicy>();

        public ILoggerPolicy this[string category, string type, string tag]
        {
            get
            {
                var key = string.Format("{0}{1}", category, type);

                if (!_policies.ContainsKey(key))
                {
                    lock (this)
                    {
                        if (!_policies.ContainsKey(key))
                        {

                            var policy = CreateLogger(category, type, tag);

                            _policies.Add(key, policy);
                        }
                    }
                }

                return _policies[key];
            }
        }

        protected abstract ILoggerPolicy CreateLogger(string category, string type, string tag);
    }

    class Log4netPolicyFactory : LoggerPolicyFactory
    {
        public Log4netPolicyFactory()
        {
            Log4netConfig.CreateRespositiesByCode();
        }

        protected override ILoggerPolicy CreateLogger(string category, string type, string tag)
        {
            return new Log4netPolicy(LogManager.GetLogger(category, type));
        }
    }

    class MongoDbPolicyFactory : LoggerPolicyFactory
    {
        protected override ILoggerPolicy CreateLogger(string category, string type, string tag)
        {
            return new MongoDbPolicy(category, type, tag);
        }
    }
}
