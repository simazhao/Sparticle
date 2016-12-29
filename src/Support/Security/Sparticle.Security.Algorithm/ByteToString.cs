using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparticle.Security.Algorithm
{
    static class ByteToString
    {
        public static string Convert(byte[] hash)
        {
            return ConvertToString1(hash);
        }

        private static string ConvertToString1(byte[] hash)
        {
            char[] charArray = new char[hash.Length * 2];
            int hashIndex = 0;

            for (int i = 0; i < charArray.Length; i += 2)
            {
                int num = hash[hashIndex++];
                charArray[i] = GetHexValue(num / 16, true);
                charArray[i + 1] = GetHexValue(num % 16, true);
            }

            return new string(charArray);
        }

        private static string ConvertToString2(byte[] hash)
        {
            var sb = new StringBuilder();

            foreach (var hashbyte in hash)
            {
                sb.Append(hashbyte.ToString("x2"));
            }

            return sb.ToString();
        }

        private static string ConvertToString3(byte[] hash)
        {
            return BitConverter.ToString(hash).Replace("-", "").ToLower();
        }

        private static char GetHexValue(int i, bool lower = false)
        {
            if (i < 10)
                return (char)(i + 48);
            else
                return (char)(i - 10 + 65 + (lower ? 32 : 0));
        }
    }
}
