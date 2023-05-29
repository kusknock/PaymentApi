namespace PaymentClassLibrary.TransactionsModels.Tinkoff
{
    /// <summary>
    /// Результат запроса выплаты на карту
    /// </summary>
    public class PayTransferResultData : TinkoffResultData
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
    }
}