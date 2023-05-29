using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentClassLibrary.TransactionsModels.Tinkoff
{
    /// <summary>
    /// Названия методов протокола E2C
    /// </summary>
    public static class TinkoffBindMethods
    {
        /// <summary>
        /// Инициализация перевода
        /// </summary>
        public static readonly string InitTransfer = "Init";
        /// <summary>
        /// Совершение перевода после инициализации
        /// </summary>
        public static readonly string PayTransfer = "Payment";
        /// <summary>
        /// Добавление покупателя в систему банка
        /// </summary>
        public static readonly string AddCustomer = "AddCustomer";
        /// <summary>
        /// Получение данных покупателя
        /// </summary>
        public static readonly string GetCustomer = "GetCustomer";
        /// <summary>
        /// Удаление покупателя из системы банка
        /// </summary>
        public static readonly string RemoveCustomer = "RemoveCustomer";
        /// <summary>
        /// Получение списка привязанных карт клиента
        /// </summary>
        public static readonly string GetCardList = "GetCardList";
        /// <summary>
        /// Инциализация привязки карты клиента
        /// </summary>
        public static readonly string AddCard = "AddCard";
        /// <summary>
        /// Удаление карты из системы банка
        /// </summary>
        public static readonly string RemoveCard = "RemoveCard";
    }

    /// <summary>
    /// Названия методов протокола EACQ
    /// </summary>
    public static class TinkoffPayMethods
    {
        /// <summary>
        /// Инициализация платежа через платежную форму
        /// </summary>
        public static readonly string InitPayment = "Init";
        /// <summary>
        /// Инициализация реккурента
        /// </summary>
        public static readonly string InitRebill = "Init";
        /// <summary>
        /// Исполнение реккурентного списания
        /// </summary>
        public static readonly string ChargeRebill = "Charge";
        /// <summary>
        /// Запрос статуса платежа
        /// </summary>
        public static readonly string GetState = "GetState";
        /// <summary>
        /// Запрос статуса привязки карты
        /// </summary>
        public static readonly string GetAddCardState = "GetAddCardState";
    }
}
