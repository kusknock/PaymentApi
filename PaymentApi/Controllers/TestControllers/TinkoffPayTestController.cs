using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using PaymentApi.Configuration;
using PaymentApi.Utils;
using PaymentClassLibrary.TransactionsModels.Tinkoff;
using PaymentClassLibrary.Transport;
using System.IO;

namespace PaymentApi.Controllers
{
    /// <summary>
    /// Контролер для отладки и тестов EACQ-протокола Тинькофф (оплаты и статусы транзакций)
    /// </summary>
    [LocalOnly]
    [ApiController]
    [Route("[controller]")]
    public class TinkoffPayTestController : ControllerBase
    {
        private readonly TinkoffTestSettings tinkoffTestSettings;
        private readonly TinkoffPayController tinkoffPay;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="client">Клиент для отправки запросов</param>
        public TinkoffPayTestController(GatewayClient client)
        {
            tinkoffTestSettings = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, false)
                .AddEnvironmentVariables()
                .Build()
                .GetSection("TinkoffTestSettings")
                .Get<TinkoffTestSettings>();

            tinkoffPay = new TinkoffPayController(client,
                Options.Create(tinkoffTestSettings));
        }

        /// <summary>
        /// Инициализация платежа (платежная форма, клиент вводит данные карты сам)
        /// Тут важно получить PaymentId
        /// </summary>
        /// <param name="data">Данные платежа</param>
        /// <response code="200">Возвращен объект с данными об инициализации платежа</response>
        /// <response code="400">Входные данные неверны</response>
        [HttpPost]
        [Route("InitPayment")]
        public IActionResult InitPayment(InitPaymentData data)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return tinkoffPay.InitPayment(data);
        }

        /// <summary>
        /// Инициализация платежа на другом терминале (по RebillId карты)
        /// Тут важно получить PaymentId
        /// </summary>
        /// <param name="data">Данные платежа</param>
        /// <response code="200">Возвращен объект с данными об инициализации платежа</response>
        /// <response code="400">Входные данные неверны</response>
        [HttpPost]
        [Route("InitRebill")]
        public IActionResult InitRebill(InitRebillData data)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return tinkoffPay.InitRebill(data);
        }


        /// <summary>
        /// Непосредственное списание по полученным PaymentId (из InitRebill) и RebillId (GetCardList)
        /// </summary>
        /// <param name="data">Данные инициализированного платежа</param>
        /// <response code="200">Возвращен объект с данными о платеже</response>
        /// <response code="400">Входные данные неверны</response>
        [HttpPost]
        [Route("ChargeRebill")]
        public IActionResult ChargeRebill(ChargeRebillData data)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return tinkoffPay.ChargeRebill(data);
        }


        /// <summary>
        /// Получение статуса платежа по PaymentId
        /// </summary>
        /// <param name="data">Данные платежа</param>
        /// <response code="200">Возвращен объект с данными о платеже</response>
        /// <response code="400">Входные данные неверны</response>
        [HttpPost]
        [Route("GetState")]
        public IActionResult GetState(GetStatePaymentData data)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return tinkoffPay.GetState(data);
        }


        /// <summary>
        /// Проверка статуса привязки карты по RequestKey
        /// </summary>
        /// <param name="data">Данные карты</param>
        /// <response code="200">Возвращен объект с данными о привязке карты</response>
        /// <response code="400">Входные данные неверны</response>
        [HttpPost]
        [Route("GetAddCardState")]
        public IActionResult GetAddCardState(GetAddStateCardData data)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return tinkoffPay.GetAddCardState(data);
        }
    }
}
