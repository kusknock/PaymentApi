using System;

namespace PaymentApi.Configuration
{
    /// <summary>
    /// Настройки для работы с SBC-терминалами
    /// </summary>
    public class AlfaBankSettings
    {
        /// <summary>
        /// Секретный ключ для доступа к терминалам (передается в модель)
        /// </summary>
        public string MerchantControlKey { get; set; }
        /// <summary>
        /// Логин для авторизации при запросах
        /// </summary>
        public string ConsumerKey { get; set; }
        /// <summary>
        /// Адрес хоста SBC
        /// </summary>
        public string AlfaGatewayUrl { get; set; }

        /// <summary>
        /// Путь до расположения ключа для запроса OAuth
        /// </summary>
        public string PathPrivateKey
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
        /// Фабрика для получения различных вариантов пути.
        /// <br/>Используется для получения текущей рабочей диркетории на сервере
        /// </summary>
        private PathConfigurator pathConfig = PathConfigurator.Factory.GetPathCurrentDomain();
    }
}
