namespace PaymentClassLibrary.TransactionsModels.Tinkoff
{
    /// <summary>
    /// Результат инициализации оплаты через платежную форму
    /// </summary>
    public class InitPaymentResultData : TinkoffResultData
    {
        /// <summary>
        /// Идентификатор заявки в системе Продавца
        /// </summary>
        public string OrderId { get; set; }

        /// <summary>
        /// Ссылка на страницу оплаты. На данную страницу необходимо переадресовать клиента для оплаты
        /// </summary>
        public string PaymentUrl { get; set; }

        /// <summary>
        /// Идентификатор платежа в системе банка
        /// </summary>
        public string PaymentId { get; set; }

        /// <summary>
        /// Статус платежа
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Сумма в копейках
        /// </summary>
        public string Amount { get; set; }
    }
}