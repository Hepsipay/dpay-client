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
    public class CardProcessorTests : UnitTestBase
    {
        [Test]
        public void Test_Get_Cards_Then_Return_GetCardsResponse()
        {
            var getCardResponse = Create<GetMerchantCardResponse>();
            var testableCardProcessor = new TestableGetCardProcessor(getCardResponse);

            var apiUrl = Create<Uri>();
            var secretKey = Create<string>();
            var requestModel = Create<GetMerchantCardRequest>();

            var result = testableCardProcessor.GetCard(requestModel, apiUrl.AbsoluteUri, secretKey);

            result.ShouldBeEquivalentTo(getCardResponse);
        }

        [Test]
        public void Test_Save_Card_Then_Return_SaveCardResponse()
        {
            var saveCardResponse = Create<SaveCardResponse>();
            var testableSaveCardProcessor = new TestableSaveCardProcessor(saveCardResponse);

            var apiUrl = Create<Uri>();
            var secretKey = Create<string>();
            var requestModel = Create<SaveCardRequest>();

            var result = testableSaveCardProcessor.SaveCard(requestModel, apiUrl.AbsoluteUri, secretKey);

            result.ShouldBeEquivalentTo(saveCardResponse);
        }
        
        [Test]
        public void Test_Delete_Card_Then_Return_DeleteCardResponse()
        {
            var deleteCardResponse = Create<DeleteCardResponse>();
            var testableDeleteCardProcessor = new TestableDeleteCardProcessor(deleteCardResponse);

            var apiUrl = Create<Uri>();
            var secretKey = Create<string>();
            var requestModel = Create<DeleteCardRequest>();

            var result = testableDeleteCardProcessor.DeleteCard(requestModel, apiUrl.AbsoluteUri, secretKey);

            result.ShouldBeEquivalentTo(deleteCardResponse);
        }
        
        [Test]
        public void Test_Update_Card_Then_Return_UpdateCardResponse()
        {
            var updateCardResponse = Create<UpdateCardResponse>();
            var testableUpdateCardProcessor = new TestableUpdateCardProcessor(updateCardResponse);

            var apiUrl = Create<Uri>();
            var secretKey = Create<string>();
            var requestModel = Create<UpdateCardRequest>();

            var result = testableUpdateCardProcessor.UpdateCard(requestModel, apiUrl.AbsoluteUri, secretKey);

            result.ShouldBeEquivalentTo(updateCardResponse);
        }
    }

    public class TestableGetCardProcessor : CardProcessor
    {
        private readonly GetMerchantCardResponse _getcardResponse;

        public TestableGetCardProcessor(GetMerchantCardResponse getcardResponse)
        {
            _getcardResponse = getcardResponse;
        }

        protected override T RestCallPost<T>(string apiUrl, BaseRequest reqModel, string secretKey)
        {
            return (T)Convert.ChangeType(_getcardResponse, typeof(T));
        }
    }

    public class TestableSaveCardProcessor : CardProcessor
    {
        private readonly SaveCardResponse _saveCardResponse;

        public TestableSaveCardProcessor(SaveCardResponse saveCardResponse)
        {
            _saveCardResponse = saveCardResponse;
        }

        protected override T RestCallPost<T>(string apiUrl, BaseRequest reqModel, string secretKey)
        {
            return (T)Convert.ChangeType(_saveCardResponse, typeof(T));
        }
    }

    public class TestableDeleteCardProcessor : CardProcessor
    {
        private readonly DeleteCardResponse _deleteCardResponse;

        public TestableDeleteCardProcessor(DeleteCardResponse deleteCardResponse)
        {
            _deleteCardResponse = deleteCardResponse;
        }

        protected override T RestCallDelete<T>(string apiUrl, BaseRequest reqModel, string secretKey)
        {
            return (T)Convert.ChangeType(_deleteCardResponse, typeof(T));
        }
    }
    
    public class TestableUpdateCardProcessor : CardProcessor
    {
        private readonly UpdateCardResponse _updateCardResponse;

        public TestableUpdateCardProcessor(UpdateCardResponse updateCardResponse)
        {
            _updateCardResponse = updateCardResponse;
        }

        protected override T RestCallPut<T>(string apiUrl, BaseRequest reqModel, string secretKey)
        {
            return (T)Convert.ChangeType(_updateCardResponse, typeof(T));
        }
    }
}
