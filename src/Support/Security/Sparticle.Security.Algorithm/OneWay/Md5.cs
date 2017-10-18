using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Sparticle.Security.Algorithm.OneWayEncryption
{
    public class Md5 : IOneWayEncryption
    {
        public string Encrypt(Encoding encode, string text, string key)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] hash = md5.ComputeHash(encode.GetBytes(text));

                return ByteToString.Convert(hash);
            }
        }
    }
}
