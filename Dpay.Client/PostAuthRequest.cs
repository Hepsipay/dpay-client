using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dpay.Client.Helpers;
using Dpay.Client.Models;
using Dpay.Client.Models.Request;

namespace Dpay.Client
{
    public class PostAuthRequest:BaseRequest
    {
        public string ReferenceTransactionId { get; set; }

        public string Currency { get; set; }

        public string Version { get; set; }
        public string TransactionId { get; set; }
        public string TransactionTime { get; set; }
        public string Signature { get; set; }
        public string MerchantIpAddress { get; set; }
        public int Amount { get; set; }
        public int? Installment { get; set; }
        public Customer Customer { get; set; }
        public string HashVersion { get; set; }

        internal void SetSignature(string secretKey)
        {
            var transactionId = (!string.IsNullOrEmpty(this.TransactionId) ? this.TransactionId : "");
            var referenceTransactionId = (!string.IsNullOrEmpty(this.ReferenceTransactionId)
                ? this.ReferenceTransactionId
                : "");
            var transactionTime = (!string.IsNullOrEmpty(this.TransactionTime) ? this.TransactionTime : "");
            var currency = (!string.IsNullOrEmpty(this.Currency) ? this.Currency : "");

            var installmentCount = Installment.HasValue ? this.Installment : null;
            var text = secretKey + transactionId + referenceTransactionId + transactionTime + this.Amount + currency +
                       installmentCount;

            if (HashVersion == "1.1")
                Signature = CryptographyHelper.HMacSha512(text, secretKey);
            else
                Signature = CryptographyHelper.HashSha256(text);
        }
    }
}
