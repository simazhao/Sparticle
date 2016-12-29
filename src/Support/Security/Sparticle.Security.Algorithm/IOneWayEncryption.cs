using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparticle.Security.Algorithm
{
    public interface IOneWayEncryption
    {
        string Encrypt(Encoding encode, string text, string key=null);
    }
}
