using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Dpay.Client.Models.Request;
using Dpay.Client.Models.Response;
using Newtonsoft.Json;

namespace Dpay.Client.Helpers
{
    public class HttpClientHelper : IHttpClientHelper
    {
        public T Delete<T>(string apiUrl, string resource, object postObject)
        {
            var request = (HttpWebRequest)WebRequest.Create(String.Format("{0}/{1}", apiUrl, resource));

            request.Method = "DELETE";

            request.ContentType = "application/json";
            request.Accept = "application/json";

            SetBearerToken(request);

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                string json = JsonConvert.SerializeObject(postObject);

                streamWriter.Write(json);
                streamWriter.Flush();
            }

            using (var httpResponse = (HttpWebResponse)request.GetResponse())
            {
                using (var reader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var json = reader.ReadToEnd();
                    var obj = JsonConvert.DeserializeObject<T>(json);

                    return obj;
                }
            }
        }

        public HttpResponseMessage Put(string apiUrl, string resource, object postObject)
        {
            using (var client = CreateHttpClient())
            {

                SetBearerToken(client);

                HttpContent content = new ObjectContent<object>(postObject, new JsonMediaTypeFormatter());

                var response = client.PutAsync(resource, content);

                var result = response.Result;

                return result;
            }
        }

        public HttpResponseMessage PostWithFormContent(string apiUrl, string resource, KeyValuePair<string, string>[] formContent, string contentType)
        {
            using (var client = CreateHttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(contentType));

                SetBearerToken(client);

                var content = new FormUrlEncodedContent(formContent);
                content.Headers.Add("Keep-Alive", "true");
                var response = client.PostAsync(resource, content);
                var result = response.Result;

                return result;
            }
        }

        public HttpResponseMessage PostAsJson(string apiUrl, string resource, object postObject)
        {
            using (var client = CreateHttpClient())
            {
                SetBearerToken(client);

                var response = client.PostAsJsonAsync(resource, postObject);

                var result = response.Result;

                return result;
            }
        }

        public HttpResponseMessage PostAsJson(string apiUrl, string resource, object postObject, Action<HttpClient> setBearerToken)
        {
            using (var client = CreateHttpClient())
            {
                if (setBearerToken != null)
                    setBearerToken(client);

                var response = client.PostAsJsonAsync(resource, postObject);

                var result = response.Result;

                return result;
            }
        }

        public HttpResponseMessage PutAsJson(string apiUrl, string resource, object postObject, Action<HttpClient> setBearerToken)
        {
            using (var client = CreateHttpClient())
            {
                if (setBearerToken != null)
                    setBearerToken(client);

                var response = client.PutAsJsonAsync(resource, postObject);

                var result = response.Result;

                return result;
            }
        }

        public string PostAsJsonForMail(string apiUrl, string resource, object postObject)
        {
            using (var client = CreateHttpClient())
            {
                var response = client.PostAsJsonAsync(resource, postObject);

                var result = response.Result.Content.ReadAsStringAsync().Result;

                return result;
            }
            //var request = WebRequest.Create(apiUrl + resource);
            //request.Timeout = _configurationSource.GetIntegerValue("HttpRequestTimeOut") * 1000;
            //request.Method = "POST";

            //byte[] byteArray = Encoding.UTF8.GetBytes(postObject);
            //request.ContentType = "application/json";
            //request.ContentLength = byteArray.Length;

            //using (var requestStream = request.GetRequestStream())
            //{
            //    requestStream.Write(byteArray, 0, byteArray.Length);
            //    requestStream.Close();
            //}
            //request.GetResponseAsync();
        }

        public HttpResponseMessage Post(string apiUrl, string resource, object postObject, string contentType)
        {
            using (var client = CreateHttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(contentType));

                SetBearerToken(client);
                var response = client.PostAsync(resource, new StringContent(postObject.ToString()));

                var result = response.Result;

                return result;
            }
        }

        public string Post(string apiUrl, string postObject, string contentType)
        {
            var request = WebRequest.Create(apiUrl);
            request.Method = "POST";

            byte[] byteArray = Encoding.UTF8.GetBytes(postObject);
            request.ContentType = contentType;
            request.ContentLength = byteArray.Length;

            using (var requestStream = request.GetRequestStream())
            {
                requestStream.Write(byteArray, 0, byteArray.Length);
                requestStream.Close();
            }

            using (var response = request.GetResponse())
            {
                using (var responseStream = response.GetResponseStream())
                {
                    using (var reader = new StreamReader(responseStream))
                    {
                        string responseFromServer = reader.ReadToEnd();
                        reader.Close();
                        responseStream.Close();
                        response.Close();
                        return responseFromServer;
                    }
                }
            }
        }

        public HttpResponseMessage PostForAccessToken(string apiUrl, string oauthUrl, AccessTokenRequest accessTokenRequest)
        {
            using (var client = CreateHttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                    Convert.ToBase64String(
                        Encoding.ASCII.GetBytes(string.Format("{0}:{1}", accessTokenRequest.ClientId,
                            accessTokenRequest.ClientSecret))));
                var postData = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("state_id", accessTokenRequest.StateId),
                    new KeyValuePair<string, string>("user_agent", accessTokenRequest.UserAgent),
                    new KeyValuePair<string, string>("state_duration", accessTokenRequest.StateDuration.ToString()),
                    new KeyValuePair<string, string>("ip", accessTokenRequest.Ip)
                };

                HttpContent content = new FormUrlEncodedContent(postData);

                HttpResponseMessage response = client.PostAsync(oauthUrl, content).Result;
                return response;
            }
        }

        public HttpResponseMessage PostForAccessTokenEncrypted(string apiUrl, string oauthUrl, string scheme, string parameter)
        {
            using (var client = CreateHttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme, parameter);
                var postData = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("grant_type", "password")
                };

                HttpContent content = new FormUrlEncodedContent(postData);

                HttpResponseMessage response = client.PostAsync(oauthUrl, content).Result;
                return response;
            }
        }

        protected virtual void SetBearerToken(HttpClient client)
        {
            var token = AppDomain.CurrentDomain.GetData("Token");
            var tokenResultModel = (OAuthResponse)token;
            if (tokenResultModel != null && tokenResultModel.AccessToken != null)
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                    tokenResultModel.AccessToken);
        }

        protected virtual void SetBearerToken(HttpWebRequest request)
        {
            var token = AppDomain.CurrentDomain.GetData("Token");
            var tokenResultModel = (OAuthResponse)token;
            if (tokenResultModel != null && tokenResultModel.AccessToken != null)
                request.Headers[HttpRequestHeader.Authorization] = string.Format("{0} {1}", "Bearer", tokenResultModel.AccessToken);
        }

        public HttpClient CreateHttpClient()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }
    }
}
