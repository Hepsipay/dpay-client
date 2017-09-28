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
    public class InstallmentProcessorTests : UnitTestBase
    {
        [Test]
        public void Test_Get_Installments_Then_Return_GetInstallmentsResponse()
        {
            var installmentResponse = Create<GetInstallmentsResponse>();
            var testableInstallmentProcessor = new TestableInstallmentProcessor(installmentResponse);

            var apiUrl = Create<Uri>();
            var secretKey = Create<string>();
            var pointQueryRequest = Create<GetInstallmentsRequest>();
            
            var result = testableInstallmentProcessor.GetInstallments(pointQueryRequest, apiUrl.AbsoluteUri, secretKey);

            result.ShouldBeEquivalentTo(installmentResponse);
        }
    }
    
    public class TestableInstallmentProcessor : InstallmentProcessor
    {
        private readonly GetInstallmentsResponse _installmentResponse;

        public TestableInstallmentProcessor(GetInstallmentsResponse installmentResponse)
        {
            _installmentResponse = installmentResponse;
        }
        protected override T RestCallPost<T>(string apiUrl, BaseRequest reqModel, string secretKey)
        {
            return (T)Convert.ChangeType(_installmentResponse, typeof(T));
        }
    }

}
