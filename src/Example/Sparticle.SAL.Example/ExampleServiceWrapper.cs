using Sparticle.SAL.Wcf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparticle.SAL.Example
{
    class ExampleServiceWrapper : InterfaceWrapper
    {
        protected override ServiceAddressModel GetServiceAddress(string machineNo, string serviceIdentity)
        {
            // use hard code, just for example, do not real use in product.
            var host = "http://localhost:25485";

            return new ServiceAddressModel()
            {
                Address = string.Format("{0}/{1}.svc", host, serviceIdentity),
                ServiceIdentity = serviceIdentity,
                PropertyList = new Dictionary<string, string> { {"binding", "basicHttpBinding" } }
            };
        }
    }
}
