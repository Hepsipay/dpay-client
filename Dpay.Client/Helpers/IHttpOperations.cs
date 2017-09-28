using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dpay.Client.Helpers
{
    public interface IHttpOperations
    {
        T Post<T>(string apiUrl, object postData, string apiKey, string secretKey, bool isTokenRequired = true);
        T Put<T>(string apiUrl, object postData, string apiKey, string secretKey, bool isTokenRequired = true);
        T Delete<T>(string apiUrl, object postData, string apiKey, string secretKey, bool isTokenRequired = true);
    }
}
