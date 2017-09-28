using System;
using System.Collections.Generic;

namespace Dpay.Client.Models.Response
{
    public class GetMerchantCardResponse : BasePagedResponse
    {
        public IList<MerchantCardModel> MerchantCardDtos { get; set; }
    }
}
