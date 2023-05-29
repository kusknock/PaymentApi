using PaymentApi.Controllers;

namespace PaymentApi.Configuration
{
    /// <summary>
    /// Тинькофф настройки для тестирования алгоритмов
    /// <br/>Пример: <seealso cref="TinkoffBindTestController.tinkoffTestSettings"/>
    /// </summary>
    public class TinkoffTestSettings : TinkoffSettings
    {
        /// <summary>
        /// Код E2C терминала Тинькофф
        /// </summary>
        public string TinkoffE2CTerminalKey { get; set; }
        /// <summary>
        /// Код Rebill терминала
        /// </summary>
        public string TinkoffRebillTerminalKey { get; set; }
        /// <summary>
        /// Код терминала для оплаты через форму на сайте Тинькофф-банка
        /// </summary>
        public string TinkoffTerminalKey { get; set; }
        /// <summary>
        /// Пароль от Rebill-терминала
        /// </summary>
        public string TinkoffRebillTerminalPassword { get; set; }
        /// <summary>
        /// Пароль от терминала для оплаты через форму
        /// </summary>
        public string TinkoffTerminalPassword { get; set; }
    }
}