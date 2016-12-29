using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparticle.Cache
{
    public interface IStrictCachePolicy : ICachePolicy
    {
        bool Cas(string key, object value, TimeSpan expire, ulong cas);
    }
}
