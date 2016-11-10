using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparticle.Common
{
    public enum SaveLogOption
    {
        Local = 1,
        MongoDb,
        HBase,
        Oracle,
        MySql,
        SqlServer,
    }
}
