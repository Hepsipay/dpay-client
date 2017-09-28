using System;
using System.Web.Configuration;
using FluentAssertions;
using NUnit.Framework;
using Dpay.Client.Processors;
using Dpay.Client.Models.Request;
using Dpay.Client.Models.Response;
using Dpay.Client.Processors.Impl;

namespace Dpay.Client.Tests
{
    [TestFixture]
    [Ignore]
    public class PaymentProcessorIntegrationTests : UnitTestBase
    {
        readonly PaymentProcessor _paymentProcessor = new PaymentProcessor();

        [Test]
        public void DeliveryApproval()
        {
            DeliveryApprovalRequest request = new DeliveryApprovalRequest()
            { 
                ApiKey = "cab15d0f7d3247ca8e52e6d977576a4a",
                TransactionTime = ((int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds).ToString(),
                TransactionId = "e69b90f919ec4994b3821d5c0add3f4e",
                BasketItemId = "1",
                EarnedAmount = 10,
                Version = "v1.0"
            };
            DeliveryApprovalResponse deliveryApprovalResponse = _paymentProcessor.DeliveryApproval(request, WebConfigurationManager.AppSettings["ApiUrl"] + "/Payments/delivery-approval", "123");

            deliveryApprovalResponse.TransactionId.Should().Be(request.TransactionId);
            deliveryApprovalResponse.MessageCode.Should().Be("0000");
        }
    }
}