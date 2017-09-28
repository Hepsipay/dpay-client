using System.Collections.Generic;
using Dpay.Client.Models.Response;

namespace Dpay.Client.Web.Models
{
    public class InstallmentResultModel
    {
        public IList<CalculatedInstallment> InstallmentDtos { get; set; }

        public bool Success { get; set; }
        public string MessageCode { get; set; }
        public string Message { get; set; }
        public string UserMessage { get; set; }
    }
}