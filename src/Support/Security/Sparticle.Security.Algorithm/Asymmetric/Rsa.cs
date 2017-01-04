using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Sparticle.Security.Algorithm.Symmetric
{
    class Rsa : IDualWayEncryption
    {
        public string Encryption(Encoding encode, string text, string key)
        {
            using (var provider = new RSACryptoServiceProvider())
            {
                provider.FromXmlString(key);

                var encryptedData = provider.Encrypt(encode.GetBytes(text), false);

                return Convert.ToBase64String(encryptedData);
            }
        }

        public string Decryption(Encoding encode, string codes, string key)
        {
            using (var provider = new RSACryptoServiceProvider())
            {
                provider.FromXmlString(key);

                var decryptedData = provider.Encrypt(Convert.FromBase64String(codes), false);

                return encode.GetString(decryptedData);
            }
        }
    }
}
