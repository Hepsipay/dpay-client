
using System.Collections.Generic;

namespace Dpay.Client.Models.Response
{
    public class DeleteCardResponse : BaseResponse
    {
        public IList<MerchantCardModel> MerchantCardDtos { get; set; }
    }
}
