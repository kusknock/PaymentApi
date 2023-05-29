using PaymentClassLibrary.Transport.Requests;
using System.Collections.Generic;

namespace PaymentClassLibrary.OAuth
{
    /// <summary>
    /// Возможный интерфейс OAuth
    /// </summary>
    public interface IOAuth
    {
        /// <summary>
        /// Версия
        /// </summary>
        string Version { get; }

        /// <summary>
        /// Формирование данных для OAuthRequest
        /// </summary>
        /// <param name="requestFields"></param>
        /// <returns></returns>
        IDictionary<string,string> GetOAuthData(IDictionary<string, string> requestFields);

        /// <summary>
        /// Формирование заголовка OAuth c подписью
        /// </summary>
        /// <param name="signature">Подпись</param>
        /// <returns>Готовый заголовок в виде строки который подключается в Authorization
        /// <br/>Пример: <seealso cref="OAuthRequest.GetRequestHeader"/></returns>
        string GetOAuthHeader(string signature);
    }
}