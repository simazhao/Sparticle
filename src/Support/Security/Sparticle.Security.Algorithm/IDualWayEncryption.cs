using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparticle.Security.Algorithm
{
    public interface IDualWayEncryption
    {
        string Encryption(Encoding encode, string text, string key);

        string Decryption(Encoding encode, string codes, string key);
    }
}
