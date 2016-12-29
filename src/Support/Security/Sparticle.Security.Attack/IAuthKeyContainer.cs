using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparticle.Security.Attack
{
    public interface IAuthKeyContainer
    {
        string GetAuthKey(string algo, string id);
    }
}
