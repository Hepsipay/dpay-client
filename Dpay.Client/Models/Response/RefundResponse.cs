using System.Collections.Generic;
using Dpay.Client.Helpers;

namespace Dpay.Client.Models.Response
{
    public class RefundResponse : BaseResponse
    {
        public string ApiKey { get; set; }
        public string TransactionId { get; set; }
        public string TransactionTime { get; set; }
        public string Signature { get; set; }
        public int Amount { get; set; }
        public string Currency { get; set; }
        public string ReferenceTransactionId { get; set; }
        public IList<Extra> Extra { get; set; }
        public bool WaitingApproval { get; set; }

        internal void ControlSignature(string secretKey,string version)
        {
            var transactionId = (!string.IsNullOrEmpty(this.TransactionId) ? this.TransactionId : "");
            var referenceTransactionId = (!string.IsNullOrEmpty(this.ReferenceTransactionId)
                ? this.ReferenceTransactionId
                : "");
            var transactionTime = (!string.IsNullOrEmpty(this.TransactionTime) ? this.TransactionTime : "");
            var currency = (!string.IsNullOrEmpty(this.Currency) ? this.Currency : "");
            var messageCode = (!string.IsNullOrEmpty(this.MessageCode) ? this.MessageCode : "");

            var text = secretKey + transactionId + referenceTransactionId + transactionTime + this.Amount + currency +
                       messageCode;


            string signature;
            if (version == "1.1")
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