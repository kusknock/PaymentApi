namespace PaymentClassLibrary.TransactionsModels.Tinkoff
{
    /// <summary>
    /// Результат запроса добавления карты
    /// </summary>
    public class AddCardResultData : TinkoffResultData
    {
       /// <summary>
        /// Идентификатор покупателя в системе Продавца
        /// </summary>
        public string CustomerKey { get; set; }

        /// <summary>
        /// Ссылка на страницу привязки карты. На данную страницу необходимо переадресовать клиента для привязки карты (для метода AddCard)
        /// </summary>
        public string PaymentUrl { get; set; }

        /// <summary>
        /// Идентификатор транзакции привязки карты (ключ запроса)
        /// </summary>
        public string RequestKey { get; set; }

    }
}