using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparticle.Service.Example
{
    class CacheKeyPrefix
    {
        private const string store = "Example.Store";

        private static readonly string[] keyPrefixs = new[]
        {
            Store,
        };

        public static string[] KeyPrefixs
        {
            get
            {
                return keyPrefixs;
            }
        }

        public static string Store
        {
            get
            {
                return store;
            }
        }
    }
}
