using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace Sparticle.ServiceKeeper.Wcf
{
    public static class ReaderWriterLockSlimExtends
    {
        public static DisposeAction ReadLock(this ReaderWriterLockSlim slim)
        {
            return new DisposeAction(slim.EnterReadLock, slim.ExitReadLock);
        }

        public static DisposeAction WriteLock(this ReaderWriterLockSlim slim)
        {
            return new DisposeAction(slim.EnterWriteLock, slim.ExitWriteLock);
        }
    }
}