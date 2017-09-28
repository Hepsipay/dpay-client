
namespace Dpay.Client.Models.Request
{
    public class AccessTokenRequest
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string StateId { get; set; }
        public int StateDuration { get; set; }
        public string Ip { get; set; }
        public string UserAgent { get; set; }
    }
}
