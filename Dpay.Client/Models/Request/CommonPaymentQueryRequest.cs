using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dpay.Client.Helpers;

namespace Dpay.Client.Models.Request
{
    public class CommonPaymentQueryRequest : BaseRequest
    {
        public string TransactionId { get; set; }
        public string CommonPaymentUniqueId { get; set; }
        public string Signature { get; set; }
        public string HashVersion { get; set; }

        internal void SetSignature(string secretKey)
        {
            var transactionId = (!string.IsNullOrEmpty(this.TransactionId) ? this.TransactionId : "");
            var commonPaymentUniqueId = (!string.IsNullOrEmpty(this.CommonPaymentUniqueId) ? this.CommonPaymentUniqueId : "");

            var text = secretKey + transactionId + commonPaymentUniqueId;

            if (HashVersion == "1.1")
                Signature = CryptographyHelper.HMacSha512(text, secretKey);
            else
                Signature = CryptographyHelper.HashSha256(text);
        }
    }
}
