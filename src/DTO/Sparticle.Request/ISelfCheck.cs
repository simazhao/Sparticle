using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparticle.Request
{
    public interface ISelfCheck
    {
        bool Check(out string error);
    }
}
