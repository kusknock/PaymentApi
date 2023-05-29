using System.Collections.Generic;
using System.Text;

namespace PaymentClassLibrary.SignatureCollectors
{
    /// <summary>
    /// Простой коллектор
    /// </summary>
    public class NormalSignatureCollector : ICollectSignatureString
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public NormalSignatureCollector()
        {
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="data"><inheritdoc/></param>
        /// <returns></returns>
        public string GetSignatureBaseString(IDictionary<string, string> data)
        {
            StringBuilder signatureBaseString = new StringBuilder();

            foreach (var item in data)
            {
                signatureBaseString.Append(item.Value);
            }

            return signatureBaseString.ToString();
        }
    }
}
