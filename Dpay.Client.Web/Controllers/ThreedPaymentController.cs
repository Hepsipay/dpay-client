using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dpay.Client.Models;
using Dpay.Client.Models.Request;
using Dpay.Client.Processors.Impl;
using Dpay.Client.Processors.Interfaces;
using Dpay.Client.Web.Models;

namespace Dpay.Client.Web.Controllers
{
    public class ThreedPaymentController : Controller
    {
        private readonly IPaymentProcessor _paymentProcessor;

        private readonly string _apiKey;
        private readonly string _secretKey;
        private readonly string _hashVersion;
        private readonly string _baseUrl;
        private readonly string _apiUrl;
        private readonly string _hepsipayUrl;

        public ThreedPaymentController()
        {
            _paymentProcessor = new PaymentProcessor();
            _apiKey = ConfigurationManager.AppSettings["ApiKey"]; //Merchant ApiKey
            _secretKey = ConfigurationManager.AppSettings["SecretKey"]; //Merchant SecretKey
            _hashVersion = ConfigurationManager.AppSettings["HashVersion"]; //Hasversion
            _baseUrl = ConfigurationManager.AppSettings["ReturnBaseUrl"]; //Merchant redirect base url
            _apiUrl = ConfigurationManager.AppSettings["ApiUrl"]; //ThreeD işlemi tamamlama işleminin gönderileceği Hepsipay api adresi
            _hepsipayUrl = ConfigurationManager.AppSettings["HepsipayUrl"]; //ThreeD işlemi için ilk post işleminin gönderileceği Hepsipay adresi
        }

        /// <summary>
        /// ThreeD satış işlemi hazırlama 
        /// </summary>
        /// <returns></returns>
        public ActionResult Sale()
        {
            var createThreedRequest = new CreateThreedRequest
            {
                ApiKey = _apiKey, //Merchant ApiKey
                TransactionId = "NGTT201705291607455972", //Her satış işlemi için gönderilen farklı Sipariş Numarası
                TransactionTime = "1443600845", //İşlem zamanı, unix timestamp
                Amount = 5499, //İşlem Tutarı
                Description = "E-ticaretÖdemesi", //İşlem Açıklaması
                Currency = "TRY", //Para birimi
                Installment = 1, //Taksit sayısı, 1 olarak gönderirseniz tek çekim(peşin) işlem olacaktır
                Card = new Card //Kart bilgileri
                {
                    CardHolderName = "Ahmet Mehmet",
                    CardNumber = "4355084355084358",
                    ExpireMonth = "12",
                    ExpireYear = "18",
                    SecurityCode = "000"
                },
                BasketItems = new List<BasketItem> //Sepet bilgisi
                {
                    new BasketItem
                    {
                        Description = "BoyamaKalemSeti",
                        ProductCode = "7cefdf61-38cd-4b35-b5f0-4c98c5805d41",
                        Amount = 8750,
                        VatRatio = 18,
                        Count = 1,
                        Url = "http://www.ahmetmarket.com.tr/boyama-kalem-seti"
                    },
                    new BasketItem
                    {
                        Description = "BoyamaKitabı",
                        ProductCode = "7cefdf61-38cd-4b35-b5f0-4c98c5805d41",
                        Amount = 2550,
                        VatRatio = 18,
                        Count = 3,
                        Url = "http://www.ahmetmarket.com.tr/boyama-kitabi"
                    },
                    new BasketItem
                    {
                        Description = "KargoBedeli",
                        Amount = 1000,
                        VatRatio = 18,
                        Count = 1
                    }
                },
                Customer = new Customer //Müşteri bilgileri
                {
                    Email = "ahmetmehmet@ahmetmarket.com.tr",
                    IpAddress = ""
                },
                //ShippingAddress = new ShippingAddress //Teslimat Adresi
                //{
                //    Name = "Ahmet Mehmet",
                //    Address = "Kuştepe Mahallesi Mecidiyeköy Yolu Cad. No:12 Trump Towers Kule:2 Kat:11 ŞİŞLİ",
                //    Country = "Türkiye",
                //    CountryCode = "TUR",
                //    City = "İstanbul",
                //    CityCode = "34",
                //    ZipCode = "34580"
                //},
                //InvoiceAddress = new InvoiceAddress //Fatura Adresi
                //{
                //    Name = "Ahmet Mehmet",
                //    Address = "Kuştepe Mahallesi Mecidiyeköy Yolu Cad. No:12 Trump Towers Kule:2 Kat:11 ŞİŞLİ",
                //    Country = "Türkiye",
                //    CountryCode = "TUR",
                //    City = "İstanbul",
                //    CityCode = "34",
                //    ZipCode = "34580"
                //},
                //Extras = new List<Extra> { new Extra { Key = "INT_SPRS_KODU", Value = "spr_123456789" } }, //Extra alanında gönderdiğiniz değerler, işlem sonunda size tekrar gönderilir.
                SuccessUrl = _baseUrl + "/ThreedPayment/SuccessfulResult", //Başarılı işlem redirect adresi
                FailUrl = _baseUrl + "/ThreedPayment/FailedResult",//Başarısız işlem redirect adresi
                //Priority = 1, //Fraud için gerekli alan
                //VisitorId = "12312312", //Fraud için gerekli her müşterinize verdiğiniz unique id
                //UserKey = "adasdas2222", //Fraud için gerekli
                //DiscountAmount = 4010,//Fraud için gerekli
                //GiftCheqAmount = 5600,//Fraud için gerekli
                HashVersion = _hashVersion//1.1 olarak gönderilmeli                
            };

            var apiUrl = _hepsipayUrl + "/payment/ThreeDSecureV2"; //ThreeD post adresi

            //Signature oluşturma, threeD html sayfası oluşturma işlemleri için metodu inceleyiniz.
            var createThreedResponse = _paymentProcessor.CreateThreed(createThreedRequest, apiUrl, _secretKey);

            return Content(createThreedResponse.HtmlForm);
        }

