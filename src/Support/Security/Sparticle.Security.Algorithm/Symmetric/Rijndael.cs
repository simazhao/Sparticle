using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparticle.Security.Algorithm.Symmetric
{
    class RIjndael : SymmetricEncrypt
    {
        public RIjndael() // avoid name conflict
        {
            Algo = "System.Security.Cryptography.Rijndael";
        }

        public override byte[] GetIv(byte[] seed)
        {
            if (seed.Length == 16)
                return seed;

            var iv = new Byte[16];

            if (seed.Length == 8)
            {
                Array.Copy(seed, 0, iv, 0, seed.Length);
                Array.Copy(seed, 0, iv, seed.Length, seed.Length);
            }
            else
            {
                Array.Copy(seed, iv, iv.Length);
            }

            return iv;
        }
    }
}
