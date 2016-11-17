using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace Sparticle.SAL.Wcf
{
    public class SvcWrapper : IDisposable
    {
        protected IChannel Channel;
        public SvcWrapper(IChannel svc)
        {
            Channel = svc;
        }

        public IContextChannel ContextChannel
        {
            get { return Channel as IContextChannel; }
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    if (Channel != null)
                    {
                        try
                        {
                            Channel.Close();
                        }
                        catch (Exception)
                        {
                            Channel.Abort();
                        }

                        (Channel as IDisposable).Dispose();

                        Channel = null;
                    }
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        ~SvcWrapper()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(false);
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            GC.SuppressFinalize(this);
        }
        #endregion


    }

    public class SvcWrapper<TSvc> : SvcWrapper
        where TSvc : class
    {
        public SvcWrapper(TSvc svc)
            : base(svc as IChannel)
        {

        }

        public TSvc Svc
        {
            get { return Channel as TSvc; }
        }
    }
}
