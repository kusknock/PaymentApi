using PaymentClassLibrary.Transport.Requests;
using PaymentClassLibrary.Transport.Responses;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PaymentClassLibrary.Transport
{
    /// <summary>
    /// Клиент для отправки запрсоов
    /// </summary>
    public class GatewayClient
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public GatewayClient()
        {

        }

        /// <summary>
        /// Создание, валидация, отправка и обработка запроса
        /// </summary>
        /// <param name="request">Объект с данными запроса</param>
        /// <returns>Готовый Response</returns>
        public TResponse MakeRequest<TResponse>(IRequest request) where TResponse : class, IResponse
        {
            ValidateRequest(request);

            return SendRequest(request, Activator.CreateInstance<TResponse>());
        }


        /// <summary>
        /// Валидация модели запроса
        /// </summary>
        /// <param name="request">Модель запроса</param>
        private void ValidateRequest(IRequest request)
        {
            if (string.IsNullOrEmpty(request.GetApiUrl()))
                throw new ValidationException("api url is empty or null");

            if (string.IsNullOrEmpty(request.GetRequestDataBody()))
                throw new ValidationException("request data is empty or null");
        }

        /// <summary>
        /// Отправка запроса
        /// </summary>
        /// <param name="preparedRequestData">Подготовленные данные</param>
        /// <param name="response">Объект ответа</param>
        /// <returns>Инициализированный объект ответа</returns>
        private TResponse SendRequest<TResponse>(IRequest preparedRequestData, TResponse response) where TResponse : class, IResponse
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(preparedRequestData.GetApiUrl());
            HttpWebResponse webResponse;

            try
            {
                var requestData = Encoding.UTF8.GetBytes(preparedRequestData.GetRequestDataBody());

                request.Headers.Add(preparedRequestData.GetRequestHeader());

                request.Method = "POST";
                request.ContentType = preparedRequestData.GetContentType();
                request.ContentLength = requestData.Length;
                request.Timeout = 30 * 1000;
                request.UserAgent = "DotNet";

                using (var stream = request.GetRequestStream())
                {
                    stream.Write(requestData, 0, requestData.Length);
                }

                try
                {
                    webResponse = (HttpWebResponse)request.GetResponse();
                }
                catch (WebException ex)
                {
                    webResponse = ex.Response as HttpWebResponse;
                }

                return response.InitializeResponse(webResponse.StatusCode, 
                    new StreamReader(webResponse.GetResponseStream()).ReadToEnd()) as TResponse;
            }
            catch (Exception ex)
            {
                return response.InitializeResponse(HttpStatusCode.InternalServerError, ex.Message) as TResponse;
            }
        }
    }
}