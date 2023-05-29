namespace PaymentClassLibrary.TransactionsModels.Tinkoff
{
    /// <summary>
    /// Результат запроса данных клиента
    /// </summary>
    public class GetCustomerResultData : TinkoffResultData
    {
        /// <summary>
        /// Идентификатор покупателя в системе Продавца
        /// </summary>
        public string CustomerKey { get; set; }

        /// <summary>
        /// Электронная почта покупателя
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Номер телеофна покупателя
        /// </summary>
        public string Phone { get; set; }

    }
}