using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PaymentClassLibrary.TransactionsModels.Tinkoff;
using PaymentClassLibrary.Utils;
using System;
using System.Net;

namespace PaymentClassLibrary.Transport.Responses
{
    /// <summary>
    /// Обработчик ответа от платежных сервисов (пока только для Json)
    /// </summary>
    public class TinkoffResponse : IResponse
    {
        /// <summary>
        /// Строка ответа
        /// </summary>
        public string ResponseString { get; set; }

        /// <summary>
        /// Код ответа от сервера
        /// </summary>
        public HttpStatusCode Code { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        public TinkoffResponse()
        {
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="responseString">строка ответа</param>
        /// <param name="code">Код ответа</param>
        public TinkoffResponse(string responseString, HttpStatusCode code)
        {
            ResponseString = responseString;
            Code = code;
        }

        /// <summary>
        /// Получение десериализованного объекта ответа
        /// </summary>
        /// <typeparam name="T">Тип объекта в случае успешно выполненного запроса</typeparam>
        /// <param name="dictAsArray">Флаг получения сериализованного объекта в Dictionary</param>
        /// <returns>Объект с ответными данными</returns>
        public IActionResult GetResult<T>(bool dictAsArray = false) where T : TinkoffResultData, new()
        {
            JsonSerializerSettings settings = null;

            if (dictAsArray)
            {
                settings.Formatting = Formatting.Indented;
                settings.ContractResolver = new DictionaryAsArrayResolver();
            }

            
            try
            {
                return new ObjectResult(JsonConvert.DeserializeObject<T>(ResponseString, settings))
                {
                    StatusCode = (int)Code,
                };

            }
            catch
            {
                return new ObjectResult(new T()
                {
                    ErrorCode = Code.ToString(),
                    Success = false,
                    Message = ResponseString
                })
                {
                    StatusCode = (int)Code,
                };
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="code"><inheritdoc/></param>
        /// <param name="message"><inheritdoc/></param>
        /// <returns><inheritdoc/></returns>
        public IResponse InitializeResponse(HttpStatusCode code, string message)
        {
            Code = code;
            ResponseString = message;

            return this;
        }
    }
}