        /// <summary>
        /// ThreeD doğrulama ekranından dönen cevap ile işlemin tamamlanmasının sağlandığı metod.
        /// </summary>
        /// <param name="threedSuccessfulResultModel">İşlem başarılı olduğunda dolu gelecektir. Başarısız işlemlerde null gönderilir.</param>
        /// <returns></returns>
        public ActionResult SuccessfulResult(ThreedSuccessfulResultModel threedSuccessfulResultModel)
        {
            var completeThreedRequest = new CompleteThreedRequest
            {
                EncryptedThreedResult = threedSuccessfulResultModel.EncryptedThreedResult,
                ApiKey = _apiKey,
                HashVersion = _hashVersion
            };

            var apiUrl = _apiUrl + "/payments/complete3dpayment";

            var completeThreedResponse = _paymentProcessor.CompleteThreed(completeThreedRequest, apiUrl, _secretKey);

            //ThreeD işlem sonucu
            var resultModel = new ResultModel
            {
                Amount = completeThreedResponse.Amount,
                ApiKey = completeThreedResponse.ApiKey,
                CardId = completeThreedResponse.CardId,
                Currency = completeThreedResponse.Currency,
                FailUrl = completeThreedResponse.FailUrl,
                SuccessUrl = completeThreedResponse.SuccessUrl,
                Success = completeThreedResponse.Success,
                MessageCode = completeThreedResponse.MessageCode,
                Message = completeThreedResponse.Message,
                UserMessage = completeThreedResponse.UserMessage,
                Installment = completeThreedResponse.Installment,
                TransactionTime = completeThreedResponse.TransactionTime,
                TransactionId = completeThreedResponse.TransactionId,
                SaveCreditCard = completeThreedResponse.SaveCreditCard,
                TransactionType = completeThreedResponse.TransactionType,
                ThreeDHostAddress = completeThreedResponse.ThreeDHostAddress
            };

            //Result işleminin görüntülendiği sayfa
            return View("Result", resultModel);
        }

        /// <summary>
        /// Başarısız işlemin gönderildiği end point.
        /// </summary>
        /// <param name="formCollection"></param>
        /// <returns></returns>
        public ActionResult FailedResult(FormCollection formCollection)
        {
            //Form collection içinde hata mesaj içeriği gönderilecektir.
            var failedThreedResponse = _paymentProcessor.GetFailedThreedResponse(formCollection, _secretKey);

            var resultModel = new ResultModel
            {
                ApiKey = failedThreedResponse.ApiKey,
                Currency = failedThreedResponse.Currency,
                Success = failedThreedResponse.Success,
                MessageCode = failedThreedResponse.MessageCode,
                Message = failedThreedResponse.Message,
                UserMessage = failedThreedResponse.UserMessage,
                TransactionTime = failedThreedResponse.TransactionTime,
                TransactionId = failedThreedResponse.TransactionId,
                BankResponseCode = failedThreedResponse.BankResponseCode,
                BankResponseMessage = failedThreedResponse.BankResponseMessage
            };
            int installment;
            if (int.TryParse(failedThreedResponse.Installment, out installment))
            {
                resultModel.Installment = installment;
            }
            int amount;
            if (int.TryParse(failedThreedResponse.Amount, out amount))
            {
                resultModel.Amount = amount;
            }
            return View("Result", resultModel);
        }
    }
}