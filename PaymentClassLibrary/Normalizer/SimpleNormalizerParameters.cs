using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentClassLibrary.Normalizer
{
    /// <summary>
    /// Нормализатор
    /// </summary>
    public class SimpleNormalizerParameters : IParametersNormalizer
    {
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public SimpleNormalizerParameters() { }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="data"><inheritdoc/></param>
        /// <returns><inheritdoc/></returns>
        public string GetNormalizedParameters(IDictionary<string, string> data)
        {
            var result = new StringBuilder();

            foreach (var item in data)
            {
                if (result.Length > 0)
                {
                    result.Append('&');
                }

                result.Append(Uri.EscapeDataString(item.Key)).Append('=').Append(Uri.EscapeDataString(item.Value));
            }

            return result.ToString();
        }

    }
}
