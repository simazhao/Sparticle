using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparticle.Cache
{
    internal class MixedCachePolicy : IMixedCachePolicy
    {
        public MixedCachePolicy()
        {

        }

        public MixedCachePolicy(params ICachePolicy[] policies)
        {
            SetPolicies(policies);
        }

        private readonly IList<ICachePolicy> _policies = new List<ICachePolicy>();

        public void SetPolicies(params ICachePolicy[] policies)
        {
            _policies.Clear();
            policies.All(p =>
            {
                _policies.Add(p);
                return true;
            });
        }

        public ICachePolicy[] Policies
        {
            get { return _policies.ToArray(); }
        }

        public bool Add(string key, object value, TimeSpan expire)
        {
            var ret = false;

            foreach (var policy in _policies)
            {
                var part = policy.Add(key, value, expire);
                ret = ret || part;
            }

            return ret;
        }

        public bool Remove(string key)
        {
            var ret = false;

            foreach (var policy in _policies)
            {
                var part = policy.Remove(key);
                ret = ret || part;
            }

            return ret;
        }

        public TData Get<TData>(string key)
        {
            foreach (var policy in _policies)
            {
                var data = policy.Get<TData>(key);

                if (!data.Equals(default(TData)))
                    return data;
            }

            return default(TData);
        }

        public TData Get<TData>(string key, TimeSpan newExpiration)
        {
            foreach (var policy in _policies)
            {
                var data = policy.Get<TData>(key, newExpiration);

                if (!data.Equals(default(TData)))
                    return data;
            }

            return default(TData);
        }

        public bool TryGet<TData>(string key, out TData value)
        {
            value = default(TData);

            foreach (var policy in _policies)
            {
                var got = policy.TryGet(key, out value);

                if (got)
                {
                    return true;
                }
            }

            return false;
        }


        public bool TryGet<TData>(string key, TimeSpan newExpiration, out TData value)
        {
            value = default(TData);

            foreach (var policy in _policies)
            {
                var got = policy.TryGet(key, newExpiration, out value);

                if (got)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
