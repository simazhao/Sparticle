using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparticle.Support.NoSql.CouchBase
{
    public class CasedResult<TData>
    {
        public TData Result { get; set; }

        public ulong Cas { get; set; }
    }
}
