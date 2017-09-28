using System;
using System.Collections.Generic;
using Dpay.Client.Models;

namespace Dpay.Client.Web.Models
{
    public class CardResultModel
    {
        public Guid Id { get; set; }
        public string MaskedCardNumber { get; set; }
        public string FullName { get; set; }
        public string MerchantUserId { get; set; }
        public string MerchantCardUserId { get; set; }
        public bool IsSuccess { get; set; }
        public string Email { get; set; }
        public string ReferenceId1 { get; set; }

        public bool Success { get; set; }
        public string MessageCode { get; set; }
        public string Message { get; set; }
        public string UserMessage { get; set; }

        public IList<MerchantCardModel> MerchantCardDtos { get; set; }
    }
}