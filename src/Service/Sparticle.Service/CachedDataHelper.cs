using Sparticle.Cache;
using Sparticle.Common;
using Sparticle.Common.Trace;
using Sparticle.Config.LocalSetting;
using Sparticle.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Sparticle.Service
{
    public class CachedDataHelper
    {
        [ThreadStatic]
        public static IFullTrace Trace;

        private readonly IDictionary<string, object> _LockObjects = new Dictionary<string, object>();

        public CachedDataHelper(IEnumerable<string> keyPrefixs)
        {
            foreach (var prefix in keyPrefixs)
            {
                _LockObjects.Add(prefix, new object());
            }
        }

        public CachedResult<T> GetCachedValue<T>(string prefix, string subKey, Func<ApiResult<T>> loader, string cacheName = "Couch", bool useLock = false)
        {
            return GetCachedValue(prefix, subKey, loader, GlobalCacheExpireConfig.Expire, cacheName, useLock);
        }

        private readonly Regex _loaderRegex = new Regex(@">b__.+");

        public CachedResult<T> GetCachedValue<T>(string prefix, string subKey, Func<ApiResult<T>> loader, TimeSpan expire, string cacheName = "Couch", bool useLock = false)
        {
            var result = CachedResult<T>.MakeSucessResult();
            var key = prefix + subKey;
            T data;

            var callStep = new CallStep();

            var methodName = _loaderRegex.Replace(loader.Method.Name.Replace("<", ""), "");
            callStep.Start(methodName);


            var cache = CacheInstance.Collection[cacheName];

            if (!cache.TryGet(key, out data))
            {
                if (useLock && _LockObjects.ContainsKey(prefix))
                {
                    Monitor.Enter(_LockObjects[prefix]);
                }

                if (!useLock || !cache.TryGet(key, out data))
                {

                    var loadResult = loader();

                    if (loadResult.Success)
                    {
                        data = loadResult.Data;
                        cache.Add(key, data, expire);
                    }

                    result.HitCache = false;
                    result.Copy(loadResult);
                }

                if (useLock && _LockObjects.ContainsKey(prefix))
                {
                    Monitor.Exit(_LockObjects[prefix]);
                }
            }

            result.Data = data;

            callStep.Stop(result, result.HitCache);

            return result;
        }
    }
}
