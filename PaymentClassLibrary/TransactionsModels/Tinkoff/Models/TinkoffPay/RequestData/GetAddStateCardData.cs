using PaymentClassLibrary.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PaymentClassLibrary.Signature.SignatureAttributes;

namespace PaymentClassLibrary.TransactionsModels.Tinkoff
{
    /// <summary>
    /// Операция получения статуса привязки карты
    /// </summary>
    public class GetAddStateCardData : IModel
    {
        /// <summary>
        /// Ключ запроса на привязку карты
        /// </summary>
        [Required]
        public string RequestKey { get; set; }
        /// <summary>
        /// Идентификатор терминала
        /// </summary>
        [Required]
        public string TerminalKey { get; set; }
        /// <summary>
        /// Пароль терминала
        /// </summary>
        [JsonIgnoreForSignature]
        [Required]
        public string Password { get; set; }
    }
}
