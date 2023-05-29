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
    /// Данные транзакции реккурентного списания
    /// </summary>
    public class ReccurentTranData : IModel
    {
        /// <summary>
        /// Логин выданный SBC
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
        /// Карта с которой производится списание
        /// </summary>
        [Required]
        [Signature(3)]
        [Display(Name = "cardrefid")]
        public string CardRefId { get; set; }
        /// <summary>
        /// Описание заявки
        /// </summary>
        [Required]
        [Display(Name = "order_desc")]
        public string OrderDescription { get; set; }
        /// <summary>
        /// Сумма списания
        /// </summary>
        [Required]
        [Display(Name = "amount")]
        public string Amount { get; set; }
        /// <summary>
        /// Сумма списания в копейках
        /// </summary>
        [Signature(4)]
        [Display(Name = "amount_in_peny")]
        public string AmountInPeny { get { return (int.Parse(Amount) * 100).ToString(); } }
        /// <summary>
        /// Код валюты
        /// </summary>
        [Required]
        [Signature(5)]
        [Display(Name = "currency")]
        public string Currency { get; set; }
        /// <summary>
        /// IP-адрес покупателя
        /// </summary>
        [Required]
        [Display(Name = "ipaddress")]
        public string IpAddress { get; set; }
        /// <summary>
        /// Этот параметр может содержать несколько величин, разделенных символом ','
        /// Payneteasy будет циклически просматривать суммы в списке, пытаться произвести платеж на эту сумму, 
        /// пока из списка не останется больше сумм или не будет одобрен.	
        /// </summary>
        [Display(Name = "enumerate_amounts")]
        public string EnumAmount { get; set; }
        /// <summary>
        /// Адрес отправки нотификации
        /// </summary>
        [Display(Name = "server_callback_url")]
        public string ServerCallbackUrl { get; set; }
        /// <summary>
        /// Адрес перенаправления (успешный)
        /// </summary>
        [Display(Name = "redirect_fail_url")]
        public string RedirectUrlFail { get; set; }
        /// <summary>
        /// Адрес перенаправления (неуспешный)
        /// </summary>
        [Display(Name = "redirect_success_url")]
        public string RedirectUrlSuccess { get; set; }
        /// <summary>
        /// Тип транзакции ребиллинга. 
        /// Возможные значения: REGULAR или IRREGULAR. 
        /// Если этот параметр будет отправлен в запросе, 
        /// он будет иметь приоритет над тем же параметром, указанным на уровне шлюза. 
        /// Используется только для конкретных эквайеров.
        /// </summary>
        [Display(Name = "recurrent_scenario")]
        public string ReccurentScenario { get; set; }
        /// <summary>
        ///Инициатор транзакции ребиллинга. 
        ///Возможные значения: CARDHOLDER или MERCHANT. 
        ///Если этот параметр будет отправлен в запросе, 
        ///он будет иметь приоритет над тем же параметром, 
        ///указанным на уровне шлюза. 
        ///Используется только для конкретных эквайеров.	
        /// </summary>
        [Display(Name = "recurrent_initiator")]
        public string ReccurentIniciator { get; set; }
        /// <summary>
        /// Значение ключа
        /// </summary>
        [Required]
        [Signature(6)]
        [OnlySignatureField]
        [Display(Name = "merchant_control_key")]
        public string ControlKey { get; set; }
    }
}
