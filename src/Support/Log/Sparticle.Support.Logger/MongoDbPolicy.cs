using Sparticle.Support.NoSql.MongoDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparticle.Support.Logger
{
    class MongoDbPolicy : ILoggerPolicy
    {
        private MongoDbDriver17 _driver;
        private string _normalCollectionName;
        private string _warnCollectionName;
        private string _errorCollectionName;
        private string _slowCollectionName;


        public MongoDbPolicy(string dbName, string normalCollectionName, string tag)
        {
            _normalCollectionName = normalCollectionName;
            _warnCollectionName = normalCollectionName + "_Warn";
            _errorCollectionName = normalCollectionName + "_Error";
            _slowCollectionName = normalCollectionName + "_Slow";

            _driver = new MongoDbDriver17(tag, dbName, normalCollectionName, _warnCollectionName, _errorCollectionName, _slowCollectionName);
        }

        public void Info<TInfo>(TInfo msg)
        {
            _driver.Save(_normalCollectionName, msg);
        }

        public void Warn<TInfo>(TInfo msg)
        {
            _driver.Save(_warnCollectionName, msg);
        }

        public void Error<TInfo>(TInfo msg)
        {
            _driver.Save(_errorCollectionName, msg);
        }

        public void Slow<TInfo>(TInfo msg)
        {
            _driver.Save(_slowCollectionName, msg);
        }
    }
}
