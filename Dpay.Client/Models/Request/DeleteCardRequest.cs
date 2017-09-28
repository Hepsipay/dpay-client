using System;

namespace Dpay.Client.Models.Request
{
    public class DeleteCardRequest : BaseRequest
    {
        public Guid Id { get; set; }
        public string MerchantUserId { get; set; }
        public string MerchantCardUserId { get; set; }
    }
}
