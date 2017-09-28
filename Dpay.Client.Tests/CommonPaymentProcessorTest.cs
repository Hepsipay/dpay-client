using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dpay.Client.Models.Request;
using Dpay.Client.Models.Response;
using Dpay.Client.Processors.Impl;
using FluentAssertions;
using NUnit.Framework;

namespace Dpay.Client.Tests
{
    [TestFixture]
    public class CommonPaymentProcessorTest : UnitTestBase
    {
        [Test]
        public void Test_Save_CommonPayment_Then_Return_SaveCommonPaymentResponse()
        {
            var saveComPaymentResponse = Create<SaveCommonPaymentResponse>();
            var testableDeleteCardProcessor = new TestableCommonPaymentProcessor(saveComPaymentResponse);

            var apiUrl = Create<Uri>();
            var secretKey = Create<string>();
            var requestModel = Create<SaveCommonPaymentRequest>();

            var result = testableDeleteCardProcessor.Save(requestModel, apiUrl.AbsoluteUri, secretKey);

            result.ShouldBeEquivalentTo(saveComPaymentResponse);
        }

        [Test]
        public void Test_Query_CommonPayment_Then_Return_QueryCommonPaymentResponse()
        {
            var response = Create<CommonPaymentQueryResponse>();
            var testableProcessor = new TestableCommonPaymentQueryProcessor(response);

            var apiUrl = Create<Uri>();
            var secretKey = Create<string>();
            var requestModel = Create<CommonPaymentQueryRequest>();

            var result = testableProcessor.Query(requestModel, apiUrl.AbsoluteUri, secretKey);

            result.ShouldBeEquivalentTo(response);
        }
    }
    
    public class TestableCommonPaymentProcessor : CommonPaymentProcessor
    {
        private readonly SaveCommonPaymentResponse _response;

        public TestableCommonPaymentProcessor(SaveCommonPaymentResponse response)
        {
            _response = response;
        }

        protected override T RestCallPost<T>(string apiUrl, BaseRequest reqModel, string secretKey, bool istokenRequired)
        {
            return (T)Convert.ChangeType(_response, typeof(T));
        }

        protected override void ControlSaveSignature(SaveCommonPaymentRequest reqModel, SaveCommonPaymentResponse resModel, string secretKey)
        {
        }
    }

    public class TestableCommonPaymentQueryProcessor : CommonPaymentProcessor
    {
        private readonly CommonPaymentQueryResponse _response;

        public TestableCommonPaymentQueryProcessor(CommonPaymentQueryResponse response)
        {
            _response = response;
        }

        protected override T RestCallPost<T>(string apiUrl, BaseRequest reqModel, string secretKey, bool istokenRequired)
        {
            return (T)Convert.ChangeType(_response, typeof(T));
        }

        protected override void ControlQuerySignature(CommonPaymentQueryRequest reqModel, CommonPaymentQueryResponse resModel, string secretKey)
        {
        }
    }
}
