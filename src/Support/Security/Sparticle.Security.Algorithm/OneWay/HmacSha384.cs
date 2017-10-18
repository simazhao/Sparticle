using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparticle.Security.Algorithm.OneWayEncryption
{
    public class HmacSha384 : IOneWayEncryption
    {
        public string Encrypt(Encoding encode, string text, string key)
        {
            using (var sha = new System.Security.Cryptography.HMACSHA384(encode.GetBytes(key)))
            {
                var hash = sha.ComputeHash(encode.GetBytes(text));

                return ByteToString.Convert(hash);
            }
        }
    }
}
