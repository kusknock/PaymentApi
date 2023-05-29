using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PaymentApi.Configuration;
using PaymentApi.Utils;
using PaymentClassLibrary.ApiUrlFactory;
using PaymentClassLibrary.Queries;
using PaymentClassLibrary.Signature.SignatureCreators;
using PaymentClassLibrary.SignatureCollectors;
using PaymentClassLibrary.SignatureMethods;
using PaymentClassLibrary.TransactionsModels.Tinkoff;
using PaymentClassLibrary.Transport;
using PaymentClassLibrary.Transport.Responses;
using PaymentClassLibrary.Utils;
using System.Collections.Generic;

namespace PaymentApi.Controllers
{
    /// <summary>
    /// Контроллер для работы с Тинькофф E2C (привязки и выплаты)
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class TinkoffBindController : ControllerBase
    {
        private readonly TinkoffBindQuery tinkoffBindQuery;
        private readonly GatewayClient client;
        private readonly TinkoffSettings settings;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="client">Клиент для отправки запросов</param>
        /// <param name="settings">Параметры из конфигурационного файла</param>
        public TinkoffBindController(GatewayClient client, IOptions<TinkoffSettings> settings)
        {
            this.settings = settings.Value;
            this.client = client;

            tinkoffBindQuery = new TinkoffBindQuery(
                new TinkoffBindSignatureCreator(
                new RsaSha256Pfx(this.settings.PfxPath,
                this.settings.PfxPassword,
                new SignatureCollectorWithSort())));
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
                                                          IsE2C: true,
                                                          Version: settings.Version).ApiUrl;

            var transaction = TinkoffTransactionFactory
                .GetTinkoffTransactionByVersion(settings.Version)
                .SetData(data)
                .SetApiUrl(apiUrl);

            var preparedRequestData = tinkoffBindQuery.CreateRequest(transaction);

            var response = client.MakeRequest<TinkoffResponse>(preparedRequestData);

            return response;
        }
        #endregion

        /// <summary>
        /// Добавление покупателя в систему банка
        /// </summary>
        /// <remarks>Добавление покупателя в систему банка</remarks>
        /// <param name="data">Данные покупателя</param>
        /// <response code="200">Покупатель добавлен в систему банка</response>
        /// <response code="400">Входные данные неверны</response>
        [HttpPost]
        [Route("AddCustomer")]
        public IActionResult AddCustomer(AddCustomerData data)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = SendRequest(data, TinkoffBindMethods.AddCustomer);

            return response.GetResult<AddCustomerResultData>();
        }

        /// <summary>
        /// Получение данных покупателя из системы банка
        /// </summary>
        /// <param name="data">Данные покупателя</param>
        /// <response code="200">Получены данные покупателя от банка</response>
        /// <response code="400">Входные данные неверны</response>
        [HttpPost]
        [Route("GetCustomer")]
        public IActionResult GetCustomer(CustomerData data)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = SendRequest(data, TinkoffBindMethods.GetCustomer);

            return response.GetResult<GetCustomerResultData>();
        }

        /// <summary>
        /// Удаление покупателя
        /// </summary>
        /// <param name="data">Данные покупателя</param>
        /// <response code="200">Покупатель удален из системы банка</response>
        /// <response code="400">Входные данные неверны</response>
        [HttpPost]
        [Route("RemoveCustomer")]
        public IActionResult RemoveCustomer(CustomerData data)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = SendRequest(data, TinkoffBindMethods.RemoveCustomer);

            return response.GetResult<RemoveCustomerResultData>();
        }

        /// <summary>
        /// Добавление карты для покупателя в систему банка
        /// </summary>
        /// <param name="data">Данные карты</param>
        /// <response code="200">Возвращен объект с Success = true и ссылкой на привязку карты</response>
        /// <response code="400">Входные данные неверны</response>
        [HttpPost]
        [Route("AddCard")]
        public IActionResult AddCard(AddCardData data)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = SendRequest(data, TinkoffBindMethods.AddCard);

            return response.GetResult<AddCardResultData>();
        }

        /// <summary>
        /// Получение списка карт по идентификатору покупателя
        /// </summary>
        /// <param name="data">Данные покупателя</param>
        /// <response code="200">Возвращен объект с Success = true и привязанные карты</response>
        /// <response code="400">Входные данные неверны</response>
        [HttpPost]
        [Route("GetCardList")]
        public IActionResult GetCardList(CustomerData data)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = SendRequest(data, TinkoffBindMethods.GetCardList);

            try
            {
                var cardsResult = JsonConvert.DeserializeObject<List<TinkoffCustomerCard>>(response.ResponseString);

                var resultResponse = new GetCardListResultData
                {
                    Success = true,
                    ErrorCode = "0",
                    CustomerKey = data.CustomerKey,
                    TerminalKey = data.TerminalKey,
                    Cards = cardsResult
                };

                return Ok(resultResponse);
            }
            catch
            {
                return response.GetResult<GetCardListResultData>();
            }
        }

        /// <summary>
        /// Удаление карты по идентификатору покупателя
        /// </summary>
        /// <param name="data">Данные для удаления карты</param>
        /// <response code="200">Возвращен объект с данными карты, которые удалены</response>
        /// <response code="400">Входные данные неверны</response>
        [HttpPost]
        [Route("RemoveCard")]
        public IActionResult RemoveCard(RemoveCardData data)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = SendRequest(data, TinkoffBindMethods.RemoveCard);

            return response.GetResult<RemoveCardResultData>();
        }

        /// <summary>
        /// Инициализация выплаты
        /// </summary>
        /// <param name="data">Данные для инициализации платежа</param>
        /// <response code="200">Возвращен объект с данными об инициализации платежа</response>
        /// <response code="400">Входные данные неверны</response>
        [HttpPost]
        [Route("InitTransfer")]
        public IActionResult InitTransfer(InitTransferData data)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = SendRequest(data, TinkoffBindMethods.InitTransfer);

            return response.GetResult<InitTransferResultData>();
        }

        /// <summary>
        /// Непосредственная выплата
        /// </summary>
        /// <param name="data"></param>
        /// <response code="200">Возвращен объект с данными платежа и Success = true</response>
        /// <response code="400">Входные данные неверны</response>
        [HttpPost]
        [Route("PayTransfer")]
        public IActionResult PayTransfer(PayTransferData data)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = SendRequest(data, TinkoffBindMethods.PayTransfer);

            return response.GetResult<PayTransferResultData>();
        }
    }
}
