using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace Sparticle.SAL.Wcf
{
    public class WcfFactory
    {
        public readonly static WcfFactory Instance = new WcfFactory();

        public SvcWrapper<TService> CreateServiceWrapper<TService>(string bindingType, string address)
            where TService : class
        {
            var svc = CreateService<TService>(bindingType, address);
            return new SvcWrapper<TService>(svc);
        }

        private TService CreateService<TService>(string bindingType, string address)
            where TService : class
        {
            var binding = BindingFactory.GetBinding(bindingType);
            if (binding == null)
                return null;

            return CreateService<TService>(binding, address);
        }

        private TService CreateService<TService>(Binding binding, string address)
        {
            return CreateService<TService>(binding, new EndpointAddress(address));
        }

        private TService CreateService<TService>(Binding binding, EndpointAddress address)
        {
            var factory = ChannelFactoryCache.CreateFactory<TService>(binding, address);

            var service = factory.CreateChannel(address);

            return service;
        }
    }
}
