using PaymentClassLibrary.Signature;
using PaymentClassLibrary.Utils;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PaymentClassLibrary.TransactionsModels.AlfaBank
{
    /// <summary>
    /// Данные статуса транзакции
    /// </summary>
    public class StatusData : IModel
    {
        /// <summary>
        /// Логин продавца (colibridengi-sbox)
        /// </summary>
        [Required]
        [Signature(1)]
        [Display(Name = "login")]
        public string Login { get; set; }

        /// <summary>
        /// Идентификатор заявки в сиcтеме продавца
        /// </summary>
        [Required]
        [Signature(2)]
        [Display(Name = "client_orderid")]
        public string MerchantOrderId { get; set; }

        /// <summary>
        /// Идентификатор заявки в сиcтеме банка
        /// </summary>
        [Required]
        [Signature(3)]
        [Display(Name = "orderid")]
        public string OrderId { get; set; }

        /// <summary>
        /// Merchant control key (1353DE81-43A1-437D-A4BA-191909E1EDBE)
        /// </summary>
        [Required]
        [Signature(4)]
        [OnlySignatureField]
        [Display(Name = "control_key")]
        public string ControlKey { get; set; }
    }
}