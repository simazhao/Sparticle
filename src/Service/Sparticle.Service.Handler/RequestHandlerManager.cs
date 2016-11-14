using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sparticle.Common;
using Sparticle.Result;

namespace Sparticle.Service.Handler
{
    public class RequestHandlerManager : IRequestHandler
    {
        private IRequestHandler Tail2 = null;
        private IRequestHandler Head = null;

        public RequestHandlerManager()
        {
            Head = new TailRequstHandler();
        }

        /// <summary>
        /// add new handler before tail
        /// </summary>
        /// <param name="handler"></param>
        public void PushBack(IRequestHandler handler)
        {
            if (Tail2 == null)
            {
                handler.Next = Head;
                Tail2 = Head = handler;
            }
            else
            {
                handler.Next = Tail2.Next;
                Tail2.Next = handler;
                Tail2 = handler;
            }
        }

        public ApiResult Handle<TRequest>(InspectContext<TRequest> inspectCxt, Func<TRequest, IFullTrace, ApiResult> finalFunc)
        {
            return Head.Handle(inspectCxt, finalFunc);
        }

        public IRequestHandler Next { get; set; }

    }
}
