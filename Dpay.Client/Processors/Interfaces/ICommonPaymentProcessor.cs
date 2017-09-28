using Dpay.Client.Models.Request;
using Dpay.Client.Models.Response;

namespace Dpay.Client.Processors.Interfaces
{
    /// <summary>
    /// ICommonPaymentProcessor methods
    /// </summary>
    public interface ICommonPaymentProcessor
    {
        /// <summary>
        /// This method is used to save common payment
        /// </summary>
        /// <param name="reqModel"></param>
        /// <param name="apiUrl"></param>
        /// <param name="secretKey"></param>
        /// <returns></returns>
        SaveCommonPaymentResponse Save(SaveCommonPaymentRequest reqModel, string apiUrl, string secretKey);

        /// <summary>
        /// This method is used to query common payment
        /// </summary>
        /// <param name="reqModel"></param>
        /// <param name="apiUrl"></param>
        /// <param name="secretKey"></param>
        /// <returns></returns>
        CommonPaymentQueryResponse Query(CommonPaymentQueryRequest reqModel, string apiUrl, string secretKey);
    }
}
