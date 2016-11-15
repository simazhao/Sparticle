using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparticle.Support.Logger
{
    internal class LoggerBuilder
    {
        private IKernel _kernel = new StandardKernel();

        public static LoggerBuilder Instance = new LoggerBuilder();

        public LoggerBuilder()
        {
            _kernel.Bind<ILoggerPolicyFactory>().To<Log4netPolicyFactory>().InSingletonScope().Named("Log4Net");

            _kernel.Bind<ILoggerPolicyFactory>().To<MongoDbPolicyFactory>().InSingletonScope().Named("Log4Net");
        }

        public ILoggerPolicyFactory this[string key]
        {
            get { return _kernel.Get<ILoggerPolicyFactory>(key); }
        }
    }
}
