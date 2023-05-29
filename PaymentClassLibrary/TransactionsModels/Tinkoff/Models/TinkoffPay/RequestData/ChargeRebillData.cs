using PaymentClassLibrary.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PaymentClassLibrary.Signature.SignatureAttributes;

namespace PaymentClassLibrary.TransactionsModels.Tinkoff
{
    /// <summary>
    /// Операция списания по идентификатору платежа
    /// </summary>
    public class ChargeRebillData : IModel
    {
        /// <summary>
        /// Платежный идентификатор карты из GetCardList
        /// </summary>
        [Required]
        public string RebillId { get; set; }
        /// <summary>
        /// Идентификатор платежа
        /// </summary>
        [Required]
        public string PaymentId { get; set; }
        /// <summary>
        /// Идентификатор терминала
        /// </summary>
        [Required]
        public string TerminalKey { get; set; }
        /// <summary>
        /// Пароль для доступа к терминалу
        /// </summary>
        [JsonIgnoreForSignature]
        [Required]
        public string Password { get; set; }
    }
}
