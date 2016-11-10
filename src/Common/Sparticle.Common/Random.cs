using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Sparticle.Common
{
    public class Randomer
    {
        public IEnumerator<int> FastRandom(int min = 1, int max = int.MaxValue)
        {
            var random = new Random((int)DateTime.Now.Ticks);
            int count = 0;
            while (count <= max - min)
            {
                ++count;
                yield return random.Next(min, max);
            }
        }

        public int RealRandom()
        {
            using (RNGCryptoServiceProvider r = new RNGCryptoServiceProvider())
            {
                byte[] bytes = new byte[4];

                r.GetBytes(bytes);

                return Convert.ToInt32(bytes);
            }
        }
    }
}
