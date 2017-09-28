using System;
using System.Collections.Generic;

namespace Dpay.Client.Models.Response
{
    public class GetInstallmentsResponse : BaseResponse
    {
        public IList<CalculatedInstallment> InstallmentDtos { get; set; }
    }
}
