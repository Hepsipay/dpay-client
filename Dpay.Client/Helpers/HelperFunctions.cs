using Dpay.Client.Models.Request;
using Dpay.Client.Models.Response;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Dpay.Client.Helpers
{
    /// <summary>
    /// OAuthToken Model Extension Methods
    /// </summary>
    internal static class HelperFunctions
    {
        /// <summary>
        /// This method is used to add token object to app domain
        /// </summary>
        public static void AddToAppDomain(string key, object obj)
        {
            AppDomain.CurrentDomain.SetData(key, obj);
        }

        /// <summary>
        /// This method is used to get token object from app domain
        /// </summary>
        /// <returns></returns>
        public static object GetFromAppDomain(string key)
        {
            return AppDomain.CurrentDomain.GetData(key);
        }

        /// <summary>
        /// This method checks app domain contains token object or not
        /// </summary>
        /// <returns></returns>
        public static bool AppDomainContains(string key)
        {
            return GetFromAppDomain(key) != null;
        }

        /// <summary>
        /// Checks whether token expired or not
        /// </summary>
        /// <param name="tokenObject"></param>
        /// <returns></returns>
        public static bool IsTokenExpired(this OAuthResponse tokenObject)
        {
            var tokenExpiration = TimeSpan.FromMinutes(5);
            if ((DateTime.Now - tokenObject.TokenDate).TotalSeconds - tokenObject.ExpiresIn >
                tokenExpiration.TotalSeconds || string.IsNullOrEmpty(tokenObject.AccessToken))
            {
                return true;
            }
            return false;
        }

        public static string GetUriLeftPart(this string uri)
        {
            Uri uriOut;
            if (Uri.TryCreate(uri, UriKind.Absolute, out uriOut))
                return uriOut.GetLeftPart(UriPartial.Authority);
            throw new Exception("Uri not found.");
        }
    }
}
