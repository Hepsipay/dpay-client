using Dpay.Client.Helpers;
using Dpay.Client.Models.Request;
using Dpay.Client.Models.Response;
using Dpay.Client.Processors.Interfaces;

namespace Dpay.Client.Processors.Impl
{
    public class InstallmentProcessor : IInstallmentProcessor
    {
        private readonly IHttpOperations _httpClient;
        public InstallmentProcessor()
        {
            _httpClient = new HttpOperations();
        }

        public GetInstallmentsResponse GetInstallments(GetInstallmentsRequest reqModel, string apiUrl, string secretKey)
        {
            var installmentResponse = RestCallPost<GetInstallmentsResponse>(apiUrl, reqModel, secretKey);

            return installmentResponse;
        }


        protected virtual T RestCallPost<T>(string apiUrl, BaseRequest reqModel, string secretKey)
        {
            var pointQueryResponse = _httpClient.Post<T>(apiUrl, reqModel, reqModel.ApiKey,
                secretKey);

            return pointQueryResponse;
        }
    }
}
