using System;

namespace Dpay.Client.Models
{
    public class MerchantInformation
    {
        public Guid Id { get; set; }
        public string Url { get; set; }
        public string TestApiKey { get; set; }
        public string ProductionApiKey { get; set; }
        public string ProdSecretKey { get; set; }
        public string TestSecretKey { get; set; }
    }
}
