using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaymentApi.Utils;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace PaymentApi.Controllers
{
    /// <summary>
    /// Контроллер для работы c нотификациями (callback) от АльфаБанка (по сути SBC)
    /// </summary>
    /// <remarks>
    /// В appsettings.json добавлены параметры для назначения CallbackUrl для каждой обработки нотификации
    /// <para>
    /// TODO: для контроллера нотификаций SBC
    /// <br/>1. Описать логику обработки нотификации
    /// <br/>2. Обработать параметры в appsettings.json, чтобы нотификации после транзакции приходили правильно
    /// </para>
    /// </remarks>
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AlfaBankCallbackController : ControllerBase
    {
        /// <summary>
        /// Обработка нотификации после привязки карты
        /// </summary>
        /// <param name="typeProcess">Этап процесса</param>
        /// <response code="200">Результат обработки запроса</response>
        /// <response code="400">Входные данные неверны</response>
        [HttpGet]
        [HttpPost]
        [Route("CardRegistrationCallback")]
        public IActionResult CardRegistrationCallback(string typeProcess)
        {
            if (typeProcess == "callback")
            {
                NameValueCollection form = HttpUtility.ParseQueryString(Request.QueryString.Value);

                // process callback

                return Ok(form);
            }
            else if (typeProcess == "success")
            {
                NameValueCollection form = Request.Form as NameValueCollection;

                // process callback

                // create-ref-id

                // return transaction

                return Ok(form);
            }
            else if (typeProcess == "fail")
            {
                NameValueCollection form = Request.Form as NameValueCollection;

                // process callback

                return Ok(form);
            }

            return BadRequest();
        }

        /// <summary>
        /// Обработка нотификации после оплаты клиентом через платежную форму
        /// </summary>
        /// <param name="typeProcess">Этап процесса</param>
        /// <response code="200">Результат обработки запроса</response>
        /// <response code="400">Входные данные неверны</response>
        [HttpGet]
        [HttpPost]
        [Route("SaleTransactionCallback")]
        public IActionResult SaleTransactionCallback(string typeProcess)
        {
            if (typeProcess == "callback")
            {
                NameValueCollection form = HttpUtility.ParseQueryString(Request.QueryString.Value);

                // process callback

                return Ok(form);
            }
            else if (typeProcess == "success")
            {
                NameValueCollection form = Request.Form as NameValueCollection;

                // process callback

                return Ok(form);
            }
            else if (typeProcess == "fail")
            {
                NameValueCollection form = Request.Form as NameValueCollection;

                // process callback

                return Ok(form);
            }

            return BadRequest();
        }

        /// <summary>
        /// Обработка нотификации после перевода денег
        /// </summary>
        /// <param name="typeProcess">Этап процесса</param>
        /// <response code="200">Результат обработки запроса</response>
        /// <response code="400">Входные данные неверны</response>
        [HttpGet]
        [HttpPost]
        [Route("TransferMoneyCallback")]
        public IActionResult TransferMoneyCallback(string typeProcess)
        {
            if (typeProcess == "callback")
            {
                NameValueCollection form = HttpUtility.ParseQueryString(Request.QueryString.Value);

                // process callback

                return Ok(form);
            }
            else if (typeProcess == "success")
            {
                NameValueCollection form = Request.Form as NameValueCollection;

                return Ok(form);
            }
            else if (typeProcess == "fail")
            {
                NameValueCollection form = Request.Form as NameValueCollection;

                // process callback

                return Ok(form);
            }

            return BadRequest();
        }

        /// <summary>
        /// Обработка нотификации после рекуррентного списания
        /// </summary>
        /// <param name="typeProcess">Этап процесса</param>
        /// <response code="200">Результат обработки запроса</response>
        /// <response code="400">Входные данные неверны</response>
        [HttpGet]
        [HttpPost]
        [Route("ReccurentTransactionCallback")]
        public IActionResult ReccurentTransactionCallback(string typeProcess)
        {
            if (typeProcess == "callback")
            {
                NameValueCollection form = HttpUtility.ParseQueryString(Request.QueryString.Value);

                // process callback

                return Ok(form);
            }
            else if (typeProcess == "success")
            {
                NameValueCollection form = Request.Form as NameValueCollection;

                // process callback

                return Ok(form);
            }
            else if (typeProcess == "fail")
            {
                NameValueCollection form = Request.Form as NameValueCollection;

                // process callback

                return Ok(form);
            }

            return BadRequest();
        }
    }
}
