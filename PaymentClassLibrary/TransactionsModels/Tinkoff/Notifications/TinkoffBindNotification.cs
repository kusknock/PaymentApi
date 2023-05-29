namespace PaymentClassLibrary.TransactionsModels.Tinkoff
{
    /// <summary>
    /// Данные нотификации по протоколу E2C
    /// </summary>
    public class TinkoffBindNotification
    {
        /// <summary>
        /// Терминал
        /// </summary>
        public string TerminalKey { get; set; }
        /// <summary>
        /// Покупатель
        /// </summary>
        public string CustomerKey { get; set; }
        /// <summary>
        /// Идентификатор привязки
        /// </summary>
        public string RequestKey { get; set; }
        /// <summary>
        /// Параметр успешности запроса
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// Статус транзакции
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// Идентификатор оплаты
        /// </summary>
        public string PaymentId { get; set; }
        /// <summary>
        /// Код ошибки
        /// </summary>
        public string ErrorCode { get; set; }
        /// <summary>
        /// Идентификатор карты
        /// </summary>
        public string CardId { get; set; }
        /// <summary>
        /// Первые и последние 4 цифры карты
        /// </summary>
        public string Pan { get; set; }
        /// <summary>
        /// Дата истечения срока действия
        /// </summary>
        public string ExpDate { get; set; }
        /// <summary>
        /// Тип нотификации
        /// </summary>
        public string NotificationType { get; set; }
    }
}