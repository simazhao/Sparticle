using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparticle.Config.Types
{
    [Flags]
    public enum SaveLogOptionPosition
    {
        Text        = 0,
        MongoDb     = 1,
        HBase       = 2,
        Oracle      = 3,
        MySql       = 4,
        SqlServer   = 5,
    }
}
