using Dpay.Client.Helpers;
using System;
using System.Collections.Generic;

namespace Dpay.Client.Models.Request
{
    public class RefundRequest : BaseRequest
    {
        public string Version { get; set; }
        public string HashVersion { get; set; }
        public string TransactionId { get; set; }
        public string ReferenceTransactionId { get; set; }
        public string TransactionTime { get; set; }
        public string Signature { get; private set; }
        public int Amount { get; set; }
        public string Currency { get; set; }
        public Customer Customer { get; set; }
        public IList<RefundBasketItem> BasketItems { get; set; }
        public int ApprovalAmountLimit { get; set; }
        public Guid UserId { get; set; }

        internal void SetSignature(string secretKey)
        {
            var transactionId = (!string.IsNullOrEmpty(this.TransactionId) ? this.TransactionId : "");
            var referenceTransactionId = (!string.IsNullOrEmpty(this.ReferenceTransactionId)
                ? this.ReferenceTransactionId
                : "");
            var transactionTime = (!string.IsNullOrEmpty(this.TransactionTime) ? this.TransactionTime : "");
            var currency = (!string.IsNullOrEmpty(this.Currency) ? this.Currency : "");
            var text = secretKey + transactionId + referenceTransactionId + transactionTime + this.Amount + currency;

            if (HashVersion == "1.1")
                Signature = CryptographyHelper.HMacSha512(text, secretKey);
            else
                Signature = CryptographyHelper.HashSha256(text);
        }
    }
}