using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Sparticle.ServiceCollection.Interface
{
    [ServiceContract]
    public interface IServiceCollection
    {
        [OperationContract]
        ServiceAddressResponse GetServiceAddress(ServiceAddressRequest request);
        
        [OperationContract(IsOneWay = true)]
        void Reload();
    }
}
