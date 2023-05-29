namespace PaymentClassLibrary.TransactionsModels.Tinkoff
{
    /// <summary>
    /// Данные карты покупателя
    /// </summary>
    public class TinkoffCustomerCard
    {
        /// <summary>
        /// Идентификатор карты в системе Банка
        /// </summary>
        public string CardId { get; set; }

        /// <summary>
        /// Номер карты 411111******1111
        /// </summary>
        public string Pan { get; set; }

        /// <summary>
        /// Статус карты: A – активная, I – не активная, E – срок действия карты истек, D - удаленная
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Идентификатор рекуррентного платежа
        /// </summary>
        public string RebillId { get; set; }

        /// <summary>
        /// Тип карты: 0 - карта списания; 1 - карта пополнения; 2 - карта пополнения и списания
        /// </summary>
        public int CardType { get; set; }

        /// <summary>
        /// Срок действия карты
        /// </summary>
        public string ExpDate { get; set; }
    }
}