using Sparticle.Security.Algorithm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Sparticle.Security.Attack.NoncePool;

namespace Sparticle.Security.Attack
{
    public class ReplayAttackDefender
    {
        IAuthKeyContainer authKeyContainer;
        static NoncePool noncePool = new NoncePool();

        public ReplayAttackDefender(IAuthKeyContainer authKeyContainer)
        {
            this.authKeyContainer = authKeyContainer;
        }

        public bool Defend(ReplayAttackParam raParam, string authAlgo, out string error)
        {
            error = null;

            var authKey = this.authKeyContainer.GetAuthKey(authAlgo, raParam.ClientId);

            if (string.IsNullOrEmpty(authKey))
            {
                error = "client id is not matched";
                return false;
            }

            // make own signature
            var signature = MakeSignature(authAlgo, raParam.ClientId, raParam.Nonce, raParam.TimeStamp, raParam.Message);

            if (signature != raParam.Signature)
            {
                error = "request param has been modified";
                return false;
            }

            if (noncePool.Exists(raParam.Nonce))
            {
                error = "replay attack detected";
                return false;
            }

            var tsCheck = noncePool.IsValidTimestamp(raParam.TimeStamp);

            if (tsCheck == TimeStampChecked.InTime)
            {
                var saved = noncePool.Save(raParam.Nonce);

                if (!saved)
                {
                    error = "fail to save nonce, maybe has the same nonce.";
                    return false;
                }
            }
            else
            {
                error = ReportError(tsCheck);
                return false;
            }

            return true;
        }

        private string MakeSignature(string authAlgo, string clientId, string nonce, long timeStamp, string message)
        {
            var encode = Encoding.Default;

            var algorithm = EncryptionFactory.Instance.GetOneWayEncryption(authAlgo);

            var text = string.Format("{0}-{1}-{2}-{3}", clientId, nonce, timeStamp, message);

            var encryptText = algorithm.Encrypt(encode, text);

            return encryptText;
        }

        private string ReportError(TimeStampChecked tsCheck)
        {
            if (tsCheck == TimeStampChecked.Expired)
            {
                return "client timestamp was illegal";
            }

            if (tsCheck == TimeStampChecked.Beyond)
            {
                return "client timestamp was illegal";
            }

            return "";
        }
    }
}
