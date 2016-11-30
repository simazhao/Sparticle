using Sparticle.ServiceKeeper.Interface;
using Sparticle.ServiceKeeper.Wcf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Web;
using System.Text;

namespace Sparticle.ServiceKeeper.Wcf
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class ServiceKeeper : IServiceRegister, IServiceKeeper
    {
        private static ServiceAddressPool pool = ServiceAddressPool.Instance;

        public ServiceAddress GetServiceAddress(string serviceIdentity)
        {
            var node = pool.GetOne(serviceIdentity);

            if (node == null)
                return null;

            return node.CopyBase();
        }

        public bool Register(ServiceRegisteRequest request)
        {
            return pool.Add(request.Address, request.ServiceIdentity, GetClientIp());
        }

        public bool UnRegister(ServiceUnregisteRequest request)
        {
            return pool.Remove(request.ServiceIdentity, GetClientIp(), request.Address);
        }

        private string GetClientIp()
        {
            OperationContext context = OperationContext.Current;

            var endpoint = context.IncomingMessageProperties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;

            return endpoint.Address;
        }
    }
}
