using System.Collections.Generic;

namespace PaymentClassLibrary.TransactionsModels.Tinkoff
{
    /// <summary>
    /// Результат запроса получения списка карт
    /// </summary>
    public class GetCardListResultData : TinkoffResultData
    {
        /// <summary>
        /// Идентификатор покупателя в системе Продавца
        /// </summary>
        public string CustomerKey { get; set; }

        /// <summary>
        /// Список карт клиента
        /// </summary>
        public List<TinkoffCustomerCard> Cards { get; set; }

    }
}