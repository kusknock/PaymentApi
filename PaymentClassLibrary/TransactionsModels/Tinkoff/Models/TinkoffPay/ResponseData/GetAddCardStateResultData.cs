namespace PaymentClassLibrary.TransactionsModels.Tinkoff
{
    /// <summary>
    /// Результат состояния привязки карты
    /// </summary>
    public class GetAddCardStateResultData : TinkoffResultData
    {
        /// <summary>
        /// Ключ запроса на привязку карты
        /// </summary>
        public string RequestKey { get; set; }

        /// <summary>
        /// Статус привязки
        /// </summary>
        public string Status { get; set; }
    }
}