using Sparticle.Security.Algorithm.OneWayEncryption;
using Sparticle.Security.Algorithm.Symmetric;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparticle.Security.Algorithm
{
    public class OneWayEncryptionNameCollection
    {
        public static List<string> Names { get; } = OnewayEncryptionCollection.EncryptionMap.Keys.ToList<string>();
    }

    class OnewayEncryptionCollection
    {
        public static readonly Dictionary<string, IOneWayEncryption> EncryptionMap =
            new Dictionary<string, IOneWayEncryption>()
            {
                {nameof(Md5), new Md5() },
                {nameof(Sha1), new Sha1() },
                {nameof(Sha256), new Sha256() },
                {nameof(Sha384), new Sha384() },
                {nameof(Sha512), new Sha512() },
                {nameof(HmacSha1), new HmacSha1() },
                {nameof(HmacSha256), new HmacSha256() },
                {nameof(HmacSha384), new HmacSha384() },
                {nameof(HmacSha512), new HmacSha512() },
            };
    }

    public class DualWayEncryptionNameCollection
    {
        public static List<string> Names { get; } = DualwayEncryptionCollection.EncryptionMap.Keys.ToList<string>();
    }

    class DualwayEncryptionCollection
    {
        public static readonly Dictionary<string, IDualWayEncryption> EncryptionMap =
            new Dictionary<string, IDualWayEncryption>()
            {
                {nameof(Des), new Des() },
                {nameof(Aes), new Aes() },
                {nameof(TripleDes), new TripleDes() },
                {nameof(RIjndael), new RIjndael() },

            };
    }
}
