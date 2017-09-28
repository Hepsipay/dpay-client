using System;

namespace Dpay.Client.Models
{
    public class BasketItem
    {

        public Guid? SubMerchantId { get; set; }
        public string BasketItemId { get; set; }
        public string Description { get; set; }
        public string ProductCode { get; set; }
        public int? Amount { get; set; }
        public int? VatRatio { get; set; }
        public int? Count { get; set; }
        public string Url { get; set; }
        public int BasketItemType { get; set; }
    }
}