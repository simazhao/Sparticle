﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparticle.Security.Algorithm.OneWayEncryption
{
    class HmacSha256 : IOneWayEncryption
    {
        public string Encrypt(Encoding encode, string text, string key)
        {
            using (var sha = new System.Security.Cryptography.HMACSHA256(encode.GetBytes(key)))
            {
                var hash = sha.ComputeHash(encode.GetBytes(text));

                return ByteToString.Convert(hash);
            }
        }
    }
}
