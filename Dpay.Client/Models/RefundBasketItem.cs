namespace Dpay.Client.Models
{
    public class RefundBasketItem
    {
        public string BasketItemId { get; set; }
        public int? MerchantOffsetAmount { get; set; }
        public int? SubMerchantOffsetAmount { get; set; }
        public int? Amount { get; set; }
    }
}