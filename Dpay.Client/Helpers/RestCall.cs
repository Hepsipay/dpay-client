using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Newtonsoft.Json;
using RestSharp;
using System.Diagnostics;
using Dpay.Client.Models.Response;

namespace Dpay.Client.Helpers
{
    /// <summary>
    /// Provides functionality for REST based requests.
    /// </summary>
    internal class RestCall
    {
        private readonly IRestClient _restClient;
        private IRestRequest _restRequest;

        /// <summary>
        /// Initializes a Restcall instance with the specified Url
        /// </summary>
        /// <param name="baseUrl">REST Url</param>
        public RestCall(string baseUrl)
        {
            this._restClient = new RestClient(baseUrl);
        }

        /// <summary>
        /// POSTs the request
        /// </summary>
        /// <returns>itself</returns>
        public RestCall Post()
        {
            return CreateRequest(Method.POST);
        }

        /// <summary>
        /// PUTs the request
        /// </summary>
        /// <returns>itself</returns>
        public RestCall Put()
        {
            return CreateRequest(Method.PUT);
        }

        /// <summary>
        /// GETs the request
        /// </summary>
        /// <returns>itself</returns>
        public RestCall Get()
        {
            return CreateRequest(Method.GET);
        }

        /// <summary>
        /// DELETEs the request
        /// </summary>
        /// <returns>itself</returns>
        public RestCall Delete()
        {
            return CreateRequest(Method.DELETE);
        }

        /// <summary>
        /// Creates Request
        /// </summary>
        /// <param name="method">Request method</param>
        /// <returns>itself</returns>
        private RestCall CreateRequest(Method method)
        {
            this._restRequest = new RestRequest(method)
            {
                RequestFormat = DataFormat.Json,
            };

            return this;
        }

        /// <summary>
        /// Body
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public RestCall IncludingBody(object data)
        {
            this._restRequest.AddBody(data);

            return this;
        }

        /// <summary>
        /// Parameter
        /// </summary>
        /// <param name="name"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public RestCall IncludingParameter(string name, object data)
        {
            this._restRequest.AddParameter(name, data);

            return this;
        }

        /// <summary>
        /// IncludingHeader
        /// </summary>
        /// <param name="name"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public RestCall IncludingHeader(string name, string data)
        {
            var parameter = this._restRequest.Parameters.FirstOrDefault(s => s.Name == name);
            if (parameter != null)
                parameter.Value = data;
            else
                this._restRequest.AddHeader(name, data);

            return this;
        }

        /// <summary>
        /// Parameter
        /// </summary>
        /// <param name="parametersDictionary"></param>
        /// <returns></returns>
        public RestCall IncludingParameters(IDictionary<string, object> parametersDictionary)
        {
            foreach (var parameter in parametersDictionary)
                this._restRequest.AddParameter(parameter.Key, parameter.Value);

            return this;
        }

        /// <summary>
        /// Parameter
        /// </summary>
        /// <param name="name"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public RestCall IncludingQueryParameter(string name, string data)
        {
            this._restRequest.AddQueryParameter(name, data);

            return this;
        }

        /// <summary>
        /// Send
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <returns></returns>
        public TModel SendAndGetResponse<TModel>() where TModel : BaseResponse, new()
        {
            try
            {
                var response = this._restClient.Execute<TModel>(this._restRequest);
                var responseModel = JsonConvert.DeserializeObject<TModel>(response.Content);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return responseModel;
                }

                if (!string.IsNullOrEmpty(responseModel.MessageCode))
                    throw new DpayClientException(responseModel.MessageCode, responseModel.Message,
                        responseModel.UserMessage);

                throw new DpayClientException(Constants.CommunucationError,
                    Constants.CommunucationError.ReturnErrorMessage(),
                    Constants.CommunucationError.ReturnUserErrorMessage());
            }
            catch (DpayClientException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw new DpayClientException(Constants.CommunucationError, Constants.CommunucationError.ReturnErrorMessage(), Constants.CommunucationError.ReturnUserErrorMessage());
            }
        }

        internal string GetRequestUriLeftPart()
        {
            return _restClient.BaseUrl.GetLeftPart(UriPartial.Authority);
        }
    }
}