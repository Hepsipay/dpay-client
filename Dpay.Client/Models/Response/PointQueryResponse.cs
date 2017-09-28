using System;
using Dpay.Client.Helpers;

namespace Dpay.Client.Models.Response
{
    public class PointQueryResponse : BaseResponse
    {
        public long PointBalance { get; set; }
        public string OrderId { get; set; }
        public string TransactionId { get; set; }
        public string TransactionTime { get; set; }
        public string ApiKey { get; set; }
        public string Signature { get; set; }
        public int? Installment { get; set; }
        public string Currency { get; set; }
    }
}