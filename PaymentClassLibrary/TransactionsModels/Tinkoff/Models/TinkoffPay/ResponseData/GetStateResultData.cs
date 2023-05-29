using System.Collections.Generic;

namespace PaymentClassLibrary.TransactionsModels.Tinkoff
{
    /// <summary>
    /// Результат состояния платежа
    /// </summary>
    public class GetStateResultData : TinkoffResultData
    {
        /// <summary>
        /// Статус платежа
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Идентификатор платежа в системе банка
        /// </summary>
        public string PaymentId { get; set; }

        /// <summary>
        /// Идентификатор заявка в системе продавца
        /// </summary>
        public string OrderId { get; set; }

        /// <summary>
        /// Сумма платежа в копейках
        /// </summary>
        public string Amount { get; set; }

        /// <summary>
        /// Параметры платежа
        /// </summary>
        public Dictionary<string, string> Params { get; set; }
    }
}