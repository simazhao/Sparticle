using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparticle.Cache
{
    public interface IMixedCachePolicy : ICachePolicy
    {
        void SetPolicies(params ICachePolicy[] policies);

        ICachePolicy[] Policies { get; }
    }
}
