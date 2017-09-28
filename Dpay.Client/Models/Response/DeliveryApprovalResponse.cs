using System;
using Dpay.Client.Helpers;

namespace Dpay.Client.Models.Response
{
    public class DeliveryApprovalResponse : BaseResponse
    {
        public string ApiKey { get; set; }
        public string TransactionId { get; set; }
        public string TransactionTime { get; set; }
        public string Signature { get; set; }
        public int EarnedAmount { get; set; }
        
    }
}