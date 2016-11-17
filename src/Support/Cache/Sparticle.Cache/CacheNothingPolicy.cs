using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparticle.Cache
{
    internal class CacheNothingPolicy : ICachePolicy
    {
        public bool Add(string key, object value, TimeSpan expire)
        {
            return false;
        }

        public bool Remove(string key)
        {
            return false;
        }

        public TData Get<TData>(string key)
        {
            return default(TData);
        }

        public TData Get<TData>(string key, TimeSpan newExpiration)
        {
            return default(TData);
        }

        public bool TryGet<TData>(string key, out TData value)
        {
            value = default(TData);
            return false;
        }

        public bool TryGet<TData>(string key, TimeSpan newExpiration, out TData value)
        {
            value = default(TData);
            return false;
        }
    }
}
