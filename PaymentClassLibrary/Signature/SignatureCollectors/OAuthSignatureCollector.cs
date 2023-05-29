using PaymentClassLibrary.Normalizer;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentClassLibrary.SignatureCollectors
{
    /// <summary>
    /// Коллектор строк для подписи OAuth запроса
    /// </summary>
    public class OAuthSignatureCollector : ICollectSignatureString
    {
        private readonly string apiUrl;
        private readonly IParametersNormalizer normalizer;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="apiUrl">Адрес отправки запроса</param>
        /// <param name="normalizer">Нормализатор параметров</param>
        public OAuthSignatureCollector(string apiUrl, IParametersNormalizer normalizer)
        {
            this.apiUrl = apiUrl;
            this.normalizer = normalizer;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="data"><inheritdoc/></param>
        /// <returns><inheritdoc/></returns>
        public string GetSignatureBaseString(IDictionary<string, string> data)
        {
            var result = new StringBuilder();

            result.Append("POST").Append("&");
            result.Append(Uri.EscapeDataString(apiUrl)).Append("&");
            result.Append(Uri.EscapeDataString(normalizer.GetNormalizedParameters(data)));

            return result.ToString();
        }
    }
}
