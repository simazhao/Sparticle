using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparticle.Security.Algorithm.Symmetric
{
    class Aes : SymmetricEncrypt
    {
        public Aes()
        {
            Algo = "AES";
        }

        public override byte[] GetIv(byte[] seed)
        {
            if (seed.Length == 16)
                return seed;

            var iv = new Byte[16];
            Array.Copy(seed, iv, iv.Length);
            return iv;
        }
    }
}
