using Dpay.Client.Helpers;

namespace Dpay.Client.Models.Request
{
    public class PointQueryRequest : BaseRequest
    {
        public string Version { get; set; }
        public string TransactionId { get; set; }
        public string TransactionTime { get; set; }
        public string Signature { get; set; }

        public string Description { get; set; }
        public int Amount { get; set; }

        public string Currency { get; set; }


        public Card Card { get; set; }
        public string HashVersion { get; set; }

        public void SetSignature(string secretKey)
        {
            var transactionId = (!string.IsNullOrEmpty(this.TransactionId) ? this.TransactionId : "");
            var transactionTime = (!string.IsNullOrEmpty(this.TransactionTime) ? this.TransactionTime : "");
            var currency = (!string.IsNullOrEmpty(this.Currency) ? this.Currency : "");

            var text = secretKey + transactionId + transactionTime + "100" + currency;

            if (HashVersion == "1.1")
                Signature = CryptographyHelper.HMacSha512(text, secretKey);
            else
                Signature = CryptographyHelper.HashSha256(text);
        } 
    }
}