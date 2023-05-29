using PaymentClassLibrary.Signature;
using PaymentClassLibrary.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PaymentClassLibrary.Signature.SignatureAttributes;

namespace PaymentClassLibrary.TransactionsModels.Tinkoff
{
    /// <summary>
    /// Инициализация реккурентного списания
    /// </summary>
    public class InitRebillData : IModel
    {
        /// <summary>
        /// Идентификатор заявки
        /// </summary>
        [Required]
        public string OrderId { get; set; }
        /// <summary>
        /// Идентификатор клиента
        /// </summary>
        [Required]
        public string CustomerKey { get; set; }
        /// <summary>
        /// Определяет тип проведения платежа –
        /// двухстадийная или одностадийная оплата.
        /// − "O" - одностадийная оплата;
        /// − "T"- двухстадийная оплата
        /// </summary>
        [Required]
        public string PayType { get; set; }
        /// <summary>
        /// Идентификатор терминала
        /// </summary>
        [Required]
        public string TerminalKey { get; set; }
        /// <summary>
        /// Пароль от терминала
        /// </summary>
        [JsonIgnoreForSignature]
        [Required]
        public string Password { get; set; }
        /// <summary>
        /// Сумма платежа
        /// </summary>
        [JsonIgnore]
        [OnlySignatureField]
        [Required]
        public string Amount { get; set; }
        /// <summary>
        /// Сумма в копейках
        /// </summary>
        [JsonProperty("Amount")]
        public string AmountInPeny
        {
            get
            {
                double amount;

                if (!double.TryParse(Amount, out amount))
                    throw new Exception("Неверная сумма");

                return ((int)(amount * 100)).ToString();
            }
        }
        /// <summary>
        /// Дополнительные данные
        /// </summary>
        [OnlySignatureField]
        public MfoAgreementData DATA { get; set; }
    }
}
