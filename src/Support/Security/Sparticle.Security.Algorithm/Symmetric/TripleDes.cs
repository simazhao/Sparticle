using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparticle.Security.Algorithm.Symmetric
{
    public class TripleDes : SymmetricEncrypt
    {
        public TripleDes()
        {
            Algo = "System.Security.Cryptography.TripleDES";
        }

        public override byte[] GetIv(byte[] seed)
        {
            var iv = new byte[8];
            Array.Copy(seed, iv, iv.Length);
            return iv;
        }
    }
}
