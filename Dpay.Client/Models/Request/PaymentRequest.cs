using Dpay.Client.Helpers;
using System.Collections.Generic;


namespace Dpay.Client.Models.Request
{
    public class PaymentRequest : BaseRequest
    {
        public string Version { get; set; }
        public string TransactionId { get; set; }
        public string TransactionTime { get; set; }
        public string Signature { get; private set; }
        public string Description { get; set; }
        public int Amount { get; set; }
        public int? PointAmount { get; set; }
        public string Currency { get; set; }
        public int? Installment { get; set; }
        public Card Card { get; set; }
        public IList<BasketItem> BasketItems { get; set; }
        public Customer Customer { get; set; }
        public ShippingAddress ShippingAddress { get; set; }
        public InvoiceAddress InvoiceAddress { get; set; }
        public IList<Extra> Extras { get; set; }
        public bool SaveCreditCard { get; set; }
        public string MerchantUserId { get; set; }
        public string MerchantCardUserId { get; set; }
        public string MerchantCardId { get; set; }

        public bool IsPreAuth { get; set; }
        public string HashVersion { get; set; }

        public int BankId { get; set; }

        public string ReturnUrl { get; set; }

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

