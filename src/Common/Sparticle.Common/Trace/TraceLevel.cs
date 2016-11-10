using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparticle.Common
{
    [Flags]
    public enum TraceLevel
    {
        /// <summary>
        /// 0x00000001
        /// </summary>
        Info = 1,

        /// <summary>
        /// 0x00000010
        /// </summary>
        Warn = 2,

        /// <summary>
        /// 0x00000100
        /// </summary>
        Error = 4,
    }
}
