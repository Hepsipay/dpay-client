using System.Collections.Generic;
using Dpay.Client.Helpers;

namespace Dpay.Client.Models.Response
{
    public class PaymentResponse : BaseResponse
    {
        public string ApiKey { get; set; }

        public string TransactionId { get; set; }

        public string TransactionTime { get; set; }

        public string Signature { get; set; }
        public int Amount { get; set; }

        public string Currency { get; set; }

        public int? Installment { get; set; }

        public IList<Extra> Extras { get; set; }

        public string PaymentUrl { get; set; }

        public string CardId { get; set; }

        internal void ControlSignature(string secretKey, string version)
        {
            var transactionId = (!string.IsNullOrEmpty(this.TransactionId) ? this.TransactionId : "");
            var transactionTime = (!string.IsNullOrEmpty(this.TransactionTime) ? this.TransactionTime : "");
            var currency = (!string.IsNullOrEmpty(this.Currency) ? this.Currency : "");
            var installment = (this.Installment.HasValue ? this.Installment.ToString() : "");
            var messageCode = (!string.IsNullOrEmpty(this.MessageCode) ? this.MessageCode : "");
            var text = secretKey +
                       transactionId + transactionTime + this.Amount + currency + installment + messageCode;


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