using PaymentApi.Controllers;

namespace PaymentApi.Configuration
{
    /// <summary>
    /// Базовый класс настроек Тинькофф-банка
    /// </summary>
    public class TinkoffSettings
    {
        /// <summary>
        /// Путь до расположения pfx-контейнера ключа Тинькофф
        /// </summary>
        public string PfxPath
        {
            get
            {
                return pathConfig.Path;
            }
            set
            {
                pathConfig = PathConfigurator.Factory.GetPathCurrentDomain(value);
            }
        }

        /// <summary>
        /// Пароль от pfx-контейнера ключа
        /// </summary>
        public string PfxPassword { get; set; }
        /// <summary>
        /// Серийный номер сертификата Тинькофф
        /// </summary>
        public string TinkoffCertSerial { get; set; }
        /// <summary>
        /// Адрес хоста (шлюза) 
        /// </summary>
        public string TinkoffGatewayUrl { get; set; }
        /// <summary>
        /// Версия реализации
        /// </summary>
        /// <remarks>
        /// Есть две версии реализации алгоритмов:
        /// <br/>Обе версии возвращают Json-объект
        /// <list type="table">
        /// <item>
        /// <term>0-версия</term>
        /// <description>
        /// Адрес запроса выглядит так:
        /// <br/> http://securepayments.tinkoff.ru/e2c/MethodName/ - E2C-терминал
        /// <br/> http://securepayments.tinkoff.ru/MethodName/ - EACQ-терминал
        /// <br/> Суть версии в том, что она принимает UrlEncoded-параметры 
        /// </description>
        /// </item>
        /// <item>
        /// <term>v2-версия</term>
        /// <description>
        /// Адрес запроса выглядит так:
        /// <br/> http://securepayments.tinkoff.ru/e2c/v2/MethodName/ - E2C-терминал
        /// <br/> http://securepayments.tinkoff.ru/v2/MethodName/ - EACQ-терминал
        /// <br/> V2-версия принимает Json-объекты
        /// </description>
        /// </item>
        /// </list>
        /// Реализации этих версий есть тут: 
        /// <br/><seealso cref="PaymentClassLibrary.TransactionsModels.Tinkoff.TinkoffBankTransaction"/>
        /// <br/><seealso cref="PaymentClassLibrary.TransactionsModels.Tinkoff.TinkoffBankV2Transaction"/>
        /// <para>
        /// Разграничение по версиям происходит через <see cref="PaymentClassLibrary.TransactionsModels.Tinkoff.TinkoffTransactionFactory"/>
        /// </para>
        /// <para>
        /// Пример работы есть в Тинькофф-контроллерах (при изменении версии в конфиге, будет меняться способ отправки запроса и обработки ответа)
        /// <br/><see cref="TinkoffBindController.SendRequest(PaymentClassLibrary.Utils.IModel, string)"/>
        /// </para>
        /// </remarks>
        public int Version { get; set; }

        /// <summary>
        /// Фабрика для получения различных вариантов пути.
        /// <br/>Используется для получения текущей рабочей диркетории на сервере
        /// </summary>
        private PathConfigurator pathConfig = PathConfigurator.Factory.GetPathCurrentDomain();
    }
}
