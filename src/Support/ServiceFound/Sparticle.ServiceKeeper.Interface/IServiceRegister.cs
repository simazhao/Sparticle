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
        bool Register(ServiceRegisteRequest request);

        bool UnRegister(ServiceUnregisteRequest request);
    }
}
