using PaymentClassLibrary.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PaymentClassLibrary.Signature.SignatureAttributes;

namespace PaymentClassLibrary.TransactionsModels.Tinkoff
{
    /// <summary>
    /// Получение статуса платежа
    /// </summary>
    public class GetStatePaymentData : IModel
    {
        /// <summary>
        /// Идентификатор платежа
        /// </summary>
        public string PaymentId { get; set; }
        /// <summary>
        /// Идентификатор терминала
        /// </summary>
        public string TerminalKey { get; set; }
        /// <summary>
        /// Пароль для доступа к терминалу
        /// </summary>
        [JsonIgnoreForSignature]
        public string Password { get; set; }
    }
}
