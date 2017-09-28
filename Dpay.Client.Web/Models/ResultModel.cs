namespace Dpay.Client.Web.Models
{
    public class ResultModel
    {
        public string ThreeDHostAddress { get; set; }
        public string SuccessUrl { get; set; }
        public string FailUrl { get; set; }
        public string TransactionId { get; set; }
        public string TransactionTime { get; set; }
        public string Currency { get; set; }
        public int TransactionType { get; set; }
        public int? Installment { get; set; }
        public string ApiKey { get; set; }
        public int Amount { get; set; }
        public bool SaveCreditCard { get; set; }
        public string CardId { get; set; }
        public string BankResponseCode { get; set; }
        public string BankResponseMessage { get; set; }

        public bool Success { get; set; }
        public string MessageCode { get; set; }
        public string Message { get; set; }
        public string UserMessage { get; set; }
    }
}