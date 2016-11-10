using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparticle.Common
{
    public interface IExceptionTrace
    {
        string Exception { get; }

        void SetException(Exception exp);
    }
}
