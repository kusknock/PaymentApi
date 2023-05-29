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
    /// Транзакция возврата средств (одно из применений возврат при холдировании средств на карте при привязке)
    /// </summary>
    public class RetrunTransactionData : IModel
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
        public string ClientOrderId { get; set; }

        /// <summary>
        /// Идентификатор заявки в сиcтеме банка
        /// </summary>
        [Required]
        [Signature(3)]
        [Display(Name = "orderid")]
        public string OrderId { get; set; }
        /// <summary>
        /// Сумма возврата
        /// </summary>

        [Display(Name = "amount")]
        public string Amount { get; set; }
        /// <summary>
        /// Код валюты
        /// </summary>
        [Display(Name = "currency")]
        public string Currency { get; set; }
        /// <summary>
        /// Комментарий
        /// </summary>
        [Required]
        [Display(Name = "comment")]
        public string Comment { get; set; }
        /// <summary>
        /// Значение ключа
        /// </summary>
        [Required]
        [Signature(4)]
        [OnlySignatureField]
        [Display(Name = "control_key")]
        public string ControlKey { get; set; }
    }
}
