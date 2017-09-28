
using System.Collections.Generic;

namespace Dpay.Client.Helpers
{
    public static class Constants
    {
        public static readonly string CommunucationError = "6000";
        public static string SignatureVerificationError = "6001";
        public static string DeserializationError = "6002";

        private const string CommunucationErrorMessage = "İletişim hatası oluştu.";
        private const string DeserializationErrorMessage = "Cevap mesajı parse edilemiyor";
        private const string SignatureVerificationErrorMessage = "Cevap mesajında gönderilen imza değeri doğrulanamıyor.";
        private const string UserGeneralFailureErrorMessage = "Şu anda işleminiz gerçekleştirilemiyor. Lütfen daha sonra tekrar deneyiniz.";

        private static readonly Dictionary<string, string> TechnicalErrorMessages =
            new Dictionary<string, string>()
            {
                {"6000", CommunucationErrorMessage},
                {"6001", SignatureVerificationErrorMessage},
                {"6002", DeserializationErrorMessage}
            };

        private static readonly Dictionary<string, string> UserErrorMessages =
               new Dictionary<string, string>()
            {
                {"6000", UserGeneralFailureErrorMessage},
                {"6001", UserGeneralFailureErrorMessage},
                {"6002", UserGeneralFailureErrorMessage}
            };

        public static string ReturnErrorMessage(this string errorCode)
        {
            string errorMessage;
            TechnicalErrorMessages.TryGetValue(errorCode, out errorMessage);
            return errorMessage;
        }

        public static string ReturnUserErrorMessage(this string errorCode)
        {
            string errorMessage;
            UserErrorMessages.TryGetValue(errorCode, out errorMessage);
            return errorMessage;
        }

        public static string ApiGetTokenUrl
        {
            get { return "/oauth/token"; }
        }

        public static string ApiAppDomainTokenKey
        {
            get { return "Dpay.Client.Token"; }
        }
    }
}
