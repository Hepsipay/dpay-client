using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;
using Dpay.Client.Models;
using Dpay.Client.Models.Request;
using Dpay.Client.Processors.Impl;
using Dpay.Client.Processors.Interfaces;
using Dpay.Client.Web.Models;

namespace Dpay.Client.Web.Controllers
{
    public class CommonPaymentController : Controller
    {
        private readonly ICommonPaymentProcessor _commonPaymentProcessor;

        private readonly string _apiKey;
        private readonly string _secretKey;
        private readonly string _hashVersion;
        private readonly string _baseUrl;
        private readonly string _commonPaymentUrl;
        private readonly string _commonPaymentApiUrl;

        public CommonPaymentController()
        {
            _commonPaymentProcessor = new CommonPaymentProcessor();
            _apiKey = ConfigurationManager.AppSettings["ApiKey"];
            _secretKey = ConfigurationManager.AppSettings["SecretKey"];
            _hashVersion = ConfigurationManager.AppSettings["HashVersion"];
            _baseUrl = ConfigurationManager.AppSettings["ReturnBaseUrl"];
            _commonPaymentUrl = ConfigurationManager.AppSettings["CommonPaymentUrl"];
            _commonPaymentApiUrl = ConfigurationManager.AppSettings["CommonPaymentApiUrl"];
        }

        public ActionResult Save()
        {
            var request = new SaveCommonPaymentRequest
            {
                Version = "1.0",
                ApiKey = _apiKey,
                TransactionId = Guid.NewGuid().ToString("N"),
                TransactionTime = "1443600845",
                Amount = 5499,
                Description = "E-ticaretÖdemesi",
                Currency = "TRY",
                BasketItems = new List<BasketItem>
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
                Customer = new Customer
                {
                    Name = "Ahmet",
                    Surname = "Mehmet",
                    Email = "ahmetmehmet@ahmetmarket.com.tr",
                    PhoneNumber = "5337654321",
                    Code = "7cefdf61-38cd-4b35-b5f0-4c98c5805d41",
                    IpAddress = "127.0.0.1"
                },
                ShippingAddress = new ShippingAddress
                {
                    Name = "Ahmet Mehmet",
                    Address = "Kuştepe Mahallesi Mecidiyeköy Yolu Cad. No:12 Trump Towers Kule:2 Kat:11 ŞİŞLİ",
                    Country = "Türkiye",
                    CountryCode = "TUR",
                    City = "İstanbul",
                    CityCode = "34",
                    ZipCode = "34580"
                },
                InvoiceAddress = new InvoiceAddress
                {
                    Name = "Ahmet Mehmet",
                    Address = "Kuştepe Mahallesi Mecidiyeköy Yolu Cad. No:12 Trump Towers Kule:2 Kat:11 ŞİŞLİ",
                    Country = "Türkiye",
                    CountryCode = "TUR",
                    City = "İstanbul",
                    CityCode = "34",
                    ZipCode = "34580"
                },
                Extras = new List<Extra> {new Extra {Key = "INT_SPRS_KODU", Value = "spr_123456789"}},
                HashVersion = _hashVersion,
                AmountEditable = true,
                CommissionAmountReflect = 1,
                HasInstallmentChoice = true,
                ReturnUrl = _baseUrl + "/CommonPayment/Result"
            };

            var apiUrl = _commonPaymentApiUrl + "/commonpayments/save";

            var response = _commonPaymentProcessor.Save(request, apiUrl, _secretKey);

            var redirectUrl = _commonPaymentUrl + "/" + response.CommonPaymentUniqueId;

            return Redirect(redirectUrl);
        }

        public ActionResult Query()
        {
            var request = new CommonPaymentQueryRequest
            {
                ApiKey = _apiKey,
                TransactionId = "TestCommonPaymentSave00001",
                CommonPaymentUniqueId = "9617ba1b05aa41e0b6f790306b7303b6",
                HashVersion = _hashVersion
            };

            var apiUrl = _commonPaymentApiUrl + "/commonpayments/query";

            var response = _commonPaymentProcessor.Query(request, apiUrl, _secretKey);

            return View(new CommonPaymentResultModel
            {
                Amount = response.Amount,
                ApiKey = response.ApiKey,
                Currency = response.Currency,
                Installment = response.Installment,
                Success = response.Success,
                MessageCode = response.MessageCode,
                Message = response.Message,
                UserMessage = response.UserMessage,
                TransactionId = response.TransactionId,
                TransactionTime = response.TransactionTime,
                CommonPaymentUniqueId = response.CommonPaymentUniqueId,
                PaymentTransactionReponseMessage = response.PaymentTransactionReponseMessage,
                PaymentTransactionResponseCode = response.PaymentTransactionResponseCode,
                Status = response.Status
            });
        }

        public ActionResult Result(CommonPaymentResultModel commonPaymentResultModel)
        {
            return View(commonPaymentResultModel);
        }
    }
}