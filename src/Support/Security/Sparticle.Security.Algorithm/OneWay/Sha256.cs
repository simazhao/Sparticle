using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparticle.Security.Algorithm.OneWayEncryption
{
    public class Sha256 : IOneWayEncryption
    {
        public string Encrypt(Encoding encode, string text, string key = null)
        {
            using (var sha = System.Security.Cryptography.SHA256.Create())
            {
                var hash = sha.ComputeHash(encode.GetBytes(text));

                return ByteToString.Convert(hash);
            }
        }
    }
}
