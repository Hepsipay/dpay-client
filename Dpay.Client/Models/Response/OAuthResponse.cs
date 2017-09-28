using System;
using Newtonsoft.Json;

namespace Dpay.Client.Models.Response
{
    internal class OAuthResponse : BaseResponse
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        public DateTime TokenDate { get; set; }

        [JsonProperty("error_description")]
        public string Message { get; set; }

        [JsonProperty("error")]
        public string MessageCode { get; set; }
    }
}
