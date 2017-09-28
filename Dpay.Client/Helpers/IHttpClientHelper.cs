using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Dpay.Client.Models.Request;

namespace Dpay.Client.Helpers
{
    public interface IHttpClientHelper
    {
        string Post(string apiUrl, string postObject, string contentType);
        HttpResponseMessage Post(string apiUrl, string resource, object postObject, string contentType);
        T Delete<T>(string apiUrl, string resource, object postObject);
        HttpResponseMessage Put(string apiUrl, string resource, object postObject);
        HttpResponseMessage PostWithFormContent(string apiUrl, string resource, KeyValuePair<string, string>[] formContent, string contentType);
        HttpResponseMessage PutAsJson(string apiUrl, string resource, object postObject, Action<HttpClient> setBearerToken);
        HttpResponseMessage PostAsJson(string apiUrl, string resource, object postObject);
        HttpResponseMessage PostAsJson(string apiUrl, string resource, object postObject, Action<HttpClient> setBearerToken);
        HttpResponseMessage PostForAccessToken(string apiUrl, string oAuthUrl, AccessTokenRequest accessTokenRequest);
        HttpResponseMessage PostForAccessTokenEncrypted(string apiUrl, string oauthUrl, string scheme, string parameter);
    }
}
