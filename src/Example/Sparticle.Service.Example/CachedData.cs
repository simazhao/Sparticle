using Sparticle.Common;
using Sparticle.Result;
using Sparticle.SAL.Example;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparticle.Service.Example
{
    internal class CachedData
    {
        private static ExampleAccess _access = new ExampleAccess();
        private static readonly CachedDataHelper _helper = null;
        private static Random random = new CyptoRandom();

        static CachedData()
        {
            _helper = new CachedDataHelper(CacheKeyPrefix.KeyPrefixs);
        }

        public static CachedResult<int> MakeRandom(string name, int seconds)
        {
            return _helper.GetCachedValue(CacheKeyPrefix.Store, name, () =>
            {
                return ApiResult<int>.MakeSuccessResult(random.Next(1, Int32.MaxValue));

            }, TimeSpan.FromSeconds(seconds), "Local");
        }
    }
}
