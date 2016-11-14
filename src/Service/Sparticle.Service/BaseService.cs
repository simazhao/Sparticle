using Sparticle.Common;
using Sparticle.Config.Types;
using Sparticle.Request.Context;
using Sparticle.Result;
using Sparticle.Service.Handler;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparticle.Service
{
    public abstract class BaseService
    {
        private readonly IRequestHandler _handler = null;

        public BaseService(string apiName, string hostType)
        {
            this.ApiName = apiName;
            this.HostType = hostType;

            _handler = CreateRequestHandler();
        }

        public string ApiName { get; private set; }
        public string HostType { get; private set; }
        public string Domain { get; protected set; }

        public ApiResult HandleRequest<TRequest, TResponse>(string apiActionName, TRequest request, RequestContext reqContext,
            Func<TRequest, IFullTrace, ApiResult<TResponse>> handler, AccessLevel defaultAccessLevel=AccessLevel.NoLimit)
        {
            ServiceSession.Current.RequestContext = reqContext;

            using (var trace = CreateTrace())
            {
                trace.Method = apiActionName;

                var inspectContext = new InspectContext<TRequest>
                {
                    Request = request,
                    RequestContext = reqContext,
                    Trace = trace,
                    AccessLevel = defaultAccessLevel,
                    ApiName = this.ApiName,
                    Domain = this.Domain,
                };

                var fullActionName = string.Format("{0}.{1}.{2}", ApiName, apiActionName, Domain);

                MakeupInpectContext(inspectContext, fullActionName);

                return _handler.Handle(inspectContext, handler);
            }
        }

        private RequestHandlerManager CreateRequestHandler()
        {
            var handler = new RequestHandlerManager();

            var otherhandlers = RequestHandlerLoader.Instance.Handlers;

            otherhandlers = OrderRequestHandlers(otherhandlers);

            foreach (var otherhandler in otherhandlers)
            {
                handler.PushBack(otherhandler);
            }

            return handler;
        }

        private IOrderedEnumerable<IRequestHandler> OrderRequestHandlers(IEnumerable<IRequestHandler> handlers)
        {
            return handlers.OrderBy(handler =>
            {
                var handlerType = handler.GetType().FullName;
                if (!RequestHandlerLoader.Instance.HandlersOrder.ContainsKey(handlerType))
                {
                    throw new ConfigurationErrorsException(string.Format("do not config ({0})'s order in [RequestHandlerOrderSections]", handlerType));
                }

                return RequestHandlerLoader.Instance.HandlersOrder[handlerType];
            });
        }

        private IFullTrace CreateTrace()
        {
            throw new NotImplementedException();
        }

        private void MakeupInpectContext(InspectContext context, string actionName)
        {
            context.ActionConfig = RetrieveActionConfig(actionName);

            if (context.Trace != null)
            {
                context.Trace.StepTrace.SerializeResult = false;
                if (context.ActionConfig != null)
                {
                    context.Trace.StepTrace.SerializeResult = context.ActionConfig.ServiceDebug;
                }

                // todo: add core & cache handle inject later
                // CoreAccessBase.Trace = context.Trace;
                // CachedDataHelper.Trace = context.Trace;
            }
        }

        protected abstract ActionAccessConfig RetrieveActionConfig(string actionName);
    }
}
