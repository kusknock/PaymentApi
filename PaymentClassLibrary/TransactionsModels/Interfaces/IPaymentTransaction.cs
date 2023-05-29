using PaymentClassLibrary.Transport;
using PaymentClassLibrary.Transport.Requests;
using PaymentClassLibrary.Utils;
using System;
using System.Collections.Generic;

namespace PaymentClassLibrary.TransactionsModels
{
    /// <summary>
    /// Данные для совершения транзакций через платежные сервисы
    /// </summary>
    public interface IPaymentTransaction
    {
        /// <summary>
        /// Адрес для отправки запроса
        /// </summary>
        string ApiUrl { get; }
        /// <summary>
        /// Установка адреса для отправки запроса
        /// </summary>
        /// <param name="apiUrl"></param>
        /// <returns>Объект с установленным адресом</returns>
        IPaymentTransaction SetApiUrl(string apiUrl);
        /// <summary>
        /// Инициализация данных для запроса
        /// </summary>
        /// <param name="data">Данные для инициализации</param>
        /// <returns>Инициализированный данными объект транзакции</returns>
        IPaymentTransaction SetData(IModel data);

        /// <summary>
        /// Получение модели запроса для передачи в <see cref="GatewayClient"/>
        /// </summary>
        /// <returns></returns>
        IRequest GetRequest(); // TODO: передавать тип сериализатора

        /// <summary>
        /// Получение набора данных подписи запроса
        /// </summary>
        /// <returns></returns>
        IDictionary<string, string> GetSignatureData(); // TODO: добавить ITypeModel для унификации PaymentTransaction (Json, UrlEncoded)
    }
}