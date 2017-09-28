using System.Configuration;
using System.Web.Mvc;
using Dpay.Client.Models.Request;
using Dpay.Client.Processors.Impl;
using Dpay.Client.Processors.Interfaces;
using Dpay.Client.Web.Models;

namespace Dpay.Client.Web.Controllers
{
    public class InstallmentController : Controller
    {
        private readonly IInstallmentProcessor _installmentProcessor;

        private readonly string _apiKey;
        private readonly string _secretKey;
        private readonly string _apiUrl;

        public InstallmentController()
        {
            _installmentProcessor = new InstallmentProcessor();
            _apiKey = ConfigurationManager.AppSettings["ApiKey"];
            _secretKey = ConfigurationManager.AppSettings["SecretKey"];
            _apiUrl = ConfigurationManager.AppSettings["ApiUrl"];
        }

        // Taksit Sorgulama
        public ActionResult Get()
        {
            var request = new GetInstallmentsRequest
            {
                ApiKey = _apiKey,
                BinNumber = "454360"
            };

            var apiUrl = _apiUrl + "/merchants/installments";

            var response = _installmentProcessor.GetInstallments(request, apiUrl, _secretKey);

            return View(new InstallmentResultModel
            {
                Success = response.Success,
                MessageCode = response.MessageCode,
                Message = response.Message,
                UserMessage = response.UserMessage,
                InstallmentDtos = response.InstallmentDtos
            });
        }
    }
}