using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Sparticle.Security.Algorithm.Symmetric
{
    public abstract class SymmetricEncrypt : IDualWayEncryption
    {
        public string Algo { get; protected set; }

        public abstract byte[] GetIv(byte[] seed);

        public string Encryption(Encoding encode, string text, string key)
        {
            using (var encryptAlgo = SymmetricAlgorithm.Create(Algo))
            {
                Byte[] bkey = encode.GetBytes(key);
                Byte[] biv = GetIv(bkey);

                ICryptoTransform encryptor = encryptAlgo.CreateEncryptor(bkey, biv);
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (var cs = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter writer = new StreamWriter(cs))
                        {
                            writer.Write(text);
                            writer.Flush();
                        }
                    }

                    var encryptedData = memoryStream.ToArray();

                    return Convert.ToBase64String(encryptedData);
                }
            }
        }

        public string Decryption(Encoding encode, string codes, string key)
        {
            using (var encryptAlgo = SymmetricAlgorithm.Create(Algo))
            {
                string resultData = string.Empty;
                Byte[] encryptedData = Convert.FromBase64String(codes);

                Byte[] bkey = encode.GetBytes(key);
                Byte[] biv = GetIv(bkey);

                ICryptoTransform decryptor = encryptAlgo.CreateDecryptor(bkey, biv);
                using (var memoryStream = new MemoryStream(encryptedData))
                {
                    using (var cs = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader reader = new StreamReader(cs))
                        {
                            resultData = reader.ReadLine();

                            return resultData;
                        }
                    }
                }
            }
        }
    }
}
