using System.Collections.Generic;
using Dpay.Client.Helpers;

namespace Dpay.Client.Models.Request
{
    public class SaveCommonPaymentRequest : BaseRequest
    {
        public string Version { get; set; }
        public string TransactionId { get; set; }
        public string TransactionTime { get; set; }
        public string Signature { get; set; }
        public string Description { get; set; }
        public bool AmountEditable { get; set; }
        public int? Amount { get; set; }
        public string Currency { get; set; }
        public IList<BasketItem> BasketItems { get; set; }
        public Customer Customer { get; set; }
        public ShippingAddress ShippingAddress { get; set; }
        public InvoiceAddress InvoiceAddress { get; set; }
        public IList<Extra> Extras { get; set; }
        public string ReturnUrl { get; set; }
        public string HashData { get; set; }
        public bool HasInstallmentChoice { get; set; }
        public int CommissionAmountReflect { get; set; } // EndUser = 1, Merchant = 2
        public int? Installment { get; set; }
        public string HashVersion { get; set; }

        internal void SetSignature(string secretKey)
        {
            var transactionId = (!string.IsNullOrEmpty(this.TransactionId) ? this.TransactionId : "");
            var transactionTime = (!string.IsNullOrEmpty(this.TransactionTime) ? this.TransactionTime : "");
            var currency = (!string.IsNullOrEmpty(this.Currency) ? this.Currency : "");
            var installment = (this.Installment.HasValue ? this.Installment.Value.ToString() : "");

            var text = secretKey + transactionId + transactionTime + this.Amount + currency + installment;

            if (HashVersion == "1.1")
                Signature = CryptographyHelper.HMacSha512(text, secretKey);
            else
                Signature = CryptographyHelper.HashSha256(text);
        }
    }
}
