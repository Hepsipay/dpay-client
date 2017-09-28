using System.Collections.Generic;
using Dpay.Client.Helpers;

namespace Dpay.Client.Models.Response
{
    public class FailedThreedResponse : BaseResponse
    {
        public string TransactionId { get; set; }
        public string Installment { get; set; }
        public string Currency { get; set; }
        public string ApiKey { get; set; }
        public string Amount { get; set; }
        public string Signature { get; set; }
        public string TransactionTime { get; set; }
        public IList<Extra> Extras { get; set; }
        public string BankResponseCode { get; set; }
        public string BankResponseMessage { get; set; }
        public string HashVersion { get; set; }

        internal void ControlSignature(string secretKey)
        {
            var transactionId = (!string.IsNullOrEmpty(this.TransactionId) ? this.TransactionId : "");
            var transactionTime = (!string.IsNullOrEmpty(this.TransactionTime) ? this.TransactionTime : "");
            var currency = (!string.IsNullOrEmpty(this.Currency) ? this.Currency : "");
            var messageCode = (!string.IsNullOrEmpty(this.MessageCode) ? this.MessageCode : "");
            var installmentCount = !string.IsNullOrEmpty(this.Installment) ? this.Installment : "";

            var text = secretKey + transactionId + transactionTime + this.Amount + currency +
                       installmentCount
                       + messageCode;

            var signature = "";
            if (HashVersion == "1.1")
            {
                signature = CryptographyHelper.HMacSha512(text, secretKey);
            }
            else
            {
                signature = CryptographyHelper.HashSha256(text);
            }

            if (signature != this.Signature)
            {
                throw new DpayClientException(Constants.SignatureVerificationError, Constants.SignatureVerificationError.ReturnErrorMessage(), Constants.SignatureVerificationError.ReturnUserErrorMessage());
            }
        }
    }
}