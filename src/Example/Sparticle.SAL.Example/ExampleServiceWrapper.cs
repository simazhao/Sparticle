using Sparticle.Config.LocalSetting;
using Sparticle.SAL.Example.ServiceCollectionReference;
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
            var client = new ServiceCollectionClient();

            var address = client.GetServiceAddress(new ServiceAddressRequest()
            {
                No = LocalConfig.MachineNo,

                ServiceGroup = "ExampleGroup",

                ServiceIdentity = serviceIdentity,

                MMode = ServiceAddressRequest.MatchMode.No,
            });

            client.Close();

            if (address == null)
                return null;

            return new ServiceAddressModel()
            {
                Address = address.Address,
                ServiceIdentity = address.ServiceIdentity, 
                PropertyList = address.PropertyList,
            };
        }
    }
}
