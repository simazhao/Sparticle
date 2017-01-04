using Sparticle.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparticle.Security.Attack
{
    class NoncePool
    {
        private static string NonceKeyPrefix = "Nonce-";
        private static readonly TimeSpan AllowTimeDiff = new TimeSpan(0, 0, 10);
        private static readonly TimeSpan NonceLifeTime = new TimeSpan(1, 0, 0);
        private string cachePolicy = "Couch";

        private IStrictCachePolicy nonceStore
        {
            get
            {
                return CacheInstance.Collection[cachePolicy] as IStrictCachePolicy;
            }
        }

        public enum TimeStampChecked
        {
            Expired,
            InTime,
            Beyond,
        }

        class NonceItem
        {
            public string Nonce { get; set; }

            public string ClientId { get; set; }

            public long TimeStamp { get; set; }
        }

        public bool Save(string nonce, string clientId)
        {
            if (!Exists(nonce, clientId))
            {
                return nonceStore.Cas(MakeKey(nonce, clientId), nonce, new TimeSpan(), 0x12345678);
            }

            return false;
        }

        public bool Exists(string nonce, string clientId)
        {
            return nonceStore.Get<string>(MakeKey(nonce, clientId)) != null;
        }

        private string MakeKey(string nonce, string clientId)
        {
            return string.Format("{0}-{1}-{2}", NonceKeyPrefix, clientId, nonce);
        }

        public TimeStampChecked IsValidTimestamp(long timestamp)
        {
            var localTimeStamp = 1;
            var diff = localTimeStamp - timestamp;

            var dittTs = TimeSpan.FromMilliseconds(Math.Abs(diff));

            if (dittTs <= AllowTimeDiff)
                return TimeStampChecked.InTime;

            return diff > 0 ? TimeStampChecked.Expired : TimeStampChecked.Beyond;
        }
    }
}
