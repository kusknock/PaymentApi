using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaymentClassLibrary.SignatureCollectors
{
    /// <summary>
    /// Коллектор строк для подписи с сортировкой
    /// </summary>
    public class SignatureCollectorWithSort : ICollectSignatureString
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public SignatureCollectorWithSort() { }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="data"><inheritdoc/></param>
        /// <returns><inheritdoc/></returns>
        public string GetSignatureBaseString(IDictionary<string, string> data)
        {
            StringBuilder dictionaryValues = new StringBuilder();

            foreach (var item in data.OrderBy(pair => pair.Key))
            {
                dictionaryValues.Append(item.Value);
            }

            return dictionaryValues.ToString();
        }
    }
}
