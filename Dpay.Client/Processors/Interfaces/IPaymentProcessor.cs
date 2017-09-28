using System.Web.Mvc;
using Dpay.Client.Models.Request;
using Dpay.Client.Models.Response;

namespace Dpay.Client.Processors.Interfaces
{
    public interface IPaymentProcessor
    {
        PaymentResponse Pay(PaymentRequest paymentRequest, string apiUrl, string secretKey);

        PointQueryResponse PointQuery(PointQueryRequest pointQueryRequest, string apiUrl, string secretKey);

        RefundResponse Refund(RefundRequest refundRequest, string apiUrl, string secretKey);

        DirectPaymentRefundResponse DirectPaymentRefund(DirectPaymentRefundRequest refundRequest, string apiUrl, string secretKey);

        DeliveryApprovalResponse DeliveryApproval(DeliveryApprovalRequest deliveryApprovalRequest, string apiUrl, string secretKey);

        PostAuthResponse PostAuth(PostAuthRequest postAuthRequest, string apiUrl, string secretKey);

        CreateThreedResponse CreateThreed(CreateThreedRequest createThreedRequestRequest, string apiUrl, string secretKey);

        CompleteThreedResponse CompleteThreed(CompleteThreedRequest completeThreedRequest, string apiUrl, string secretKey);

        FailedThreedResponse GetFailedThreedResponse(FormCollection formCollection, string secretKey);
    }
}