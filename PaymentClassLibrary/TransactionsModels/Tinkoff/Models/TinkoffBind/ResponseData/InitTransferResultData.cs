namespace PaymentClassLibrary.TransactionsModels.Tinkoff
{
    /// <summary>
    /// Результат запроса инициализации платежа
    /// </summary>
    public class InitTransferResultData : TinkoffResultData
    {
        /// <summary>
        /// Статус транзакции
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Уникальный идентификатор транзакции в системе Банка 
        /// </summary>
        public string PaymentId { get; set; }

        /// <summary>
        /// Идентификатор заявки
        /// </summary>
        public string OrderId { get; set; }

        /// <summary>
        /// Сумма в копейках
        /// </summary>
        public string Amount { get; set; }
    }
}