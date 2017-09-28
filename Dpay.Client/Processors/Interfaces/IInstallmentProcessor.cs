using Dpay.Client.Models.Request;
using Dpay.Client.Models.Response;

namespace Dpay.Client.Processors.Interfaces
{
    /// <summary>
    /// IInstallmentProcessor interface is used for installment operations
    /// </summary>
    public interface IInstallmentProcessor
    {
        /// <summary>
        /// This method is used to get installments according to bin code
        /// </summary>
        /// <param name="reqModel"></param>
        /// <param name="apiUrl"></param>
        /// <param name="secretKey"></param>
        /// <returns></returns>
        GetInstallmentsResponse GetInstallments(GetInstallmentsRequest reqModel, string apiUrl, string secretKey);
    }
}
