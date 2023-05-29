using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PaymentApi.Configuration;
using PaymentApi.Utils;
using PaymentClassLibrary.CallbackValidators;
using PaymentClassLibrary.Signature.SignatureCreators;
using PaymentClassLibrary.Signature.SignatureMethods;
using PaymentClassLibrary.SignatureCollectors;
using PaymentClassLibrary.TransactionsModels.Tinkoff;
using PaymentClassLibrary.Utils.TypeModels.Tinkoff;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace PaymentApi.Controllers
{
    /// <summary>
    /// Контроллер обработки нотификаций от Тинькофф
    /// <br/> Еще в работе
    /// <br/> TODO: Нотификации Тинькофф
    /// <br/> Доделать обработку и перенаправление запроса на основное приложение + ответ банку
    /// </summary>
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class TinkoffBankCallbackController : ControllerBase
    {
        private readonly TinkoffSettings settings;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="settings">Параметры из кофигурационного файла</param>
        public TinkoffBankCallbackController(IOptions<TinkoffSettings> settings)
        {
            this.settings = settings.Value;
        }

        /// <summary>
        /// Метод для получения нотификаций по привязке карт
        /// </summary>
        /// <param name="notification">Модель нотификации</param>
        /// <response code="200">Результат обработки запроса</response>
        /// <response code="400">Входные данные неверны</response>
        [HttpGet]
        [HttpPost]
        [Route("BindNotification")]
        public IActionResult BindNotification(TinkoffBindNotification notification)
        {
            return Ok(notification);
        }

        /// <summary>
        /// Метод для получения нотифкаций по произведенным оплатам клиентов
        /// </summary>
        /// <param name="notification">Модель нотификации</param>
        /// <response code="200">Результат обработки запроса</response>
        /// <response code="400">Входные данные неверны</response>
        [HttpGet]
        [HttpPost]
        [Route("PayNotification")]
        public IActionResult PayNotification(TinkoffPayNotification notification)
        {
            // TODO: надо написать тесты
            notification.Password = "70wdpdd1a78fy82z";

            var validator = new TinkoffNotificationValidator(
                new TinkoffPaySignatureCreator(
                new Sha256Hexadecimal(new SignatureCollectorWithSort())));

            var isValid = validator.ValidateSignature(
                TinkoffModelFactory.GetTinkoffModelByVersion(settings.Version), 
                notification, 
                notification.GetTinkoffPaySignature());

            return Ok(isValid);
        }
    }
}
