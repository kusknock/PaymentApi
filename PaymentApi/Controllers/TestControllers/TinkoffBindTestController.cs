using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using PaymentApi.Configuration;
using PaymentApi.Utils;
using PaymentClassLibrary.ApiUrlFactory;
using PaymentClassLibrary.Queries;
using PaymentClassLibrary.TransactionsModels.Tinkoff;
using PaymentClassLibrary.Transport;
using PaymentClassLibrary.Transport.Responses;
using PaymentClassLibrary.Utils;
using System.Collections.Generic;
using System.IO;

namespace PaymentApi.Controllers
{

    /// <summary>
    /// Тестовый контроллер для отладки алгоритмов E2C-протокола
    /// <br/>Настройки берутся из TinkoffTestSettings в appsettings.json
    /// </summary>
    [LocalOnly]
    [ApiController]
    [Route("[controller]")]
    public class TinkoffBindTestController : ControllerBase
    {
        private readonly TinkoffTestSettings tinkoffTestSettings;
        private readonly TinkoffBindController tinkoffBind;

        /// <summary>
        /// Конструктор контроллера
        /// </summary>
        /// <param name="client">Клиент для отправки запросов</param>
        public TinkoffBindTestController(GatewayClient client)
        {
            tinkoffTestSettings = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json", false, false)
                 .AddEnvironmentVariables()
                 .Build()
                 .GetSection("TinkoffTestSettings")
                 .Get<TinkoffTestSettings>();

            tinkoffBind = new TinkoffBindController(client,
                Options.Create(tinkoffTestSettings));
        }

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

            return tinkoffBind.AddCustomer(data);
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

            return tinkoffBind.GetCustomer(data);
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

            return tinkoffBind.RemoveCustomer(data);
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

            return tinkoffBind.AddCard(data);
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

            return tinkoffBind.GetCardList(data);
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

            return tinkoffBind.RemoveCard(data);
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

            return tinkoffBind.InitTransfer(data);
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

            return tinkoffBind.PayTransfer(data);
        }
    }
}
