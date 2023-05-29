namespace PaymentClassLibrary.TransactionsModels.Tinkoff
{
    /// <summary>
    /// Результат добавления покупателя в систему банка
    /// </summary>
    public class AddCustomerResultData : TinkoffResultData
    {
        /// <summary>
        /// Идентификатор покупателя в системе Продавца
        /// </summary>
        public string CustomerKey { get; set; }
    }
}