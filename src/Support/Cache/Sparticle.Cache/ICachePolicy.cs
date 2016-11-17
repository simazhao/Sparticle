using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparticle.Cache
{
    public interface ICachePolicy
    {
        bool Add(string key, object value, TimeSpan expire);

        bool Remove(string key);

        TData Get<TData>(string key);

        TData Get<TData>(string key, TimeSpan newExpiration);

        bool TryGet<TData>(string key, out TData value);

        bool TryGet<TData>(string key, TimeSpan newExpiration, out TData value);
    }


}
