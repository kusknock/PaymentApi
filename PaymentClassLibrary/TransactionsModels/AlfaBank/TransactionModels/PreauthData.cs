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
    /// Данные для предварительной тразакции (используется при оплатах и привязках карт)
    /// </summary>
    public class PreauthData : IModel
    {
        /// <summary>
        /// Идентификатор заявки в системе продавца
        /// </summary>
        [Required]
        [Signature(2)]
        [Display(Name = "client_orderid")]
        public string ClientOrderId { get; set; }
        /// <summary>
        /// Краткое описание покупки
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
        /// Страховой номер (американский вариант)
        /// </summary>
        [Display(Name = "ssn")]
        public string SSN { get; set; }
        /// <summary>
        /// Дата рождения покупателя
        /// </summary>
        [Display(Name = "birthday")]
        public string BirthDate { get; set; }
        /// <summary>
        /// Адрес покупателя
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
        /// Регион покупателя
        /// </summary>
        [Display(Name = "state")]
        public string State { get; set; }
        /// <summary>
        /// Почтовый индекс (американский вариант)
        /// </summary>
        [Required]
        [Display(Name = "zip_code")]
        public string ZipCode { get; set; }
        /// <summary>
        /// Страна покупателя
        /// </summary>
        [Required]
        [Display(Name = "country")]
        public string Country { get; set; }
        /// <summary>
        /// Номер телефона покупателя
        /// </summary>
        [Required]
        [Display(Name = "phone")]
        public string Phone { get; set; }
        /// <summary>
        /// Сотовый телефон покупателя 
        /// </summary>
        [Display(Name = "cell_phone")]
        public string CellPhone { get; set; }
        /// <summary>
        /// Электронная почта покупателя
        /// </summary>
        [Required]
        [Signature(4)]
        [Display(Name = "email")]
        public string Email { get; set; }
        /// <summary>
        /// Сумма холдирования
        /// </summary>
        [Required]
        [Display(Name = "amount")]
        public string Amount { get; set; }
        /// <summary>
        /// Сумма холдирования в копейках
        /// </summary>
        [Signature(3)]
        [Display(Name = "amount_in_peny")]
        public string AmountInPeny
        {
            get
            {
                int amount;

                int.TryParse(Amount, out amount);

                return (amount * 100).ToString();
            }
        }
        /// <summary>
        /// Код валюты
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
        /// Дополнительные данные о продавце
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
        /// Адрес перенаправления при успешной транзакции
        /// </summary>
        [Required]
        [Display(Name = "redirect_success_url")]
        public string RedirectUrlSuccess { get; set; }
        /// <summary>
        /// Адрес перенаправления при неудачной транзакции
        /// </summary>
        [Required]
        [Display(Name = "redirect_fail_url")]
        public string RedirectUrlFail { get; set; }
        /// <summary>
        /// Ключ проверки продавца (выданный SBC)
        /// </summary>
        [Required]
        [Signature(5)]
        [OnlySignatureField]
        [Display(Name = "merchant_control_key")]
        public string ControlKey { get; set; }
        /// <summary>
        /// Идентификатор терминала
        /// </summary>
        [Required]
        [Signature(1)]
        [OnlySignatureField]
        [Display(Name = "end-point-id")]
        public string EndPointId { get; set; }
    }
}
