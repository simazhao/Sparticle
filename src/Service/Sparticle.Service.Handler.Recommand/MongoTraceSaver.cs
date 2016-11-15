using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sparticle.Common;
using Sparticle.Support.Logger;

namespace Sparticle.Service.Handler.Recommand
{
    class MongoTraceSaver : TraceSaver
    {
        public override void SaveError(string domain, IFullTrace trace)
        {
            var db = GetDbName(domain, trace);
            var collection = GetCollectionName(domain);

            LoggerInstance.Mongo[db, collection].Error(trace);
        }

        public override void SaveInfo(string domain, IFullTrace trace)
        {
            var db = GetDbName(domain, trace);
            var collection = GetCollectionName(domain);

            LoggerInstance.Mongo[db, collection].Info(trace);
        }

        public override void SaveSlow(string domain, IFullTrace trace)
        {
            var db = GetDbName(domain, trace);
            var collection = GetCollectionName(domain);

            LoggerInstance.Mongo[db, collection].Slow(trace);
        }

        public override void SaveWarn(string domain, IFullTrace trace)
        {
            var db = GetDbName(domain, trace);
            var collection = GetCollectionName(domain);

            LoggerInstance.Mongo[db, collection].Warn(trace);
        }

        private string GetDbName(string domain, IFullTrace trace)
        {
            return string.Format("{0}-{1}", trace.ApiName, domain);
        }

        private string GetCollectionName(string domain)
        {
            return String.Format("{0}{1}", domain, DateTime.Now.ToString("yyyyMMdd"));
        }
    }
}
