using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparticle.Security.Algorithm
{
    public class EncryptionFactory
    {
        public static readonly EncryptionFactory Instance = new EncryptionFactory();

        public IOneWayEncryption GetOneWayEncryption(string algo)
        {
            return OnewayEncryptionCollection.EncryptionMap[algo];
        }

        public IDualWayEncryption GetEncryption(string algo)
        {
            return DualwayEncryptionCollection.EncryptionMap[algo];
        }
    }
}
