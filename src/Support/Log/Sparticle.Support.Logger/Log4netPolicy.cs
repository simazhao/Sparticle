using log4net;
using log4net.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparticle.Support.Logger
{
    internal class Log4netPolicy : ILoggerPolicy
    {
        private readonly ILog _log;

        public Log4netPolicy(ILog log)
        {
            _log = log;
        }

        public void Info<TInfo>(TInfo msg)
        {
            _log.Info(msg.ToString());
        }

        public void Error<TInfo>(TInfo msg)
        {
            _log.Error(msg.ToString());
        }

        public void Warn<TInfo>(TInfo msg)
        {
            _log.Warn(msg.ToString());
        }

        public void Slow<TInfo>(TInfo msg)
        {
            var slowEventData = new LoggingEventData()
            {
                Level = Level.Notice,
                Message = msg.ToString(),
            };

            _log.Logger.Log(new LoggingEvent(slowEventData));
        }
    }
}
