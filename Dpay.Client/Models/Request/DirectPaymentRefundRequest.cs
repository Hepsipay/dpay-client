using Dpay.Client.Helpers;

namespace Dpay.Client.Models.Request
{
    public class DirectPaymentRefundRequest : BaseRequest
    {
        public string Version { get; set; }
        public string TransactionId { get; set; }
        public string ReferenceTransactionId { get; set; }
        public string TransactionTime { get; set; }
        public string Signature { get; set; }
        public int Amount { get; set; }
        public string Currency { get; set; }
        public string MerchantIP { get; set; }

        internal void SetSignature(string secretKey)
        {
            //var transactionId = (!string.IsNullOrEmpty(this.TransactionId) ? this.TransactionId : "");
            var referenceTransactionId = (!string.IsNullOrEmpty(this.ReferenceTransactionId)
                ? this.ReferenceTransactionId
                : "");
            var merchantOrderId = (!string.IsNullOrEmpty(this.TransactionId)
                ? this.TransactionId
                : ""); ;
            var transactionTime = (!string.IsNullOrEmpty(this.TransactionTime) ? this.TransactionTime : "");
            var currency = (!string.IsNullOrEmpty(this.Currency) ? this.Currency : "");
            var text = referenceTransactionId + secretKey + merchantOrderId + transactionTime + this.Amount + currency;

            if (Version == "1.1")
                Signature = CryptographyHelper.HMacSha512(text, secretKey);
            else
                Signature = CryptographyHelper.HashSha256(text);
        }
    }
}
