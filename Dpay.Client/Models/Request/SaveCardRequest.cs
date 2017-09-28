
namespace Dpay.Client.Models.Request
{
    public class SaveCardRequest : BaseRequest
    {
        public string CardNumber { get; set; }
        public string ExpireMonth { get; set; }
        public string ExpireYear { get; set; }
        public string CardHolderFullName { get; set; }
        public string MerchantUserId { get; set; }
        public string MerchantCardUserId { get; set; }
        public string ApiKeyName { get; set; }
        public string Email { get; set; }
        public string ReferenceId1 { get; set; }

    }
}
