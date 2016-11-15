using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparticle.Support.NoSql.MongoDb
{
    public static class MongoDbConnections
    {
        private static IDictionary<string, MongoDbConnection> _dbConnections = new Dictionary<string, MongoDbConnection>();

        public static IDictionary<string, MongoDbConnection> DbConnections
        {
            get
            {
                return _dbConnections;
            }
        }

        static MongoDbConnections()
        {
            Init();
        }

        private static void Init()
        {
            if (_dbConnections.Count == 0)
            {
                var configs = MongoDbConfigs.DbConfigs;

                foreach (var mongoDbConfig in configs)
                {
                    try
                    {
                        var conn = new MongoDbConnection(mongoDbConfig);
                        _dbConnections.Add(mongoDbConfig.ServerTag, conn);
                    }
                    catch (Exception)
                    {

                    }
                }
            }
        }
    }

    public class MongoDbConnection
    {
        private MongoServer _mongoSever = null;
        private MongoDbConfig _config = null;

        public MongoDbConnection(MongoDbConfig config)
        {
            Init(config);
        }

        public MongoServer Server
        {
            get
            {
                return _mongoSever;
            }
        }

        private bool Init(MongoDbConfig config)
        {
            _config = config;
            _mongoSever = null;

            try
            {
                _mongoSever = Connect(config);
            }
            catch
            {

            }

            return _mongoSever != null;
        }

        /// <summary>
        /// 连接数据库
        /// </summary>
        /// <returns></returns>
        private MongoServer Connect(MongoDbConfig config)
        {
            if (_mongoSever == null)
            {
                try
                {
                    var mongoSettings = new MongoServerSettings();
                    mongoSettings.Server = new MongoServerAddress(config.Server, config.Port);

                    if (config.HasCredential)
                    {
                        mongoSettings.DefaultCredentials = new MongoCredentials(config.UserName, config.Password, true);
                    }

                    _mongoSever = new MongoServer(mongoSettings);
                    _mongoSever.Connect();
                }
                catch (Exception ex)
                {
                    throw (new Exception("_conn:" + config.ServerConn + "\r\n" + ex));
                }
            }

            return _mongoSever;
        }
    }
}
