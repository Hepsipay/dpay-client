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
    public class StandardPaymentController : Controller
    {
        private readonly IPaymentProcessor _paymentProcessor;

        private readonly string _apiKey;
        private readonly string _secretKey;
        private readonly string _hashVersion;
        private readonly string _apiUrl;

        public StandardPaymentController()
        {
            _paymentProcessor = new PaymentProcessor();
            _apiKey = ConfigurationManager.AppSettings["ApiKey"];
            _secretKey = ConfigurationManager.AppSettings["SecretKey"];
            _hashVersion = ConfigurationManager.AppSettings["HashVersion"];
            _apiUrl = ConfigurationManager.AppSettings["ApiUrl"];
        }

        // Satış
        public ActionResult Sale()
        {
            var paymentRequest = new PaymentRequest
            {
                Version = "1.0",
                ApiKey = _apiKey,
                TransactionId = "TestSale00003",
                TransactionTime = "1443600845",
                Amount = 5499,
                Description = "E-ticaretÖdemesi",
                Currency = "TRY",
                Installment = 1,
                Card = new Card
                {
                    CardHolderName = "Ahmet Mehmet",
                    CardNumber = "4531444531442283",
                    ExpireMonth = "12",
                    ExpireYear = "18",
                    SecurityCode = "001"
                },
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
                Extras = new List<Extra> { new Extra { Key = "INT_SPRS_KODU", Value = "spr_123456789" } },
                MerchantCardId = "",
                SaveCreditCard = true,
                HashVersion = _hashVersion
            };

            var apiUrl = _apiUrl + "/payments/pay";

            var paymentResponse = _paymentProcessor.Pay(paymentRequest, apiUrl, _secretKey);

            return View(new ResultModel
            {
                Amount = paymentResponse.Amount,
                ApiKey = paymentResponse.ApiKey,
                Currency = paymentResponse.Currency,
                Installment = paymentResponse.Installment,
                Success = paymentResponse.Success,
                MessageCode = paymentResponse.MessageCode,
                Message = paymentResponse.Message,
                UserMessage = paymentResponse.UserMessage,
                TransactionId = paymentResponse.TransactionId,
                TransactionTime = paymentResponse.TransactionTime,
                CardId = paymentResponse.CardId
            });
        }

        // Puan Sorgulama
        public ActionResult PointQuery()
        {
            var request = new PointQueryRequest
            {
                Version = "1.0",
                ApiKey = _apiKey,
                TransactionId = "TestPointQuery00002",
                TransactionTime = "1443600845",
                Card = new Card
                {
                    CardHolderName = "Ahmet Mehmet",
                    CardNumber = "4506347011448053",
                    ExpireMonth = "02",
                    ExpireYear = "20",
                    SecurityCode = "000"
                },
                Currency = "TRY",
                Description = "E-ticaretÖdemesi",
                HashVersion = _hashVersion
            };

            var apiUrl = _apiUrl + "/payments/points/query";

            var response = _paymentProcessor.PointQuery(request, apiUrl, _secretKey);

            return View(new ResultModel
            {
                ApiKey = response.ApiKey,
                Currency = response.Currency,
                Installment = response.Installment,
                Success = response.Success,
                MessageCode = response.MessageCode,
                Message = response.Message,
                UserMessage = response.UserMessage,
                TransactionId = response.TransactionId,
                TransactionTime = response.TransactionTime
            });
        }

        // İade
        public ActionResult Refund()
        {
            var request = new RefundRequest
            {
                Version = "1.0",
                ApiKey = _apiKey,
                TransactionId = "TestRefund00002",
                TransactionTime = "1443600845",
                Amount = 5499,
                Currency = "TRY",
                BasketItems = new List<RefundBasketItem>
                {
                    new RefundBasketItem
                    {
                        BasketItemId = "BoyamaKalemSeti",
                        Amount = 8750,
                        MerchantOffsetAmount = 18,
                        SubMerchantOffsetAmount = 1
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
                ReferenceTransactionId = "TestSale00002",
                HashVersion = _hashVersion
            };

            var apiUrl = _apiUrl + "/payments/refund";

            var response = _paymentProcessor.Refund(request, apiUrl, _secretKey);

            return View(new ResultModel
            {
                Amount = response.Amount,
                ApiKey = response.ApiKey,
                Currency = response.Currency,
                Success = response.Success,
                MessageCode = response.MessageCode,
                Message = response.Message,
                UserMessage = response.UserMessage,
                TransactionId = response.TransactionId,
                TransactionTime = response.TransactionTime
            });
        }

        // Ön Provizyon
        public ActionResult PreAuth()
        {
            var request = new PaymentRequest
            {
                Version = "1.0",
                ApiKey = _apiKey,
                TransactionId = "TestPreAuth00006",
                TransactionTime = "1443600845",
                Amount = 5499,
                Description = "E-ticaretÖdemesi",
                Currency = "TRY",
                Installment = 1,
                Card = new Card
                {
                    CardHolderName = "Ahmet Mehmet",
                    CardNumber = "4506347011448053",
                    ExpireMonth = "02",
                    ExpireYear = "20",
                    SecurityCode = "000"
                },
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
                Extras = new List<Extra> { new Extra { Key = "INT_SPRS_KODU", Value = "spr_123456789" } },
                MerchantCardId = "",
                SaveCreditCard = true,
                IsPreAuth = true,
                HashVersion = _hashVersion
            };

            var apiUrl = _apiUrl + "/payments/pay";

            var response = _paymentProcessor.Pay(request, apiUrl, _secretKey);

            return View(new ResultModel
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
                CardId = response.CardId
            });
        }

        // Ön Provizyon Kapama
        public ActionResult PostAuth()
        {
            var request = new PostAuthRequest
            {
                Version = "1.0",
                ApiKey = _apiKey,
                TransactionId = "TestPostAuth00005",
                TransactionTime = "1443600845",
                Amount = 5499,
                Currency = "TRY",
                Installment = 2,
                Customer = new Customer
                {
                    Name = "Ahmet",
                    Surname = "Mehmet",
                    Email = "ahmetmehmet@ahmetmarket.com.tr",
                    PhoneNumber = "5337654321",
                    Code = "7cefdf61-38cd-4b35-b5f0-4c98c5805d41",
                    IpAddress = "127.0.0.1"
                },
                ReferenceTransactionId = "TestPreAuth00006",
                HashVersion = _hashVersion
            };

            var apiUrl = _apiUrl + "/payments/postauth";

            var response = _paymentProcessor.PostAuth(request, apiUrl, _secretKey);

            return View(new ResultModel
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
                TransactionTime = response.TransactionTime
            });
        }
    }
}