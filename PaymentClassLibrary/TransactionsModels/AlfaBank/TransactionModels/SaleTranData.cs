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
    /// Данные для транзакции оплаты через платежную форму
    /// </summary>
    public class SaleTranData : IModel
    {
        /// <summary>
        /// Идентификатор заявки в системе продавца
        /// </summary>
        [Required]
        [Signature(2)]
        [Display(Name = "client_orderid")]
        public string ClientOrderId { get; set; }
        /// <summary>
        /// Описание заявки
        /// </summary>
        [Required]
        [Display(Name = "order_desc")]
        public string OrderDescription { get; set; }
        /// <summary>
        /// Имя покупателя
        /// </summary>
        [Required]
        [Display(Name = "first_name")]
        public string FirstName { get; set; }
        /// <summary>
        /// Фамилия покупателя
        /// </summary>
        [Required]
        [Display(Name = "last_name")]
        public string LastName { get; set; }
        /// <summary>
        /// Страховой номер (американский варианты)
        /// </summary>
        [Display(Name = "ssn")]
        public string SSN { get; set; }
        /// <summary>
        /// Дата рождения покупателя
        /// </summary>
        [Display(Name = "birthday")]
        public string BirthDate { get; set; }
        /// <summary>
        /// Адрес проживания покупателя
        /// </summary>
        [Required]
        [Display(Name = "address1")]
        public string Address { get; set; }
        /// <summary>
        /// Город проживания покупателя
        /// </summary>
        [Required]
        [Display(Name = "city")]
        public string City { get; set; }
        /// <summary>
        /// Регион проживания покупателя
        /// </summary>
        [Display(Name = "state")]
        public string State { get; set; }
        /// <summary>
        /// Индекс покупателя
        /// </summary>
        [Required]
        [Display(Name = "zip_code")]
        public string ZipCode { get; set; }
        /// <summary>
        /// Страна
        /// </summary>
        [Required]
        [Display(Name = "country")]
        public string Country { get; set; }
        /// <summary>
        /// Номер телефона
        /// </summary>
        [Required]
        [Display(Name = "phone")]
        public string Phone { get; set; }
        /// <summary>
        /// Номер сотового телефона
        /// </summary>
        [Display(Name = "cell_phone")]
        public string CellPhone { get; set; }
        /// <summary>
        /// Электронная почта
        /// </summary>
        [Required]
        [Signature(4)]
        [Display(Name = "email")]
        public string Email { get; set; }
        /// <summary>
        /// Сумма оплаты
        /// </summary>
        [Required]
        [Display(Name = "amount")]
        public string Amount { get; set; }
        /// <summary>
        /// Сумма в копейках
        /// </summary>
        [Signature(3)]
        [Display(Name = "amount_in_peny")]
        public string AmountInPeny { get { return (int.Parse(Amount) * 100).ToString(); } }
        /// <summary>
        /// Валюта
        /// </summary>
        [Required]
        [Display(Name = "currency")]
        public string Currency { get; set; }
        /// <summary>
        /// IP-адрес
        /// </summary>
        [Display(Name = "ipaddress")]
        public string IpAddress { get; set; }
        /// <summary>
        /// Дополнительные данные продавца
        /// </summary>
        [Display(Name = "merchant_data")]
        public string MerchantData { get; set; }
        /// <summary>
        /// Двухбуквенный код языка клиента для многоязычных платежных форм	
        /// </summary>
        [Display(Name = "preferred_language")]
        public string PreferredLanguage { get; set; }
        /// <summary>
        /// Адрес отправки нотификации
        /// </summary>
        [Display(Name = "server_callback_url")]
        public string ServerCallbackUrl { get; set; }
        /// <summary>
        /// Адрес перенаправления (успех)
        /// </summary>
        [Required]
        [Display(Name = "redirect_success_url")]
        public string RedirectUrlSuccess { get; set; }
        /// <summary>
        /// Адрес перенаправления (неуспех)
        /// </summary>
        [Required]
        [Display(Name = "redirect_fail_url")]
        public string RedirectUrlFail { get; set; }

        /// <summary>
        /// Идентификатор терминала
        /// </summary>
        [Required]
        [Signature(1)]
        [OnlySignatureField]
        [Display(Name = "end-point-id")]
        public string EndPointId { get; set; }

        /// <summary>
        /// Секретный ключ выданный SBC
        /// </summary>
        [Required]
        [Signature(5)]
        [OnlySignatureField]
        [Display(Name = "merchant_control_key")]
        public string ControlKey { get; set; }
    }
}
