using Dpay.Client.Helpers;

namespace Dpay.Client.Models.Request
{
    public class CompleteThreedRequest : BaseRequest
    {
        public string EncryptedThreedResult { get; set; }
        public string Signature { get; set; }
        public string HashVersion { get; set; }

        internal void SetSignature(string secretKey)
        {
            var encryptedThreedResult = (!string.IsNullOrEmpty(this.EncryptedThreedResult) ? this.EncryptedThreedResult : "");
            var text = secretKey + encryptedThreedResult;

            if (HashVersion == "1.1")
                Signature = CryptographyHelper.HMacSha512(text, secretKey);
            else
                Signature = CryptographyHelper.HashSha256(text);
        }
    }
}