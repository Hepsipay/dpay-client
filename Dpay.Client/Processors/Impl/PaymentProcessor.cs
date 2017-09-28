using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using Dpay.Client.Helpers;
using Dpay.Client.Models.Request;
using Dpay.Client.Models.Response;
using Dpay.Client.Processors.Interfaces;

namespace Dpay.Client.Processors.Impl
{
    public class PaymentProcessor : IPaymentProcessor
    {
        private readonly IHttpOperations _httpClient;
        public PaymentProcessor()
        {
            _httpClient = new HttpOperations();
        }

        public PaymentResponse Pay(PaymentRequest paymentRequest, string apiUrl, string secretKey)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            paymentRequest.SetSignature(secretKey);
            var paymentResponse = RestCall<PaymentResponse>(apiUrl, paymentRequest,
                secretKey);

            if (paymentResponse.Success)
                paymentResponse.ControlSignature(secretKey, paymentRequest.HashVersion);

            return paymentResponse;
        }

        public PointQueryResponse PointQuery(PointQueryRequest pointQueryRequest, string apiUrl, string secretKey)
        {
            pointQueryRequest.SetSignature(secretKey);
            var pointQueryResponse = RestCall<PointQueryResponse>(apiUrl, pointQueryRequest,
             secretKey);

            if (pointQueryResponse.Success)
                ControlPointQueryResponseSignature(pointQueryResponse, secretKey, pointQueryRequest.HashVersion);
            return pointQueryResponse;
        }

        public RefundResponse Refund(RefundRequest refundRequest, string apiUrl, string secretKey)
        {
            refundRequest.SetSignature(secretKey);

            var refundResponse = RestCall<RefundResponse>(apiUrl, refundRequest, secretKey);

            if (refundResponse.Success && !refundResponse.WaitingApproval)
                refundResponse.ControlSignature(secretKey, refundRequest.HashVersion);
            return refundResponse;
        }

        public DirectPaymentRefundResponse DirectPaymentRefund(DirectPaymentRefundRequest refundRequest, string apiUrl, string secretKey)
        {
            refundRequest.SetSignature(secretKey);

            var refundResponse = RestCall<DirectPaymentRefundResponse>(apiUrl, refundRequest, secretKey);

            //if (refundResponse.Success && !refundResponse.WaitingApproval)
            //    refundResponse.ControlSignature(secretKey, refundRequest.HashVersion);
            return refundResponse;
        }

        public DeliveryApprovalResponse DeliveryApproval(DeliveryApprovalRequest deliveryApprovalRequest, string apiUrl, string secretKey)
        {
            deliveryApprovalRequest.SetSignature(secretKey);
            var deliveryApprovalResponse = RestCall<DeliveryApprovalResponse>(apiUrl, deliveryApprovalRequest,
             secretKey);

            if (deliveryApprovalResponse.Success)
                ControlDeliveryApprovalResponseSignature(deliveryApprovalResponse, secretKey,
                    deliveryApprovalRequest.HashVersion);

            return deliveryApprovalResponse;
        }

        protected virtual void ControlPointQueryResponseSignature(PointQueryResponse pointQueryResponse, string secretKey, string version)
        {
            var transactionId = (!string.IsNullOrEmpty(pointQueryResponse.TransactionId) ? pointQueryResponse.TransactionId : String.Empty);
            var transactionTime = (!string.IsNullOrEmpty(pointQueryResponse.TransactionTime) ? pointQueryResponse.TransactionTime : String.Empty);
            var currency = (!string.IsNullOrEmpty(pointQueryResponse.Currency) ? pointQueryResponse.Currency : String.Empty);
            //var installment = (pointQueryResponse.Installment.HasValue ? pointQueryResponse.Installment.Value.ToString() : String.Empty);
            var messageCode = pointQueryResponse.MessageCode;
            var text = secretKey + transactionId + transactionTime + pointQueryResponse.PointBalance + currency + messageCode; //+ installment + messageCode;

            string signature;
            if (version == "1.1")
                signature = CryptographyHelper.HMacSha512(text, secretKey);
            else
                signature = CryptographyHelper.HashSha256(text);

            if (signature != pointQueryResponse.Signature)
            {
                throw new DpayClientException(Constants.SignatureVerificationError, Constants.SignatureVerificationError.ReturnErrorMessage(), Constants.SignatureVerificationError.ReturnUserErrorMessage());
            }
        }

