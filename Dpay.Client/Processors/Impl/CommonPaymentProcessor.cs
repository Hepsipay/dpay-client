using Dpay.Client.Helpers;
using Dpay.Client.Models.Request;
using Dpay.Client.Models.Response;
using Dpay.Client.Processors.Interfaces;

namespace Dpay.Client.Processors.Impl
{
    public class CommonPaymentProcessor : ICommonPaymentProcessor
    {
        private readonly IHttpOperations _httpClient;
        public CommonPaymentProcessor()
        {
            _httpClient = new HttpOperations();
        }

        public SaveCommonPaymentResponse Save(SaveCommonPaymentRequest reqModel, string apiUrl, string secretKey)
        {
            reqModel.SetSignature(secretKey);

            var response = RestCallPost<SaveCommonPaymentResponse>(apiUrl, reqModel, secretKey, false);

            ControlSaveSignature(reqModel, response, secretKey);

            return response;
        }

        public CommonPaymentQueryResponse Query(CommonPaymentQueryRequest reqModel, string apiUrl, string secretKey)
        {
            reqModel.SetSignature(secretKey);

            var response = RestCallPost<CommonPaymentQueryResponse>(apiUrl, reqModel, secretKey, false);

            ControlQuerySignature(reqModel, response, secretKey);

            return response;
        }

        protected virtual T RestCallPost<T>(string apiUrl, BaseRequest reqModel, string secretKey, bool istokenRequired)
        {
            var pointQueryResponse = _httpClient.Post<T>(apiUrl, reqModel, reqModel.ApiKey,
                secretKey, istokenRequired);

            return pointQueryResponse;
        }

        protected virtual void ControlSaveSignature(SaveCommonPaymentRequest reqModel, SaveCommonPaymentResponse resModel, string secretKey)
        {
            if (resModel.Success)
                resModel.ControlSignature(secretKey, reqModel.HashVersion);
        }

        protected virtual void ControlQuerySignature(CommonPaymentQueryRequest reqModel, CommonPaymentQueryResponse resModel, string secretKey)
        {
            if (resModel.Success)
                resModel.ControlSignature(secretKey, reqModel.HashVersion);
        }
    }
}
