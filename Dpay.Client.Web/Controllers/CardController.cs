using System;
using System.Configuration;
using System.Web.Mvc;
using Dpay.Client.Models;
using Dpay.Client.Models.Request;
using Dpay.Client.Processors.Impl;
using Dpay.Client.Processors.Interfaces;
using Dpay.Client.Web.Models;

namespace Dpay.Client.Web.Controllers
{
    public class CardController : Controller
    {
        private readonly ICardProcessor _cardProcessor;

        private readonly string _apiKey;
        private readonly string _secretKey;
        private readonly string _apiUrl;

        public CardController()
        {
            _cardProcessor = new CardProcessor();
            _apiKey = ConfigurationManager.AppSettings["ApiKey"];
            _secretKey = ConfigurationManager.AppSettings["SecretKey"];
            _apiUrl = ConfigurationManager.AppSettings["ApiUrl"];
        }

        // Kart Kaydetme
        public ActionResult Save()
        {
            var request = new SaveCardRequest
            {
                ApiKey = _apiKey,
                CardHolderFullName = "Ahmet Mehmet",
                CardNumber = "4506347011448053",
                ExpireMonth = "02",
                ExpireYear = "20",
                Email = "ahmetmehmet@hepsipay.com",
                MerchantCardUserId = "1234421",
                MerchantUserId = "131231",
                ReferenceId1 = "14324234"
            };

            var apiUrl = _apiUrl + "/cards";

            var response = _cardProcessor.SaveCard(request, apiUrl, _secretKey);

            return View(new CardResultModel
            {
                Id = response.Id,
                MaskedCardNumber = response.MaskedCardNumber,
                FullName = response.FullName,
                MerchantUserId = response.MerchantUserId,
                Success = response.Success,
                MessageCode = response.MessageCode,
                Message = response.Message,
                UserMessage = response.UserMessage,
                MerchantCardUserId = response.MerchantCardUserId,
                Email = response.Email,
                ReferenceId1 = response.ReferenceId1,
                IsSuccess = response.IsSuccess
            });
        }

        // Kart Silme
        public ActionResult Delete()
        {
            var request = new DeleteCardRequest
            {
                ApiKey = _apiKey,
                Id = Guid.Parse("cf236f4d-4a74-4d7f-b54e-a75c00fbc622"),
                MerchantCardUserId = "1234421",
                MerchantUserId = "131231"
            };

            var apiUrl = _apiUrl + "/cards";

            var response = _cardProcessor.DeleteCard(request, apiUrl, _secretKey);

            return View(new CardResultModel()
            {
                Success = response.Success,
                MessageCode = response.MessageCode,
                Message = response.Message,
                UserMessage = response.UserMessage,
                MerchantCardDtos = response.MerchantCardDtos
            });
        }

        // Kart Güncelleme
        public ActionResult Update()
        {
            var request = new UpdateCardRequest
            {
                ApiKey = _apiKey,
                Id = Guid.Parse("cf236f4d-4a74-4d7f-b54e-a75c00fbc622"),
                ExpireMonth = "02",
                ExpireYear = "20",
                MerchantCardUserId = "1234421",
                MerchantUserId = "131231"
            };

            var apiUrl = _apiUrl + "/cards";

            var response = _cardProcessor.UpdateCard(request, apiUrl, _secretKey);

            return View(new CardResultModel
            {
                Id = response.Id,
                MaskedCardNumber = response.MaskedCardNumber,
                FullName = response.FullName,
                MerchantUserId = response.MerchantUserId,
                Success = response.Success,
                MessageCode = response.MessageCode,
                Message = response.Message,
                UserMessage = response.UserMessage,
                MerchantCardUserId = response.MerchantCardUserId,
                Email = response.Email,
                ReferenceId1 = response.ReferenceId1
            });
        }

        // Kart Alma
        public ActionResult Get()
        {
            var request = new GetMerchantCardRequest
            {
                ApiKey = _apiKey,
                Id = Guid.Parse("cf236f4d-4a74-4d7f-b54e-a75c00fbc622"),
                MerchantCardUserId = "1234421",
                MerchantUserId = "131231",
                Email = "ahmetmehmet@hepsipay.com",
                ReferenceId1 = "14324234",
                PagerInputDto = new PagerInputModel { PageIndex = 1, PageSize = 10 }
            };

            var apiUrl = _apiUrl + "/cards/merchantquery";

            var response = _cardProcessor.GetCard(request, apiUrl, _secretKey);

            return View(new CardResultModel()
            {
                Success = response.Success,
                MessageCode = response.MessageCode,
                Message = response.Message,
                UserMessage = response.UserMessage,
                MerchantCardDtos = response.MerchantCardDtos
            });
        }
    }
}