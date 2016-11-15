using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparticle.Support.Logger
{
    public interface ILoggerPolicy
    {
        void Info<TInfo>(TInfo msg);

        void Error<TInfo>(TInfo msg);

        void Warn<TInfo>(TInfo msg);

        void Slow<TInfo>(TInfo msg);
    }
}
