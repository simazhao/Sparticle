using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Sparticle.ServiceKeeper.Interface
{
    [ServiceContract]
    public interface IServiceRegister
    {
        [OperationContract]
        bool Register(ServiceRegisteRequest request);

        [OperationContract]
        bool UnRegister(ServiceUnregisteRequest request);
    }
}
