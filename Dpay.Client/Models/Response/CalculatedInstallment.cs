
namespace Dpay.Client.Models.Response
{
    public class CalculatedInstallment
    {
        public int? Amount { get; set; }
        public int? CalculatedAmount { get; set; }
        public int BankId { get; set; }
        public string BankName { get; set; }
        public int Installment { get; set; }
        public decimal Commission { get; set; }
    }
}
