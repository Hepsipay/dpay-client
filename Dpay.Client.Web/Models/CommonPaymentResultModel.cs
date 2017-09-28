using System.Collections.Generic;

namespace Dpay.Client.Web.Models
{
    public class CommonPaymentResultModel
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
        public IDictionary<string, string> Extras { get; set; }
        public string ReturnUrl { get; set; }
        public string TransactionType { get; set; }

        public bool Success { get; set; }
        public string MessageCode { get; set; }
        public string Message { get; set; }
        public string UserMessage { get; set; }
    }
}