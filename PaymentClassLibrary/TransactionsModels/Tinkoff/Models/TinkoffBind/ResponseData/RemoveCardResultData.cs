namespace PaymentClassLibrary.TransactionsModels.Tinkoff
{
    /// <summary>
    /// Результат удаления карты из системы банка
    /// </summary>
    public class RemoveCardResultData : TinkoffResultData
    {
        /// <summary>
        /// Идентификатор покупателя в системе Продавца
        /// </summary>
        public string CustomerKey { get; set; }

        /// <summary>
        /// Статус карты: A – активная, I – не активная, E – срок действия карты истек, D - удаленная
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Тип карты: 0 - карта списания; 1 - карта пополнения; 2 - карта пополнения и списания
        /// </summary>
        public int CardType { get; set; }

        /// <summary>
        /// Идентификатор карты в системе Банка
        /// </summary>
        public string CardId { get; set; }
    }
}