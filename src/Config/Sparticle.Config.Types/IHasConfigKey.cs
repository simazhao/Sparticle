using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparticle.Config.Types
{
    public interface IHasConfigKey
    {
        string ConfigKey { get; }
    }
}
