using log4net.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparticle.Support.Logger
{
    class LogLevelExtend
    {
        public static readonly Level SLOW = new Level(Level.Info.Value + 1, "SLOW");
    }
}
