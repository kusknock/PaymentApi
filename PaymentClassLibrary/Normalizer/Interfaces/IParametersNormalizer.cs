using PaymentClassLibrary.Transport.Requests;
using System.Collections.Generic;
using System.Text;

namespace PaymentClassLibrary.Normalizer
{
    /// <summary>
    /// Интерфейс нормализатора
    /// </summary>
    public interface IParametersNormalizer
    {
        /// <summary>
        /// Нормализует параметры в одну строку (важно для некоторых типов запросов, например)
        /// <br/>Пример тут: <seealso cref="OAuthRequest.GetRequestDataBody"/>
        /// </summary>
        /// <param name="data">Данные для нормализации</param>
        /// <returns>Строка запроса</returns>
        string GetNormalizedParameters(IDictionary<string, string> data);
    }
}