        protected virtual void ControlDeliveryApprovalResponseSignature(DeliveryApprovalResponse deliveryApprovalResponse, string secretKey, string version)
        {
            var transactionId = (!string.IsNullOrEmpty(deliveryApprovalResponse.TransactionId) ? deliveryApprovalResponse.TransactionId : String.Empty);
            var transactionTime = (!string.IsNullOrEmpty(deliveryApprovalResponse.TransactionTime) ? deliveryApprovalResponse.TransactionTime : String.Empty);
            var messageCode = deliveryApprovalResponse.MessageCode;
            var text = secretKey + transactionId + transactionTime + deliveryApprovalResponse.EarnedAmount + messageCode;

            string signature;
            if (version == "1.1")
                signature = CryptographyHelper.HMacSha512(text, secretKey);
            else
                signature = CryptographyHelper.HashSha256(text);

            if (signature != deliveryApprovalResponse.Signature)
            {
                throw new DpayClientException(Constants.SignatureVerificationError, Constants.SignatureVerificationError.ReturnErrorMessage(), Constants.SignatureVerificationError.ReturnUserErrorMessage());
            }
        }

        protected virtual T RestCall<T>(string apiUrl, BaseRequest reqModel, string secretKey)
        {
            var pointQueryResponse = _httpClient.Post<T>(apiUrl, reqModel, reqModel.ApiKey,
                secretKey, false);

            return pointQueryResponse;
        }

        public PostAuthResponse PostAuth(PostAuthRequest postAuthRequest, string apiUrl, string secretKey)
        {
            postAuthRequest.SetSignature(secretKey);

            var response = RestCall<PostAuthResponse>(apiUrl, postAuthRequest, secretKey);

            if (response.Success)
            {
                response.ControlSignature(secretKey);
            }
            return response;
        }

