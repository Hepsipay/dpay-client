using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using Newtonsoft.Json;
using Dpay.Client.Models.Request;
using Dpay.Client.Models.Response;

namespace Dpay.Client.Helpers
{
    /// <summary>
    /// Provides functionality for REST based requests.
    /// </summary>
    public class HttpOperations : IHttpOperations
    {
        public T Post<T>(string apiUrl, object postData, string apiKey, string secretKey, bool isTokenRequired = true)
        {
            try
            {
                using (var client = CreateHttpClient())
                {
                    if (isTokenRequired)
                        client.DefaultRequestHeaders.Authorization = GetAuthHeaderValue(apiUrl.GetUriLeftPart(), apiKey, secretKey);
                    var response = client.PostAsJsonAsync(apiUrl, postData);
                    var result = response.Result;
                    return JsonConvert.DeserializeObject<T>(result.Content.ReadAsStringAsync().Result);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw new DpayClientException(Constants.CommunucationError, Constants.CommunucationError.ReturnErrorMessage(), Constants.CommunucationError.ReturnUserErrorMessage());
            }
        }

        public T Put<T>(string apiUrl, object postData, string apiKey, string secretKey, bool isTokenRequired = true)
        {
            try
            {
                using (var client = CreateHttpClient())
                {
                    if (isTokenRequired)
                        client.DefaultRequestHeaders.Authorization = GetAuthHeaderValue(apiUrl.GetUriLeftPart(), apiKey, secretKey);
                    var content = new ObjectContent<object>(postData, new JsonMediaTypeFormatter());
                    var response = client.PutAsync(apiUrl, content);
                    var result = response.Result;
                    return JsonConvert.DeserializeObject<T>(result.Content.ReadAsStringAsync().Result);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw new DpayClientException(Constants.CommunucationError, Constants.CommunucationError.ReturnErrorMessage(), Constants.CommunucationError.ReturnUserErrorMessage());
            }
        }

        public T Delete<T>(string apiUrl, object postData, string apiKey, string secretKey, bool isTokenRequired = true)
        {
            try
            {
                using (var client = CreateHttpClient())
                {
                    if (isTokenRequired)
                        client.DefaultRequestHeaders.Authorization = GetAuthHeaderValue(apiUrl.GetUriLeftPart(), apiKey, secretKey);

                    var message = new HttpRequestMessage(HttpMethod.Delete, apiUrl)
                    {
                        Content = new ObjectContent<object>(postData, new JsonMediaTypeFormatter())
                    };
                    var response = client.SendAsync(message);
                    var result = response.Result;
                    return JsonConvert.DeserializeObject<T>(result.Content.ReadAsStringAsync().Result);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw new DpayClientException(Constants.CommunucationError, Constants.CommunucationError.ReturnErrorMessage(), Constants.CommunucationError.ReturnUserErrorMessage());
            }
        }

        private AuthenticationHeaderValue GetAuthHeaderValue(string hostUri, string apiKey, string secretKey)
        {
            OAuthResponse tokenObject;
            if (HelperFunctions.AppDomainContains(Constants.ApiAppDomainTokenKey))
            {
                tokenObject = HelperFunctions.GetFromAppDomain(Constants.ApiAppDomainTokenKey) as OAuthResponse;

                if (tokenObject.IsTokenExpired())
                {
                    //request for new token
                    tokenObject = GetAccessTokenFromApi(hostUri, apiKey, secretKey);

                    //add new token to app domain
                    HelperFunctions.AddToAppDomain(Constants.ApiAppDomainTokenKey, tokenObject);
                }
            }
            else
            {
                //request for new token
                tokenObject = GetAccessTokenFromApi(hostUri, apiKey, secretKey);

                //add new token to app domain
                HelperFunctions.AddToAppDomain(Constants.ApiAppDomainTokenKey, tokenObject);
            }

            if (tokenObject != null)
                return new AuthenticationHeaderValue("Bearer", tokenObject.AccessToken);
            throw new Exception("Token Object Not Found !");
        }

        private OAuthResponse GetAccessTokenFromApi(string hostUri, string apiKey, string secretKey)
        {
            var tokenRequest = new AccessTokenRequest()
            {
                ClientId = apiKey,
                ClientSecret = secretKey
            };

            using (var client = CreateHttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                    Convert.ToBase64String(
                        Encoding.ASCII.GetBytes(String.Format("{0}:{1}", tokenRequest.ClientId, tokenRequest.ClientSecret))));
                var postData = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("grant_type", "password"),
                };

                HttpContent content = new FormUrlEncodedContent(postData);

                var response = client.PostAsync(hostUri + Constants.ApiGetTokenUrl, content).Result.EnsureSuccessStatusCode();
                var tokenModel = response.Content.ReadAsAsync<OAuthResponse>().Result;

                tokenModel.TokenDate = DateTime.Now;
                return tokenModel;
            }
        }

        private HttpClient CreateHttpClient()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }

    }
}