using System;

namespace Dpay.Client.Models.Response
{
    public class UpdateCardResponse : BaseResponse
    {
        public Guid Id { get; set; }
        public string MerchantUserId { get; set; }
        public string MerchantCardUserId { get; set; }
        public string MaskedCardNumber { get; set; }
        public string FullName { get; set; }
        public string ExpireMonth { get; set; }
        public string ExpireYear { get; set; }
        public string Email { get; set; }
        public string ReferenceId1 { get; set; }
    }
}
