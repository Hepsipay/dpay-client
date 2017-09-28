using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dpay.Client.Helpers;
using Dpay.Client.Models;
using Dpay.Client.Models.Response;

namespace Dpay.Client
{
    public class PostAuthResponse:BaseResponse
    {
        public string ApiKey { get; set; }
        public string TransactionId { get; set; }
        public string TransactionTime { get; set; }
        public string Signature { get; set; }
        public int Amount { get; set; }
        public string Currency { get; set; }
        public string ReferenceTransactionId { get; set; }
        public IList<Extra> Extra { get; set; }
        public string HashVersion { get; set; }

        public int? Installment { get; set; }
        internal void ControlSignature(string secretKey)
        {
            var transactionId = (!string.IsNullOrEmpty(this.TransactionId) ? this.TransactionId : "");
            var referenceTransactionId = (!string.IsNullOrEmpty(this.ReferenceTransactionId)
                ? this.ReferenceTransactionId
                : "");
            var transactionTime = (!string.IsNullOrEmpty(this.TransactionTime) ? this.TransactionTime : "");
            var currency = (!string.IsNullOrEmpty(this.Currency) ? this.Currency : "");
            var messageCode = (!string.IsNullOrEmpty(this.MessageCode) ? this.MessageCode : "");
            var installmentCount = Installment.HasValue ? this.Installment : null;

            var text = secretKey + transactionId + referenceTransactionId + transactionTime + this.Amount + currency +
                       installmentCount
                       + messageCode;

            var signature = "";
            if (HashVersion == "1.1")
                signature = CryptographyHelper.HMacSha512(text, secretKey);
            else
                signature = CryptographyHelper.HashSha256(text);

            if (signature != this.Signature)
            {
                throw new DpayClientException(Constants.SignatureVerificationError, Constants.SignatureVerificationError.ReturnErrorMessage(), Constants.SignatureVerificationError.ReturnUserErrorMessage());
            }
        }
    }
}
