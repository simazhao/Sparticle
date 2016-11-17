using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparticle.Cache
{
    public interface ICountableCachePolicy : ICachePolicy
    {
        long Increment(string key, uint delta, TimeSpan expire, uint init = 1);

        long Decrement(string key, uint delta, TimeSpan expire, uint init = 1);

        long Increment(string key, ulong defaultValue, uint delta);
    }
}
