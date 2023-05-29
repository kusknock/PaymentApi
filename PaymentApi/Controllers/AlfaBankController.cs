using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PaymentApi.Configuration;
using PaymentApi.Utils;
using PaymentClassLibrary.ApiUrlFactory;
using PaymentClassLibrary.Normalizer;
using PaymentClassLibrary.OAuth;
using PaymentClassLibrary.Queries;
using PaymentClassLibrary.Signature.SignatureCreators;
using PaymentClassLibrary.SignatureCollectors;
using PaymentClassLibrary.SignatureMethods;
using PaymentClassLibrary.TransactionsModels.AlfaBank;
using PaymentClassLibrary.Transport;
using PaymentClassLibrary.Transport.Responses;
using PaymentClassLibrary.Utils;
using System.Collections.Specialized;
using System.Threading.Tasks;
using System.Web;

namespace PaymentApi.Controllers
{
    /// <summary>
    /// Контроллер для работы с алгоритмами SBC
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AlfaBankController : ControllerBase
    {
        private readonly AlfaBankQuery alfaBankQuery;
        private readonly GatewayClient client;
        private readonly AlfaBankSettings settings;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="client">Объект для отправки запросов</param>
        /// <param name="settings">Настройки из конфигурационного файла</param>
        public AlfaBankController(GatewayClient client, IOptions<AlfaBankSettings> settings)
        {
            this.settings = settings.Value;
            this.client = client;

            alfaBankQuery = new AlfaBankQuery(
                new AlfaBankSignatureCreator(
                new Sha1(
                new SignatureCollectorWithSort())));
        }

        /// <summary>
        /// Метод-помощник для отправки запроса, чтобы не дублировать код
        /// </summary>
        /// <param name="data">Модель данных</param>
        /// <param name="method">Объект метода (в нем название и номер терминала)</param>
        private AlfaBankResponse SendRequest(IModel data, AlfaBankMethod method)
        {
            var apiUrl = BankApiUrl.Factory.GetAlfaBankUrl(GatewayUrl: settings.AlfaGatewayUrl,
                                                           Version: 2,
                                                           ApiMethod: method.ApiMethod,
                                                           EndPointId: method.EndPointId).ApiUrl;

            var transction = new AlfaBankTransaction(data).SetApiUrl(apiUrl);

            var preparedRequestData = alfaBankQuery.CreateRequest(transction);

            var response = client.MakeRequest<AlfaBankResponse>(preparedRequestData);

            return response;
        }

        /// <summary>
        /// Предварительная аутентификация
        /// </summary>
        /// <param name="data"></param>
        /// <response code="200">Результат обработки запроса</response>
        /// <response code="400">Входные данные неверны</response>
        [HttpPost]
        [Route("Preauth")]
        public IActionResult Preauth(PreauthData data)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = SendRequest(data, AlfaBankMethod.Methods.Preauth);

            return Ok(response.ResponseString);
        }

        /// <summary>
        /// Создание идентификатора привязки карты (предварительно необходимо выполнить Preauth)
        /// </summary>
        /// <param name="data"></param>
        /// <response code="200">Результат обработки запроса</response>
        /// <response code="400">Входные данные неверны</response>
        [HttpPost]
        [Route("CreateCardRef")]
        public IActionResult CreateCardRef(CardRegistrationData data)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = SendRequest(data, AlfaBankMethod.Methods.CardRegistration);

            return Ok(response.ResponseString);
        }

        /// <summary>
        /// Транзакция для возврата холдирования (после CreateCardRef)
        /// </summary>
        /// <param name="data"></param>
        /// <response code="200">Результат обработки запроса</response>
        /// <response code="400">Входные данные неверны</response>
        [HttpPost]
        [Route("Return")]
        public IActionResult Return(RetrunTransactionData data)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = SendRequest(data, AlfaBankMethod.Methods.ReturnTransaction);

            return Ok(response.ResponseString);
        }

        /// <summary>
        /// Транзакция оплаты через платежную форму
        /// </summary>
        /// <param name="data"></param>
        /// <response code="200">Результат обработки запроса</response>
        /// <response code="400">Входные данные неверны</response>
        [HttpPost]
        [Route("Sale")]
        public IActionResult Sale(SaleTranData data)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = SendRequest(data, AlfaBankMethod.Methods.SaleForm);

            return Ok(response.ResponseString);
        }

        /// <summary>
        /// Транзакции рекуррентного списания
        /// </summary>
        /// <param name="data"></param>
        /// <response code="200">Результат обработки запроса</response>
        /// <response code="400">Входные данные неверны</response>
        [HttpPost]
        [Route("Reccurent")]
        public IActionResult Reccurent(ReccurentTranData data)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = SendRequest(data, AlfaBankMethod.Methods.ReccurentPayment);

            return Ok(response.ResponseString);
        }

        /// <summary>
        /// Транзакция статуса транзакции
        /// </summary>
        /// <param name="data"></param>
        /// <param name="apiMethod"></param>
        /// <response code="200">Результат обработки запроса</response>
        /// <response code="400">Входные данные неверны</response>
        [HttpPost]
        [Route("Status")]
        public IActionResult Status(StatusData data, string apiMethod)
        {
            AlfaBankMethod statusMethod = AlfaBankMethod.Methods.StatusTransaction(apiMethod);

            if (!ModelState.IsValid || statusMethod is null)
                return BadRequest(ModelState);

            var response = SendRequest(data, statusMethod);

            return Ok(response.ResponseString);
        }

        /// <summary>
        /// Перевод денег на карту клиенту
        /// </summary>
        /// <param name="data"></param>
        /// <response code="200">Результат обработки запроса</response>
        /// <response code="400">Входные данные неверны</response>
        [HttpPost]
        [Route("TransferMoney")]
        public IActionResult TransferMoney(TransferCardData data)
        {
            var apiUrl = BankApiUrl.Factory.GetAlfaBankUrl(GatewayUrl: settings.AlfaGatewayUrl,
                                               Version: 4,
                                               ApiMethod: AlfaBankMethod.Methods.TransferMoney.ApiMethod,
                                               EndPointId: AlfaBankMethod.Methods.TransferMoney.EndPointId).ApiUrl;

            string accessKey = System.IO.File.ReadAllText(settings.PathPrivateKey);

            var simpleNormalizer = new SimpleNormalizerParameters();

            var signatureCollector = new OAuthSignatureCollector(apiUrl, simpleNormalizer);

            var signatureMethod = new RsaSha256(accessKey, signatureCollector);

            var oAuth = new MiniOAuth1(settings.ConsumerKey, signatureMethod.Name).SetNonce(Extensions.RandomString(11))
                                                             .SetTimeStamp();

            var transferCardTransaction = new OAuthTransaction(data, oAuth, simpleNormalizer).SetApiUrl(apiUrl);

            var oauthQuery = new OAuthQuery(new OAuthSignatureCreator(signatureMethod));

            var preparedRequestData = oauthQuery.CreateRequest(transferCardTransaction);

            var response = client.MakeRequest<AlfaBankResponse>(preparedRequestData)
                                 .ResponseString;

            return Ok(response);
        }
    }
}