        public CreateThreedResponse CreateThreed(CreateThreedRequest createThreedRequestRequest, string apiUrl, string secretKey)
        {
            //ThreeD işleminden önce Signature oluşturma işlemi burada yapılır
            createThreedRequestRequest.SetSignature(secretKey);

            //ThreeD html formu oluşturulur
            var html = @"<form name=""payform"" action=""" + apiUrl + @""" method=""post"">
                        <input type=""hidden"" name=""Version"" value=""1.0"" />
                        <input type=""hidden"" name=""Transactionid"" value=""" + createThreedRequestRequest.TransactionId + @""" />
                        <input type=""hidden"" name=""ApiKey"" value=""" + createThreedRequestRequest.ApiKey + @""" />"
                        + (createThreedRequestRequest.Card == null ? "" : @"
                        <input type=""hidden"" name=""Card.CardNumber"" value=""" + createThreedRequestRequest.Card.CardNumber + @""" />
                        <input type=""hidden"" name=""Card.ExpireMonth"" value=""" + createThreedRequestRequest.Card.ExpireMonth + @""" />
                        <input type=""hidden"" name=""Card.ExpireYear"" value=""" + createThreedRequestRequest.Card.ExpireYear + @""" />
                        <input type=""hidden"" name=""Card.SecurityCode"" value=""" + createThreedRequestRequest.Card.SecurityCode + @""" />
                        <input type=""hidden"" name=""Card.CardHolderName"" value=""" + createThreedRequestRequest.Card.CardHolderName + @""" />
                        ") +
                        @"<input type=""hidden"" name=""Amount"" value=""" + createThreedRequestRequest.Amount + @""" />
                        <input type=""hidden"" name=""PointAmount"" value=""" + createThreedRequestRequest.PointAmount + @""" />"
                        + (createThreedRequestRequest.Customer == null ? "" : @"
                        <input type=""hidden"" name=""Customer.Email"" value=""" + createThreedRequestRequest.Customer.Email + @""" />
                        <input type=""hidden"" name=""Customer.IpAddress"" value=""" + createThreedRequestRequest.Customer.IpAddress + @""" />
                        ") +
                        @"<input type=""hidden"" name=""Installment"" value=""" + createThreedRequestRequest.Installment + @""" />
                        <input type=""hidden"" name=""SuccessUrl"" value=""" + createThreedRequestRequest.SuccessUrl + @""" />
                        <input type=""hidden"" name=""FailUrl"" value=""" + createThreedRequestRequest.FailUrl + @""" />
                        <input type=""hidden"" name=""ThreedReturnUrl"" value=""" + createThreedRequestRequest.ThreedReturnUrl + @""" />
                        <input type=""hidden"" name=""Signature"" value=""" + createThreedRequestRequest.Signature + @""" />
                        <input type=""hidden"" name=""Currency"" value=""" + createThreedRequestRequest.Currency + @""" />
                        <input type=""hidden"" name=""TransactionTime"" value=""" + createThreedRequestRequest.TransactionTime + @""" />
                        <input type=""hidden"" name=""SaveCreditCard"" value=""" + (createThreedRequestRequest.SaveCreditCard ? "1" : "0") + @""" />
                        <input type=""hidden"" name=""MerchantUserId"" value=""" + createThreedRequestRequest.MerchantUserId + @""" />
                        <input type=""hidden"" name=""MerchantCardUserId"" value=""" + createThreedRequestRequest.MerchantCardUserId + @""" />
                        <input type=""hidden"" name=""MerchantCardId"" value=""" + createThreedRequestRequest.MerchantCardId + @"""/>
                        <input type=""hidden"" name=""IsPreAuth"" value=""" + createThreedRequestRequest.IsPreAuth + @""" />
                        <input type=""hidden"" name=""HashVersion"" value=""" + createThreedRequestRequest.HashVersion + @""" />";
            if (createThreedRequestRequest.BasketItems != null)
            {
                var counter = 0;

                foreach (var item in createThreedRequestRequest.BasketItems)
                {
                    html += @"
                        <input type=""hidden"" name=""BasketItems[" + counter + @"].Amount"" value=""" + item.Amount + @""" />
                        <input type=""hidden"" name=""BasketItems[" + counter + @"].BasketItemId"" value=""" + item.BasketItemId + @""" />
                        <input type=""hidden"" name=""BasketItems[" + counter + @"].Description"" value=""" + item.Description + @""" />
                        <input type=""hidden"" name=""BasketItems[" + counter + @"].ProductCode"" value=""" + item.ProductCode + @""" />
                        <input type=""hidden"" name=""BasketItems[" + counter + @"].SubMerchantId"" value=""" + item.SubMerchantId + @""" />
                        <input type=""hidden"" name=""BasketItems[" + counter + @"].Url"" value=""" + item.Url + @""" />
                        <input type=""hidden"" name=""BasketItems[" + counter + @"].VatRatio"" value=""" + item.VatRatio + @""" />
                        <input type=""hidden"" name=""BasketItems[" + counter + @"].Count"" value=""" + item.Count + @""" />
                        <input type=""hidden"" name=""BasketItems[" + counter + @"].BasketItemType"" value=""" + item.BasketItemType + @""" />";
                    counter++;
                }
            }
            //document.payform.submit() ile oluşturulan formun, threeD adresine post edilmesi sağlanır. 
            html += @"</form>
                    <script type=""text/javascript"">
                        document.payform.submit();
                    </script>";
            var response = new CreateThreedResponse
            {
                HtmlForm = html
            };

            return response;
        }

        public CompleteThreedResponse CompleteThreed(CompleteThreedRequest completeThreedRequest, string apiUrl, string secretKey)
        {
            //Https Protocol bu şekilde gönderilmeli
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            //ThreeD işlemi tamamlama işlemi için Signature oluşturan metod
            completeThreedRequest.SetSignature(secretKey);

            //ApiUrl + /payments/complete3dpayment adresine hazırlanan request ile post işlemi yapılır. 
            var response = RestCall<CompleteThreedResponse>(apiUrl, completeThreedRequest, secretKey);

            //Hepsipay tarafından dönen signature doğruluğu kontrol edilir. Doğrulamak isterseniz kullanabilirsiniz.
            response.ControlSignature(secretKey);

            return response;
        }

        public FailedThreedResponse GetFailedThreedResponse(FormCollection formCollection, string secretKey)
        {
            var response = new FailedThreedResponse();

            var formDic = new Dictionary<string, string>(formCollection.Count,
                   StringComparer.OrdinalIgnoreCase);

            foreach (var formKey in formCollection.Keys)
            {

                formDic.Add((string)formKey, formCollection[(string)formKey]);
            }

            bool success;
            if (Boolean.TryParse(formDic["Success"], out success))
            {
                response.Success = success;
            }

            response.Message = formDic["Message"];
            response.UserMessage = formDic["UserMessage"];
            response.MessageCode = formDic["MessageCode"];
            response.TransactionId = formDic["TransactionId"];
            response.Installment = formDic["Installment"];
            response.Currency = formDic["Currency"];
            response.ApiKey = formDic["ApiKey"];
            response.Amount = formDic["Amount"];
            response.Signature = formDic["Signature"];
            response.TransactionTime = formDic["TransactionTime"];
            response.HashVersion = formDic["HashVersion"];
            response.BankResponseCode = formDic["BankResponseCode"];
            response.BankResponseMessage = formDic["BankResponseMessage"];

            //Signature alanı kontrol etmek isterseniz
            response.ControlSignature(secretKey);

            return response;
        }
    }
}
