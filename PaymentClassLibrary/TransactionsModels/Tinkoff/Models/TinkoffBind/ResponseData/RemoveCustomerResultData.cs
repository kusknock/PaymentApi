namespace PaymentClassLibrary.TransactionsModels.Tinkoff
{
    /// <summary>
    /// Результат удаления покупателя из системы банка
    /// </summary>
    public class RemoveCustomerResultData : TinkoffResultData
    {
        /// <summary>
        /// Идентификатор покупателя в системе Продавца
        /// </summary>
        public string CustomerKey { get; set; }
    }
}