using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dpay.Client.Helpers;

namespace Dpay.Client.Models.Response
{
    public class CommonPaymentQueryResponse : BaseResponse
    {
        public int? Amount { get; set; }
        public string ApiKey { get; set; }
        public string CommonPaymentUniqueId { get; set; }
        public string Currency { get; set; }
        public int? Installment { get; set; }
        public string Signature { get; set; }
        public string TransactionId { get; set; }
        public string TransactionTime { get; set; }
        public int? Status { get; set; }
        public string PaymentTransactionResponseCode { get; set; }
        public string PaymentTransactionReponseMessage { get; set; }

        internal void ControlSignature(string secretKey, string hashVersion)
        {
            var transactionId = (!string.IsNullOrEmpty(this.TransactionId) ? this.TransactionId : "");
            var transactionTime = (!string.IsNullOrEmpty(this.TransactionTime) ? this.TransactionTime : "");
            var currency = (!string.IsNullOrEmpty(this.Currency) ? this.Currency : "");
            var installment = (this.Installment.HasValue ? this.Installment.ToString() : "");
            var commonPaymentUniqueId = (!string.IsNullOrEmpty(this.CommonPaymentUniqueId)
                ? this.CommonPaymentUniqueId
                : "");
            var paymentTransactionReponseMessage = (!string.IsNullOrEmpty(this.PaymentTransactionReponseMessage)
                ? this.PaymentTransactionReponseMessage
                : "");
            var text = secretKey +
                       transactionId + transactionTime + (this.Amount.HasValue ? this.Amount.Value.ToString() : "") + currency + installment + commonPaymentUniqueId +
                       paymentTransactionReponseMessage;

            string signature;
            if (hashVersion == "1.1")
                signature = CryptographyHelper.HMacSha512(text, secretKey);
            else
                signature = CryptographyHelper.HashSha256(text);

            if (signature != this.Signature)
            {
                throw new DpayClientException(Constants.SignatureVerificationError,
                    Constants.SignatureVerificationError.ReturnErrorMessage(),
                    Constants.SignatureVerificationError.ReturnUserErrorMessage());
            }
        }
    }
}
