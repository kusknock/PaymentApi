using PaymentClassLibrary.Utils;
using System.Collections.Specialized;

namespace PaymentClassLibrary.Transport.Requests
{
    /// <summary>
    /// Интерфейс запроса
    /// </summary>
    public interface IRequest
    {
        /// <summary>
        /// Инициализация подписи в запросе
        /// </summary>
        /// <param name="signature">Модель подписи</param>
        void SetSignature(IModel signature);
        /// <summary>
        /// Инициализация адреса отправки запроса
        /// </summary>
        /// <param name="apiUrl">Адрес отправки запроса</param>
        /// <returns>Объект с установленным адресом</returns>
        IRequest SetApiUrl(string apiUrl);
        /// <summary>
        /// Получение API-url
        /// </summary>
        string GetApiUrl();
        /// <summary>
        /// Получение данных запроса в виде строки
        /// </summary>
        string GetRequestDataBody();
        /// <summary>
        /// Получение заголовка запроса
        /// </summary>
        NameValueCollection GetRequestHeader();
        /// <summary>
        /// Тип контента для запроса
        /// </summary>
        string GetContentType();
    }
}