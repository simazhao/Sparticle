using Sparticle.Security.Attack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparticle.Service.Handler.Security
{
    class AuthKeyContainer : IAuthKeyContainer
    {
        public string GetAuthKey(string algo, string id)
        {
            throw new NotImplementedException();
        }
    }
}
