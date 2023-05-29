using System.Text;

namespace PaymentClassLibrary.ApiUrlFactory
{
    /// <summary>
    /// Конструктор адреса для обращения к платежному сервису
    /// </summary>
    public class BankApiUrl
    {
        private readonly string apiUrl;

        /// <summary>
        /// Объект фабрики для создания <see cref="BankApiUrl"/>
        /// </summary>
        public readonly static BankApiUrlFactory Factory = new();

        /// <summary>
        /// Приватный конструктор
        /// </summary>
        /// <param name="apiUrl">Адрес API</param>
        private BankApiUrl(string apiUrl)
        {
            this.apiUrl = apiUrl;
        }

        /// <summary>
        /// Getter API-url
        /// </summary>
        public string ApiUrl => apiUrl;

        /// <summary>
        /// Фабрика для создания API-url
        /// </summary>
        public class BankApiUrlFactory
        {
            /// <summary>
            /// Метод для получения адреса Тинькофф-сервиса
            /// </summary>
            /// <param name="GatewayUrl">Шлюз</param>
            /// <param name="ApiMethod">Метод сервиса</param>
            /// <param name="IsE2C">Флаг протокола E2C</param>
            /// <param name="Version">Версия протокола</param>
            /// <returns>Объект адреса</returns>
            public BankApiUrl GetTinkoffUrl(string GatewayUrl, string ApiMethod, bool IsE2C, int Version = 0)
            {
                if (GatewayUrl.EndsWith("/"))
                    GatewayUrl = GatewayUrl.Remove(GatewayUrl.Length - 1);

                var apiUrlBuilder = new StringBuilder(GatewayUrl);

                if (IsE2C) 
                    apiUrlBuilder.Append("/e2c");

                if (Version != 0) 
                    apiUrlBuilder.Append($"/v{Version}");

                apiUrlBuilder.Append($"/{ApiMethod}");

                return new BankApiUrl(apiUrlBuilder.ToString());
            }

            /// <summary>
            /// Метод для получения Альфа-сервиса
            /// </summary>
            /// <param name="GatewayUrl">Шлюз</param>
            /// <param name="Version">Версия (для перевода денег 4, всё остальное 2)</param>
            /// <param name="ApiMethod">Метод сервиса</param>
            /// <param name="EndPointId">Идентификатор Endpoint'а</param>
            /// <returns>Объект адреса</returns>
            public BankApiUrl GetAlfaBankUrl(string GatewayUrl, int Version, string ApiMethod, string EndPointId)
            {
                if (GatewayUrl.EndsWith("/"))
                    GatewayUrl = GatewayUrl.Remove(GatewayUrl.Length-1);

                string _version = $"v{Version}";

                return new BankApiUrl($"{GatewayUrl}/paynet/api/{_version}/{ApiMethod}/{EndPointId}");
            }
        }
    }
}