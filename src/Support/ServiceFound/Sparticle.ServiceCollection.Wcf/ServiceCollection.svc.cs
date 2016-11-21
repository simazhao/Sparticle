using Sparticle.Cache;
using Sparticle.ServiceCollection.Interface;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Web;
using System.Text;
using System.Xml.Serialization;

namespace Sparticle.ServiceCollection.Wcf
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class ServiceCollection : IServiceCollection
    {
        private static ServiceCollectionConfig _config = null;
        private static readonly string ConfigDir = ConfigurationManager.AppSettings["ServiceCollectionConfigDir"];
        private static readonly string ConfigFileName = "ServiceCollectionConfig.xml";
        private static readonly TimeSpan MachineServiceCacheTime = TimeSpan.Parse(ConfigurationManager.AppSettings["MachineServiceCacheTime"]);

        static ServiceCollection()
        {
            _config = LoadServiceCollection();
        }

        public ServiceAddressResponse GetServiceAddress(ServiceAddressRequest request)
        {
            var key = string.Format("{0}-{1}-{2}-{3}", GetClientIp(), request.No, request.ServiceGroup,
                request.ServiceIdentity);

            ServiceAddressResponse result;
            if (CacheInstance.Local.TryGet(key, out result))
            {
                return result;
            }

            result = GetServiceAddressImpl(request);

            if (result != null)
            {
                CacheInstance.Local.Add(key, result, MachineServiceCacheTime);
            }

            return result;
        }

        private string GetClientIp()
        {
            OperationContext context = OperationContext.Current;

            var endpoint = context.IncomingMessageProperties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;

            return endpoint.Address;
        }

        public ServiceAddressResponse GetServiceAddressImpl(ServiceAddressRequest request)
        {
            var service = _config.Services.FirstOrDefault(s => Compare(s.Identity, request.ServiceIdentity) &&
                                                               Compare(s.Group ?? string.Empty, request.ServiceGroup ?? string.Empty));

            if (service == null)
                return null;

            var machine = service.Machines.FirstOrDefault(m =>
            {
                if (request.MMode == ServiceAddressRequest.MatchMode.Ip)
                {
                    return Compare(m.ClientIp, GetClientIp());
                }
                else if (request.MMode == ServiceAddressRequest.MatchMode.No)
                {
                    return Compare(m.No, request.No);
                }

                return false;
            });

            if (machine == null)
            {
#if EAGER
                machine = service.Machines.FirstOrDefault(m => Compare(m.No, "Any"));

                if (machine == null)
                {
                    return null;
                }
#else
                return null;
#endif
            }

            var response = new ServiceAddressResponse()
            {
                Address = machine.ServiceAddress,
                PropertyList = service.PropList.ToDictionary(p => p.Key, p => p.Value)
            };

            return response;
        }

        public void Reload()
        {
            _config = LoadServiceCollection();
        }

        private static ServiceCollectionConfig LoadServiceCollection()
        {
            return Load<ServiceCollectionConfig>(ConfigFileName);
        }

        private bool Compare(string stringA, string stringB)
        {
            return string.Compare(stringA, stringB, StringComparison.OrdinalIgnoreCase) == 0;
        }

        private static T Load<T>(string fileName) where T : class
        {
            string configdir = ConfigDir ?? AppDomain.CurrentDomain.BaseDirectory;
            string filePath = Path.Combine(configdir, fileName);
            var ser = new XmlSerializer(typeof(T));

            try
            {
                using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    return ser.Deserialize(fs) as T;
                }
            }
            catch
            {
                return default(T);
            }
        }
    }
}
