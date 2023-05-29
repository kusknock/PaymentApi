using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PaymentApi.Configuration;
using PaymentApi.Utils;
using PaymentClassLibrary.ApiUrlFactory;
using PaymentClassLibrary.Queries;
using PaymentClassLibrary.Signature.SignatureCreators;
using PaymentClassLibrary.Signature.SignatureMethods;
using PaymentClassLibrary.SignatureCollectors;
using PaymentClassLibrary.TransactionsModels.Tinkoff;
using PaymentClassLibrary.Transport;
using PaymentClassLibrary.Transport.Responses;
using PaymentClassLibrary.Utils;

namespace PaymentApi.Controllers
{
    /// <summary>
    /// Контроллер EACQ-протокола Тинькофф (оплаты и статусы транзакций)
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class TinkoffPayController : ControllerBase
    {
        private readonly TinkoffPayQuery tinkoffPayQuery;
        private readonly GatewayClient client;
        private readonly TinkoffSettings settings;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="client">Клиент для отправки запросов</param>
        /// <param name="settings">Параметры из конфигурационного файла</param>
        public TinkoffPayController(GatewayClient client, IOptions<TinkoffSettings> settings)
        {
            this.settings = settings.Value;
            this.client = client;

            tinkoffPayQuery = new TinkoffPayQuery(
                new TinkoffPaySignatureCreator(
                new Sha256Hexadecimal(new SignatureCollectorWithSort())));
        }

        #region Utils
        /// <summary>
        /// Метод-помощник для отправки запроса, чтобы не дублировать код
        /// </summary>
        /// <param name="data">Модель данных</param>
        /// <param name="ApiMethod">Имя метода</param>
        /// <returns></returns>
        private TinkoffResponse SendRequest(IModel data, string ApiMethod)
        {
            var apiUrl = BankApiUrl.Factory.GetTinkoffUrl(GatewayUrl: settings.TinkoffGatewayUrl,
                                                                      ApiMethod: ApiMethod,
                                                                      IsE2C: false,
                                                                      Version: settings.Version).ApiUrl;

            var transaction = TinkoffTransactionFactory
                .GetTinkoffTransactionByVersion(settings.Version)
                .SetData(data)
                .SetApiUrl(apiUrl);

            var preparedRequestData = tinkoffPayQuery.CreateRequest(transaction);

            var response = client.MakeRequest<TinkoffResponse>(preparedRequestData);

            return response;
        }
        #endregion

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

            var response = SendRequest(data, TinkoffPayMethods.InitPayment);

            return response.GetResult<InitPaymentResultData>();
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

            var response = SendRequest(data, TinkoffPayMethods.InitRebill);

            return response.GetResult<InitRebillResultData>();
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

            var response = SendRequest(data, TinkoffPayMethods.ChargeRebill);

            return response.GetResult<ChargeRebillResultData>();
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

            var response = SendRequest(data, TinkoffPayMethods.GetState);

            return response.GetResult<GetStateResultData>(dictAsArray: true);
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

            var response = SendRequest(data, TinkoffPayMethods.GetAddCardState);

            return response.GetResult<GetAddCardStateResultData>();
        }
    }
}
