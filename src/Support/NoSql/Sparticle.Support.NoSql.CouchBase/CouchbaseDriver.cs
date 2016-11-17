using Couchbase;
using Enyim.Caching.Memcached;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparticle.Support.NoSql.CouchBase
{
    public class CouchbaseDriver3
    {
        private static object locker = new object();
        private CouchbaseClient _client = null;

        // lazy load client
        private CouchbaseClient client
        {
            get
            {
                if (_client == null)
                {
                    lock(locker)
                    {
                        if (_client == null)
                        {
                            _client = new CouchbaseClient();
                        }
                    }
                }

                return _client;
            }
        }

        public bool Add(string key, object value, TimeSpan expire)
        {
            var json = JsonConvert.SerializeObject(value);

            return client.Store(StoreMode.Set, key, json, expire);
        }

        public ulong Increment(string key, uint delta, TimeSpan expire, uint init = 1)
        {
            return client.Increment(key, init, delta, expire);
        }

        public ulong Decrement(string key, uint delta, TimeSpan expire, uint init = 1)
        {
            return client.Decrement(key, init, delta, expire);
        }

        public ulong Increment(string key, ulong defaultValue, uint delta)
        {
            return client.Increment(key, defaultValue, delta);
        }

        public bool Remove(string key)
        {
           return client.Remove(key);
        }

        private object Get(string key)
        {
            return client.Get(key);
        }

        private object Get(string key, TimeSpan newExpiration)
        {
            return client.Get(key, DateTime.Now.Add(newExpiration));
        }

        public TData Get<TData>(string key)
        {
            var data = Get(key) as string;

            if (data == null)
                return default(TData);

            return JsonConvert.DeserializeObject<TData>(data);
        }

        public TData Get<TData>(string key, TimeSpan newExpiration)
        {
            var data = Get(key, newExpiration) as string;

            if (data == null)
                return default(TData);

            return JsonConvert.DeserializeObject<TData>(data);
        }

        public bool TryGet<TData>(string key, out TData value)
        {
            value = default(TData);

            try
            {
                object obj;

                var got = client.TryGet(key, out obj);

                got = got && obj != null && (string)obj != "null";

                if (got)
                {
                    value = JsonConvert.DeserializeObject<TData>(obj.ToString());
                }

                return got;
            }
            catch (Exception)
            {
                value = default(TData);

                return false;
            }
        }

        public bool TryGet<TData>(string key, TimeSpan newExpiration, out TData value)
        {
            value = default(TData);

            try
            {
                object obj;

                var got = client.TryGet(key, DateTime.Now.Add(newExpiration), out obj);

                got = got && null != obj && (string)obj != "null";

                if (got)
                {
                    value = JsonConvert.DeserializeObject<TData>(obj.ToString());
                }

                return got;
            }
            catch (Exception)
            {
                value = default(TData);

                return false;
            }
        }

        public CasedResult<bool> CasAdd(string key, object value, TimeSpan expire, ulong cas)
        {
            var json = JsonConvert.SerializeObject(value);
            return Convert(client.Cas(StoreMode.Set, key, json, expire, cas));
        }

        public CasedResult<object> GetWithCas(string key)
        {
            return Convert(client.GetWithCas(key));
        }

        public CasedResult<TData> GetWithCas<TData>(string key)
        {
            var casResult = client.GetWithCas(key);

            return Convert2<TData>(casResult);
        }

        private CasedResult<T> Convert<T>(CasResult<T> casResult)
        {
            return new CasedResult<T>() { Cas = casResult.Cas, Result = casResult.Result };
        }

        private CasedResult<T> Convert2<T>(CasResult<object> casResult)
        {
            var result = new CasedResult<T>();
            result.Cas = casResult.Cas;
            if (casResult.Result != null)
            {
                result.Result = JsonConvert.DeserializeObject<T>(casResult.Result.ToString());
            }

            return result;
        }
    }
}
