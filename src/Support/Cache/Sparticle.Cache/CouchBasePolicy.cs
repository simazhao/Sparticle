using Sparticle.Support.NoSql.CouchBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparticle.Cache
{
    internal class CouchBasePolicy : CouchbaseDriver3, ICountableCachePolicy
    {
        long ICountableCachePolicy.Decrement(string key, uint delta, TimeSpan expire, uint init)
        {
            return (long)Decrement(key, delta, expire, init);
        }

        long ICountableCachePolicy.Increment(string key, ulong defaultValue, uint delta)
        {
            return (long)Increment(key, defaultValue, delta);
        }

        long ICountableCachePolicy.Increment(string key, uint delta, TimeSpan expire, uint init)
        {
            return (long)Increment(key, delta, expire, init);
        }
    }
}
