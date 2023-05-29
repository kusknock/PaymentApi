using PaymentClassLibrary.Signature;
using PaymentClassLibrary.Utils;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace PaymentClassLibrary.TransactionsModels.Tinkoff
{
    /// <summary>
    /// Инициализация выплаты
    /// </summary>
    public class InitTransferData : IModel
    {
        /// <summary>
        /// Терминал
        /// </summary>
        [Required]
        public string TerminalKey { get; set; }
        /// <summary>
        /// Идентификатор заявки
        /// </summary>
        [Required]
        public string OrderId { get; set; }
        /// <summary>
        /// Сумма выплаты
        /// </summary>
        [Required]
        [JsonIgnore]
        [OnlySignatureField]
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
        /// Идентификатор карты, для выплаты
        /// </summary>
        [Required]
        public string CardId { get; set; }
        /// <summary>
        /// Дополнительные данные
        /// </summary>
        [OnlySignatureField]
        public PaymentPurposeDetailsData DATA { get; set; }
    }
}
