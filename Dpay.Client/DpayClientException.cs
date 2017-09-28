using System;

namespace Dpay.Client
{
    public class DpayClientException : Exception
    {
        public string UserMessage { get; set; }
        public string Code { get; set; }

        public DpayClientException(string code,string message, string userMessage):base(message)
        {
            Code = code;
            UserMessage = userMessage;
        }
    }
}