using Dpay.Client.Helpers;
using System;

namespace Dpay.Client.Models.Request
{
    public class DeliveryApprovalRequest : BaseRequest
    {
        public string Version { get; set; }
        public string HashVersion { get; set; }
        public string TransactionId { get; set; }
        public string TransactionTime { get; set; }
        public string Signature { get; set; }
        public string BasketItemId { get; set; }
        public int EarnedAmount { get; set; }
        public void SetSignature(string secretKey)
        {
            var transactionId = (!string.IsNullOrEmpty(TransactionId) ? TransactionId : "");
            var transactionTime = (!string.IsNullOrEmpty(TransactionTime) ? TransactionTime : "");
            var text = secretKey + transactionId + transactionTime + EarnedAmount;
            
            if (HashVersion == "1.1")
                Signature = CryptographyHelper.HMacSha512(text, secretKey);
            else
                Signature = CryptographyHelper.HashSha256(text);
        }
    }
}