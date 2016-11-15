using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparticle.Support.Logger
{
    public interface ILoggerPolicyFactory
    {
        ILoggerPolicy this[string category, string type, string tag = "Base"] { get; }
    }
}
