using System;

namespace Dpay.Client.Models.Request
{
    public class UpdateCardRequest : BaseRequest
    {
        public Guid Id { get; set; }
        public string MerchantUserId { get; set; }
        public string MerchantCardUserId { get; set; }
        public string ExpireMonth { get; set; }
        public string ExpireYear { get; set; }
    }
}
