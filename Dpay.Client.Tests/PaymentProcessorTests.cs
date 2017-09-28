using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Configuration;
using Dpay.Client.Helpers;
using FluentAssertions;
using NUnit.Framework;
using Dpay.Client.Models.Response;
using Dpay.Client.Models.Request;
using Dpay.Client.Processors;
using Dpay.Client.Processors.Impl;
using Moq;

namespace Dpay.Client.Tests
{
    [TestFixture]
    public class PaymentProcessorTests : UnitTestBase
    {
        [Test]
        public void PointQuery_ShouldBeReturn_PointQueryResponseCreatedForTest()
        {
            var pointQueryResponse = Create<PointQueryResponse>();
            var testablePointQueryProcessor = new TestablePointQueryProcessor(pointQueryResponse);

            var apiUrl = Create<Uri>();
            var secretKey = Create<string>();
            var pointQueryRequest = Create<PointQueryRequest>();

            pointQueryRequest.SetSignature(secretKey);

            var result = testablePointQueryProcessor.PointQuery(pointQueryRequest, apiUrl.AbsoluteUri, secretKey);

            result.ShouldBeEquivalentTo(pointQueryResponse);
        }

        [Test]
        public void DeliveryApproval()
        {
            var deliveryApprovalResponse = Create<DeliveryApprovalResponse>();
            var testablePointQueryProcessor = new TestableDeliveryApprovalProcessor(deliveryApprovalResponse);

            var apiUrl = Create<Uri>();
            var secretKey = Create<string>();
            var deliveryApprovalRequest = Create<DeliveryApprovalRequest>();

            deliveryApprovalRequest.SetSignature(secretKey);

            var result = testablePointQueryProcessor.DeliveryApproval(deliveryApprovalRequest, apiUrl.AbsoluteUri, secretKey);

            result.ShouldBeEquivalentTo(deliveryApprovalResponse);
        }

        [Test]
        public void DeliveryApprovalFeatureClosed()
        {
            var deliveryApprovalResponse = Create<DeliveryApprovalResponse>();
            var testablePointQueryProcessor = new TestableDeliveryApprovalProcessor(deliveryApprovalResponse);

            var apiUrl = Create<Uri>();
            var secretKey = Create<string>();
            var deliveryApprovalRequest = Create<DeliveryApprovalRequest>();

            deliveryApprovalRequest.SetSignature(secretKey);

            var result = testablePointQueryProcessor.DeliveryApproval(deliveryApprovalRequest, apiUrl.AbsoluteUri, secretKey);

            result.ShouldBeEquivalentTo(deliveryApprovalResponse);
        }
    }

    public class TestablePointQueryProcessor : PaymentProcessor
    {
        private readonly PointQueryResponse _pointQueryResponse;

        public TestablePointQueryProcessor(PointQueryResponse pointQueryResponse)
        {
            _pointQueryResponse = pointQueryResponse;
        }

        protected override void ControlPointQueryResponseSignature(PointQueryResponse pointQueryResponse, string secretKey, string version)
        {

        }

        protected override T RestCall<T>(string apiUrl, BaseRequest reqModel, string secretKey)
        {
            return (T)Convert.ChangeType(_pointQueryResponse, typeof(T));
        }
    }

    public class TestableDeliveryApprovalProcessor : PaymentProcessor
    {
        private readonly DeliveryApprovalResponse _deliveryApprovalResponse;

        public TestableDeliveryApprovalProcessor(DeliveryApprovalResponse deliveryApprovalResponse)
        {
            _deliveryApprovalResponse = deliveryApprovalResponse;
        }

        protected override void ControlDeliveryApprovalResponseSignature(DeliveryApprovalResponse deliveryApprovalResponse, string secretKey, string version)
        {
        }

        protected override T RestCall<T>(string apiUrl, BaseRequest reqModel, string secretKey)
        {
            return (T)Convert.ChangeType(_deliveryApprovalResponse, typeof(T));
        }
    }

}

