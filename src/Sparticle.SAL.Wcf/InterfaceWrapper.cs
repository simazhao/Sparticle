using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparticle.SAL.Wcf
{
    public abstract class InterfaceWrapper
    {
        public SvcWrapper<TService> GetTService<TService>()
            where TService : class
        {
            var machineNo = ConfigurationManager.AppSettings["machineNo"];

            return GetTService<TService>(machineNo);
        }

        public SvcWrapper<TService> GetTService<TService>(string machineNo)
            where TService : class
        {
            string serviceName = typeof(TService).Name.Substring(1);

            string serviceIdentity = typeof(TService).FullName;

            ServiceAddressModel serviceAddress = GetServiceAddress(machineNo, serviceName);

            return WcfFactory.Instance.CreateServiceWrapper<TService>(serviceAddress.PropertyList["binding"], serviceAddress.Address);
        }

        /// <summary>
        /// get service address model by machineno and serviceIndentity from local settings, remote service or service-found service.
        /// </summary>
        /// <param name="machineNo"></param>
        /// <param name="serviceIdentity"></param>
        /// <returns></returns>
        protected abstract ServiceAddressModel GetServiceAddress(string machineNo, string serviceIdentity);
    }
}
