namespace PaymentClassLibrary.TransactionsModels.Tinkoff
{
    /// <summary>
    /// Фабрика транзакций Тинькофф
    /// </summary>
    public class TinkoffTransactionFactory
    {
        /// <summary>
        /// Метод для получение объекта транзакции по версии
        /// </summary>
        /// <param name="Version">Версия</param>
        /// <returns>Объект транзакции</returns>
        public static IPaymentTransaction GetTinkoffTransactionByVersion(int Version)
        {
            switch (Version)
            {
                case 0:
                    return new TinkoffBankTransaction();
                case 2:
                    return new TinkoffBankV2Transaction();
                default:
                    return null;
            }
        }
    }
}
