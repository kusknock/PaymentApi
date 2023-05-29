namespace PaymentClassLibrary.TransactionsModels.Tinkoff
{
    /// <summary>
    /// Результат реккурентного списания
    /// </summary>
    public class ChargeRebillResultData : TinkoffResultData
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
    }
}