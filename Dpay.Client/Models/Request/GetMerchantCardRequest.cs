using System;

namespace Dpay.Client.Models.Request
{
    public class GetMerchantCardRequest : BasePagedRequest
    {
        public Guid? Id { get; set; }
        public string MerchantUserId { get; set; }
        public string MerchantCardUserId { get; set; }
        public string Email { get; set; }
        public string ReferenceId1 { get; set; }
    }
}
