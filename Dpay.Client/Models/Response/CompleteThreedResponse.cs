using System.Collections.Generic;
using Dpay.Client.Helpers;

namespace Dpay.Client.Models.Response
{
    public class CompleteThreedResponse : BaseResponse
    {
        public Dictionary<string, string> ThreeDRequestParameters { get; set; }
        public string ThreeDHostAddress { get; set; }
        public string SuccessUrl { get; set; }
        public string FailUrl { get; set; }
        public string TransactionId { get; set; }
        public string TransactionTime { get; set; }
        public string Currency { get; set; }
        public string Signature { get; set; }
        public IList<Extra> Extras { get; set; }
        public int TransactionType { get; set; }
        public int? Installment { get; set; }
        public string ApiKey { get; set; }
        public int Amount { get; set; }
        public bool SaveCreditCard { get; set; }
        public string CardId { get; set; }
        public string HashVersion { get; set; }

        internal void ControlSignature(string secretKey)
        {
            var transactionId = (!string.IsNullOrEmpty(this.TransactionId) ? this.TransactionId : "");
            var transactionTime = (!string.IsNullOrEmpty(this.TransactionTime) ? this.TransactionTime : "");
            var currency = (!string.IsNullOrEmpty(this.Currency) ? this.Currency : "");
            var messageCode = (!string.IsNullOrEmpty(this.MessageCode) ? this.MessageCode : "");
            var installmentCount = Installment.HasValue ? this.Installment : null;

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