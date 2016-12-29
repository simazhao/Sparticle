using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparticle.Security.Algorithm.Symmetric
{
    class Des : SymmetricEncrypt
    {
        public Des()
        {
            Algo = "System.Security.Cryptography.DES";
        }

        public override byte[] GetIv(byte[] seed)
        {
            return seed;
        }
    }
}
