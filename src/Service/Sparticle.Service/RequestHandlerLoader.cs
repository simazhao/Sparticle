using FundTrade.API.Service.Common;
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
        public static readonly string PluginDirName = "Handlers";

        public RequestHandlerLoader()
        {

        }

        private static RequestHandlerLoader _instance;
        public static RequestHandlerLoader Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock(PluginDirName)
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
            string configDllPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, PluginDirName);

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
            var sections = ConfigurationManager.GetSection("RequestHandlerOrderSections") as RequestHandlerOrderSections;

            if (sections == null)
                throw new ConfigurationErrorsException("do not config [RequestHandlerOrderSections]");

            var ret = new Dictionary<string, int>();

            foreach (var key in sections.RequestHandlerOrderSection.AllKeys)
            {
                var section = sections.RequestHandlerOrderSection[key];

                ret[section.Handler] = section.Order;
            }

            return ret;
        }
    }
}
