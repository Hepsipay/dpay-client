namespace Dpay.Client.Models.Response
{
    public class BasePagedResponse : BaseResponse
    {
        public PagerState PagerState { get; set; }
    }
}
