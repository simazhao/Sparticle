using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;

namespace Sparticle.Cache
{
    internal class RuntimeCachePolicy : ICachePolicy
    {
        public bool Add(string key, object value, TimeSpan expire)
        {
            try
            {
                HttpRuntime.Cache.Insert(key, value, null, DateTime.Now.Add(expire), System.Web.Caching.Cache.NoSlidingExpiration,
                        CacheItemPriority.High, OnRemove);
            }
            catch(Exception ex)
            {
                return false;
            }

            return true;
        }

        public bool Remove(string key)
        {
           return HttpRuntime.Cache.Remove(key) != null;
        }

        private object Get(string key)
        {
            return HttpRuntime.Cache.Get(key);
        }

        private object Get(string key, TimeSpan newExpiration)
        {
            var obj = Get(key);
            if (obj != null)
            {
                Add(key, obj, newExpiration);
            }

            return obj;
        }

        public TData Get<TData>(string key)
        {
            return (TData)Get(key);
        }

        public TData Get<TData>(string key, TimeSpan newExpiration)
        {
            return (TData)Get(key, newExpiration);
        }

        public bool TryGet<TData>(string key, out TData value)
        {
            var obj = Get(key);

            value = obj != null ? (TData)obj : default(TData);

            return obj != null;
        }


        public bool TryGet<TData>(string key, TimeSpan newExpiration, out TData value)
        {
            var got = TryGet(key, out value);
            if (got)
            {
                Add(key, value, newExpiration);
            }

            return got;
        }

        private void OnRemove(string key, object val, CacheItemRemovedReason reason)
        {
            switch (reason)
            {
                case CacheItemRemovedReason.DependencyChanged:
                    {
                        break;
                    }
                case CacheItemRemovedReason.Expired:
                    {
                        break;
                    }
                case CacheItemRemovedReason.Removed:
                    {
                        break;
                    }
                case CacheItemRemovedReason.Underused:
                    {
                        break;
                    }
                default:
                    break;
            }
        }
    }
}
