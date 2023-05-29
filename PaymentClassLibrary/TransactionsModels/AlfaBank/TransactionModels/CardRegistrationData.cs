using PaymentClassLibrary.Signature;
using PaymentClassLibrary.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentClassLibrary.TransactionsModels.AlfaBank
{
    /// <summary>
    /// Модель для транзакции CardRegistration (создание CardRefId после Preauth)
    /// </summary>
    public class CardRegistrationData : IModel
    {
        /// <summary>
        /// Логин для авторизации в платежном сервисе SBC
        /// </summary>
        [Required]
        [Signature(1)]
        [Display(Name = "login")]
        public string Login { get; set; }
        /// <summary>
        /// Идентификатор заявки в системе продавца
        /// </summary>
        [Required]
        [Signature(2)]
        [Display(Name = "client_orderid")]
        public string ClientOrderId { get; set; }
        /// <summary>
        /// Идентификатор операции по привязке карты
        /// </summary>
        [Required]
        [Signature(3)]
        [Display(Name = "orderid")]
        public string CardRegOrderId { get; set; }
        /// <summary>
        /// Merchant Control Key (выдан SBC)
        /// </summary>
        [Required]
        [Signature(4)]
        [OnlySignatureField]
        [Display(Name = "control_key")]
        public string ControlKey { get; set; }
    }
}
