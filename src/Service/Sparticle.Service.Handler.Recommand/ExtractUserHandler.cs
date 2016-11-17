using Sparticle.Request;
using Sparticle.Result;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparticle.Service.Handler.Recommand
{
    [Export(typeof(IRequestHandler))]
    public class ExtractUserHandler : RequestHandlerBase
    {
        public override void BeforeHandle<TRequest>(InspectContext<TRequest> inspectCxt)
        {
            string userId;

            if (TryGetUserId(inspectCxt, out userId))
            {
                inspectCxt.Trace.User = userId;
            }
        }

        public override void AfterHandle<TRequest>(InspectContext<TRequest> inspectCxt, ApiResult result)
        {

        }

        private bool TryGetUserId<TRequest>(InspectContext<TRequest> inspectCxt, out string userId)
        {
            var userRequest = inspectCxt.Request as IUserRequest;

            userId = null;
            
            if (userRequest != null && !string.IsNullOrWhiteSpace(userRequest.UserId))
            {
                userId = userRequest.UserId;
            }

            return !string.IsNullOrWhiteSpace(userId);
        }
    }
}
