using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparticle.Support.NoSql.MongoDb
{
    public static class MongoDbConfigs
    {
        public static readonly string ServerConns = ConfigurationManager.AppSettings["MongoDB.Servers"] ?? string.Empty;

        private static IList<MongoDbConfig> _dbConfigs;
        public static IList<MongoDbConfig> DbConfigs
        {
            get
            {
                if (_dbConfigs == null)
                {
                    lock (ServerConns)
                    {
                        if (_dbConfigs == null)
                        {
                            _dbConfigs = new List<MongoDbConfig>();

                            var conns = ServerConns.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                            foreach (var conn in conns)
                            {
                                _dbConfigs.Add(new MongoDbConfig(conn));
                            }
                        }
                    }
                }

                return _dbConfigs;
            }
        }

    }
    public class MongoDbConfig
    {
        public string ServerConn { get; private set; } // example: Base#111.222.111.222:27017{user=password}

        public MongoDbConfig(string serverConn)
        {
            ServerConn = serverConn;

            ParseConn(serverConn);
        }

        private string _serverTag;
        private string _server;
        private int _port;
        private string _userName;
        private string _password;

        private void ParseConn(string serverConn)
        {
            var lIndex = 0;
            var rIndex = serverConn.IndexOf('#');
            _serverTag = serverConn.Substring(lIndex, rIndex);

            lIndex = rIndex + 1;
            rIndex = serverConn.IndexOf(':', lIndex);
            _server = serverConn.Substring(lIndex, rIndex - lIndex);

            lIndex = rIndex + 1;
            rIndex = serverConn.IndexOf('{');

            if (rIndex == -1)
            {
                rIndex = serverConn.Length;
            }

            var port = ServerConn.Substring(lIndex, rIndex - lIndex);
            if (!int.TryParse(port, out _port))
            {
                _port = 27017;
            }

            if (rIndex == serverConn.Length)
                return;

            lIndex = rIndex + 1;
            rIndex = serverConn.IndexOf('=');
            _userName = serverConn.Substring(lIndex, rIndex - lIndex);

            lIndex = rIndex + 1;
            rIndex = serverConn.IndexOf('}');
            _password = serverConn.Substring(lIndex, rIndex - lIndex);
        }

        public string ServerTag
        {
            get
            {
                return _serverTag;
            }
        }

        public string Server
        {
            get
            {
                return _server;
            }
        }

        public int Port
        {
            get
            {
                return _port;
            }
        }

        public string UserName
        {
            get
            {
                return _userName;
            }
        }

        public string Password
        {
            get
            {
                return _password;
            }
        }

        public bool HasCredential
        {
            get
            {
                return !string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password);
            }
        }
    }
}
