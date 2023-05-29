using System.Net;

namespace PaymentClassLibrary.Transport.Responses
{
    /// <summary>
    /// Интерфейс ответа
    /// </summary>
    public interface IResponse
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
        /// Инициализация объекта из реализации HTTP-ответа клиента
        /// </summary>
        /// <param name="code">Код ответа сервера</param>
        /// <param name="message">Данные ответа</param>
        /// <returns></returns>
        public IResponse InitializeResponse(HttpStatusCode code, string message);
    }
}