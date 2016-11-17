using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparticle.Cache
{
    public static class CacheInstance
    {
        public static ICachePolicy Local { get { return CacheBuilder.Instance["Local"]; } }

        public static ICachePolicy Couch { get { return CacheBuilder.Instance["Couch"]; } }

        public static ICachePolicy Redis { get { return CacheBuilder.Instance["Redis"]; } }

        public static ICachePolicy None { get { return CacheBuilder.Instance["None"]; } }

        public static ICachePolicy CouchLocal { get { return CacheBuilder.Instance["CouchLocal"]; } }

        public static ICachePolicy LocalCouch { get { return CacheBuilder.Instance["LocalCouch"]; } }


        public class CacheCollection
        {
            public ICachePolicy this[string name]
            {
                get { return CacheBuilder.Instance[name]; }
            }
        }

        public static CacheCollection Collection = new CacheCollection();
    }
}
