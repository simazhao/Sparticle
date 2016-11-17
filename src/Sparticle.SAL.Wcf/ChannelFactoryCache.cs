using System.Collections.Concurrent;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;

namespace Sparticle.SAL.Wcf
{
    internal static class ChannelFactoryCache
    {
        private readonly static ConcurrentDictionary<string, IChannelFactory> Cache = new ConcurrentDictionary<string, IChannelFactory>();

        public static ChannelFactory<TChannel> CreateFactory<TChannel>(Binding binding, EndpointAddress address)
        {
            var key = MakeCacheKey<TChannel>(binding, address);

            var factoryo = Cache.GetOrAdd(key, s =>
            {
                var factory = new ChannelFactory<TChannel>(binding, address);

                foreach (OperationDescription op in factory.Endpoint.Contract.Operations)
                {
                    var dataContractBehavior = op.Behaviors.Find<DataContractSerializerOperationBehavior>();
                    if (dataContractBehavior != null)
                    {
                        dataContractBehavior.MaxItemsInObjectGraph = 2147483647;
                    }
                }

                return factory;
            });


            return factoryo as ChannelFactory<TChannel>;
        }

        private static string MakeCacheKey<TChannel>(Binding binding, EndpointAddress address)
        {
            return string.Format("{0}-{1}-{2}", typeof(TChannel).Name, address.Uri.AbsolutePath, binding.Name);
        }
    }
}