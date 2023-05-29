using PaymentClassLibrary.Utils;
using System.ComponentModel.DataAnnotations;

namespace PaymentClassLibrary.TransactionsModels.AlfaBank
{
    /// <summary>
    /// Транзакция выплаты на карту
    /// </summary>
    public class TransferCardData : IModel
    {
        /// <summary>
        /// Логин продавца (colibridengi-sbox)
        /// </summary>
        [Required]
        [Display(Name = "login")]
        public string Login { get; set; }

        /// <summary>
        /// Идентификатор заявки в сиcтеме продавца
        /// </summary>
        [Required]
        [Display(Name = "client_orderid")]
        public string MerchantOrderId { get; set; }

        /// <summary>
        /// Идентификатор карты в системе банка
        /// </summary>
        [Required]
        [Display(Name = "destination-card-ref-id")]
        public string CardRefId { get; set; }

        /// <summary>
        /// Сумма (в копейках)
        /// </summary>
        [Required]
        [Display(Name = "amount")]
        public string Amount { get; set; }

        /// <summary>
        /// Валюта
        /// </summary>
        [Required]
        [Display(Name = "currency")]
        public string Currency { get; set; }

        /// <summary>
        /// IP-адрес покупателя
        /// </summary>
        [Required]
        [Display(Name = "ipaddress")]
        public string IpAddress { get; set; }

        /// <summary>
        /// Описание заявки
        /// </summary>
        [Required]
        [Display(Name = "order_desc")]
        public string OrderDescription { get; set; }

        /// <summary>
        /// Дополнительные данные
        /// </summary>
        [Display(Name = "merchant_data")]
        public string MerchantData { get; set; }

        /// <summary>
        /// Merchant control key (1353DE81-43A1-437D-A4BA-191909E1EDBE)
        /// </summary>
        [Required]
        [Display(Name = "control_key")]
        public string ControlKey { get; set; }

        /// <summary>
        /// URL для перенаправления пользователя после успешного совершения действия
        /// </summary>
        [Required]
        [Display(Name = "redirect_success_url")]
        public string RedirectUrlSuccess { get; set; }

        /// <summary>
        /// URL для перенаправления пользователя после неуспешного совершения действия
        /// </summary>
        [Required]
        [Display(Name = "redirect_fail_url")]
        public string RedirectUrlFail { get; set; }

        /// <summary>
        /// URL для получения callback от банка
        /// </summary>
        [Display(Name = "server_callback_url")]
        public string ServerCallbackUrl { get; set; }

        /// <summary>
        /// Имя клиента
        /// </summary>
        [Display(Name = "first_name")]
        public string FirstName { get; set; }
        /// <summary>
        /// Среднее имя (вариант США)
        /// </summary>
        [Display(Name = "middle_name")]
        public string MiddleName { get; set; }
        /// <summary>
        /// Фамилия клиента
        /// </summary>
        [Display(Name = "last_name")]
        public string LastName { get; set; }
        /// <summary>
        /// Страховой номер (американский вариант)
        /// </summary>
        [Display(Name = "ssn")]
        public string SSN { get; set; }
        /// <summary>
        /// Дата рождения
        /// </summary>
        [Display(Name = "birthday")]
        public string BirthDate { get; set; }
        /// <summary>
        /// Адрес местожительства
        /// </summary>
        [Display(Name = "address1")]
        public string Address { get; set; }
        /// <summary>
        /// Город
        /// </summary>
        [Display(Name = "city")]
        public string City { get; set; }
        /// <summary>
        /// Регион
        /// </summary>
        [Display(Name = "state")]
        public string State { get; set; }
        /// <summary>
        /// Индекс
        /// </summary>
        [Display(Name = "zip_code")]
        public string ZipCode { get; set; }
        /// <summary>
        /// Страна
        /// </summary>
        [Display(Name = "country")]
        public string Country { get; set; }
        /// <summary>
        /// Номер телефона
        /// </summary>
        [Display(Name = "phone")]
        public string Phone { get; set; }
        /// <summary>
        /// Номер сотового
        /// </summary>
        [Display(Name = "cell_phone")]
        public string CellPhone { get; set; }
        /// <summary>
        /// Электронная почта
        /// </summary>
        [Display(Name = "email")]
        public string Email { get; set; }
        /// <summary>
        /// Назначение
        /// </summary>
        [Display(Name = "purpose")]
        public string Purpose { get; set; }
        /// <summary>
        /// Имя получателя
        /// </summary>
        [Display(Name = "receiver_first_name")]
        public string ReceiverFirstName { get; set; }
        /// <summary>
        /// Среднее имя получателя (вариант как в США)
        /// </summary>
        [Display(Name = "receiver_middle_name")]
        public string ReceiverMiddleName { get; set; }
        /// <summary>
        /// Фамилия получателя
        /// </summary>
        [Display(Name = "receiver_last_name")]
        public string ReceiverLastName { get; set; }
        /// <summary>
        /// Номер телефона
        /// </summary>
        [Display(Name = "receiver_phone")]
        public string ReceiverPhone { get; set; }
        /// <summary>
        /// Является ли получатель резидентом?	
        /// </summary>
        [Display(Name = "receiver_resident")]
        public string ReceiverResident { get; set; }
        /// <summary>
        /// Серия паспорта
        /// </summary>
        [Display(Name = "receiver_identity_document_series")]
        public string ReceiverDocSeries { get; set; }
        /// <summary>
        /// Номер паспорта
        /// </summary>
        [Display(Name = "receiver_identity_document_number")]
        public string ReceiverDocNumber { get; set; }
        /// <summary>
        /// 21 - для паспорта РФ
        /// </summary>
        [Display(Name = "receiver_identity_document_id")]
        public string ReceiverDocId { get; set; }
        /// <summary>
        /// Адрес получателя
        /// </summary>
        [Display(Name = "receiver_address1")]
        public string ReceiverAddress { get; set; }
        /// <summary>
        /// Город получателя
        /// </summary>
        [Display(Name = "receiver_city")]
        public string ReceiverCity { get; set; }


    }
}