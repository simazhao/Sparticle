using FundTrade.API.Service.Common;
using Sparticle.Config.LocalSetting;
using Sparticle.Service.Handler;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparticle.Service
{
    class RequestHandlerLoader
    {
        public RequestHandlerLoader()
        {

        }

        private readonly static object Lock = new object();

        private static RequestHandlerLoader _instance;
        public static RequestHandlerLoader Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock(Lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new RequestHandlerLoader();

                            _instance.LoadRequestHandlers();

                            _instance._handlersOrder = LoadHandlersOrder();  
                        }
                    }
                }

                return _instance;
            }
        }

        [ImportMany]
        private IEnumerable<IRequestHandler> _handlers;

        public IEnumerable<IRequestHandler> Handlers
        {
            get
            {
                return _handlers;
            }
        }

        private void LoadRequestHandlers()
        {
            string configDllPath = LocalConfig.HandlerPluginDir;

            var catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new DirectoryCatalog(configDllPath));

            var container = new CompositionContainer(catalog);
            container.ComposeParts(this);
        }

        IDictionary<string, int> _handlersOrder;
        public IDictionary<string, int>  HandlersOrder
        {
            get
            {
                return _handlersOrder;
            }
        }

        private static IDictionary<string, int> LoadHandlersOrder()
        {
            var sections = ConfigurationManager.GetSection("RequestHandlerOrderSection") as RequestHandlerOrderSection;

            if (sections == null)
                throw new ConfigurationErrorsException("do not config [RequestHandlerOrderSection]");

            var ret = new Dictionary<string, int>();

            foreach (var key in sections.RequestConfigHanlders.AllKeys)
            {
                var section = sections.RequestConfigHanlders[key];

                ret[section.Handler] = section.Order;
            }

            return ret;
        }
    }
}
