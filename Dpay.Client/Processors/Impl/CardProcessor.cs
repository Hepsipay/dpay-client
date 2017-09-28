using Dpay.Client.Helpers;
using Dpay.Client.Models.Request;
using Dpay.Client.Models.Response;
using Dpay.Client.Processors.Interfaces;

namespace Dpay.Client.Processors.Impl
{
    public class CardProcessor : ICardProcessor
    {
        private readonly IHttpOperations _httpClient;
        public CardProcessor()
        {
            _httpClient = new HttpOperations();
        }

        public SaveCardResponse SaveCard(SaveCardRequest request, string apiUrl, string secretKey)
        {
            var saveCardResponse = RestCallPost<SaveCardResponse>(apiUrl, request, secretKey);

            return saveCardResponse;
        }

        public DeleteCardResponse DeleteCard(DeleteCardRequest request, string apiUrl, string secretKey)
        {
            var deleteCardResponse = RestCallDelete<DeleteCardResponse>(apiUrl, request, secretKey);

            return deleteCardResponse;
        }

        public UpdateCardResponse UpdateCard(UpdateCardRequest request, string apiUrl, string secretKey)
        {
            var updateResponse = RestCallPut<UpdateCardResponse>(apiUrl, request, secretKey);

            return updateResponse;
        }

        public GetMerchantCardResponse GetCard(GetMerchantCardRequest request, string apiUrl, string secretKey)
        {
            var getCardResponse = RestCallPost<GetMerchantCardResponse>(apiUrl, request, secretKey);

            return getCardResponse;
        }

        protected virtual T RestCallPost<T>(string apiUrl, BaseRequest reqModel, string secretKey)
        {
            var pointQueryResponse = _httpClient.Post<T>(apiUrl, reqModel, reqModel.ApiKey,
                secretKey);

            return pointQueryResponse;
        }

        protected virtual T RestCallPut<T>(string apiUrl, BaseRequest reqModel, string secretKey)
        {
            var pointQueryResponse = _httpClient.Put<T>(apiUrl, reqModel, reqModel.ApiKey,
                secretKey);

            return pointQueryResponse;
        }
        
        protected virtual T RestCallDelete<T>(string apiUrl, BaseRequest reqModel, string secretKey)
        {
            var pointQueryResponse = _httpClient.Delete<T>(apiUrl, reqModel, reqModel.ApiKey,
                secretKey);

            return pointQueryResponse;
        }
    }
}
