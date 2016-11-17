using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;

namespace Sparticle.Cache
{
    internal class CacheBuilder
    {
        private IKernel _kernel = new StandardKernel();

        public static CacheBuilder Instance = new CacheBuilder();

        public CacheBuilder()
        {
            _kernel.Bind<ICachePolicy>().To<CacheNothingPolicy>().InSingletonScope().Named("Nothing");
            _kernel.Bind<ICachePolicy>().To<CouchBasePolicy>().InSingletonScope().Named("Couch");
            _kernel.Bind<ICachePolicy>().To<RedisCachePolicy>().InSingletonScope().Named("Redis");
            _kernel.Bind<ICachePolicy>().To<RuntimeCachePolicy>().InSingletonScope().Named("Local");

            string[] wantToMix = new string[] { "Couch", "Redis", "Local" };

            for (int first=0; first<wantToMix.Length; ++first)
            {
                for (int second=0; second<wantToMix.Length; ++second)
                {
                    if (first == second)
                        continue;

                    _kernel.Bind<ICachePolicy>().To<MixedCachePolicy>().InSingletonScope().Named(wantToMix[first] + wantToMix[second])
                        .WithConstructorArgument("policies", new ICachePolicy[] { this[wantToMix[first]], this[wantToMix[second]] });
                }
            }
        }

        public ICachePolicy this[string key]
        {
            // just throw exception
            get { return _kernel.Get<ICachePolicy>(key); }
        }
    }
}
