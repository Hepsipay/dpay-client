using System.Collections.Generic;
using Dpay.Client.Helpers;

namespace Dpay.Client.Models.Request
{
    public class CreateThreedRequest : BaseRequest
    {
        public string Version { get; set; }
        public string TransactionId { get; set; }
        public string TransactionTime { get; set; }
        public string Signature { get; set; }
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
        public string SuccessUrl { get; set; }
        public string FailUrl { get; set; }
        public bool SaveCreditCard { get; set; }
        public string MerchantCardId { get; set; }
        public string MerchantCardUserId { get; set; }
        public string MerchantUserId { get; set; }
        public string ThreedReturnUrl { get; set; }
        public bool IsCommonPayment { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public int? AmountWithoutTax { get; set; }
        public int? Priority { get; set; }
        public int? DeliveryDate { get; set; }
        public int? DiscountAmount { get; set; }
        public int? GiftCheqAmount { get; set; }
        public string GiftCheqCode { get; set; }
        public string GiftCheqType { get; set; }
        public int? PlannedShipmentAmount { get; set; }
        public int? AppliedShipmentAmount { get; set; }
        public string VisitorId { get; set; }
        public string UserKey { get; set; }
        public int? SalesChannel { get; set; }
        public bool? Opc { get; set; }
        public string OpcChannel { get; set; }
        public string Source { get; set; }
        public string Platform { get; set; }
        public string OperatingSystem { get; set; }
        public string Device { get; set; }
        public string BasketId { get; set; }
        public bool? IsPaymentApproved { get; set; }
        public bool IsPreAuth { get; set; }
        public string HashVersion { get; set; }

        internal void SetSignature(string secretKey)
        {
            var transactionId = (!string.IsNullOrEmpty(this.TransactionId) ? this.TransactionId : "");
            var transactionTime = (!string.IsNullOrEmpty(this.TransactionTime) ? this.TransactionTime : "");
            var amount = this.Amount;
            var currency = (!string.IsNullOrEmpty(this.Currency) ? this.Currency : "");

            var installmentCount = Installment.HasValue ? this.Installment : null;
            var text = secretKey + transactionId + transactionTime + amount + currency +
                       installmentCount;

            if (HashVersion == "1.1")
                Signature = CryptographyHelper.HMacSha512(text, secretKey);
            else
                Signature = CryptographyHelper.HashSha256(text);
        }
    }
}