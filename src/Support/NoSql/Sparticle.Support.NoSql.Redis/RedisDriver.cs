using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparticle.Support.NoSql.Redis
{
    public class RedisDriver39
    {
        internal class RedisClientManager
        {
            public static readonly RedisClientManager Instance = new RedisClientManager();

            /// <summary>
            /// redis连接池
            /// </summary>
            private PooledRedisClientManager _redisClientPool = null;

            private readonly object InitLock = new object();

            /// <summary>
            /// redis连接池
            /// </summary>
            private PooledRedisClientManager RedisClientPool
            {
                get
                {
                    if (_redisClientPool == null)
                    {
                        lock (InitLock)
                        {
                            if (_redisClientPool == null)
                            {
                                _redisClientPool = new PooledRedisClientManager(RedisConfig.Hosts);
                                _redisClientPool.ConnectTimeout = 50;
                                _redisClientPool.IdleTimeOutSecs = 30;
                                _redisClientPool.PoolTimeout = 30;
                            }
                        }
                    }

                    return _redisClientPool;
                }
            }

            private IRedisClient _Client;
            public IRedisClient RedisClient
            {
                // GeClient has lock inside
                get { return _Client ?? (_Client = RedisClientPool.GetClient()); }
            }
        }

        public virtual bool Add(string key, object value, TimeSpan expire)
        {
            return RedisClientManager.Instance.RedisClient.Set(key, value, expire);
        }

        public long Increment(string key, uint delta, TimeSpan expire, uint init = 1)
        {
            return RedisClientManager.Instance.RedisClient.Increment(key, delta);
        }

        public long Decrement(string key, uint delta, TimeSpan expire, uint init = 1)
        {
            return RedisClientManager.Instance.RedisClient.Decrement(key, delta);
        }
        public long Increment(string key, ulong defaultValue, uint delta)
        {
            return RedisClientManager.Instance.RedisClient.Increment(key, delta);
        }

        public bool Remove(string key)
        {
            return RedisClientManager.Instance.RedisClient.Remove(key);
        }

        public TData Get<TData>(string key)
        {
            return RedisClientManager.Instance.RedisClient.Get<TData>(key);
        }

        public TData Get<TData>(string key, TimeSpan newExpiration)
        {
            var ret = RedisClientManager.Instance.RedisClient.Get<TData>(key);
            RedisClientManager.Instance.RedisClient.ExpireEntryIn(key, newExpiration);

            return ret;
        }

        public bool TryGet<TData>(string key, out TData value)
        {
            value = Get<TData>(key);

            if (typeof(TData).IsValueType)
                return true;

            return value != null;
        }


        public bool TryGet<TData>(string key, TimeSpan newExpiration, out TData value)
        {
            value = Get<TData>(key, newExpiration);

            if (typeof(TData).IsValueType)
                return true;

            return value != null;
        }
    }
}
