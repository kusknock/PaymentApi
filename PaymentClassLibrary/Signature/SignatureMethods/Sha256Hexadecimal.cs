using PaymentClassLibrary.SignatureCollectors;
using PaymentClassLibrary.SignatureMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PaymentClassLibrary.Signature.SignatureMethods
{
    /// <summary>
    /// Реализация хэш-алгоритма SHA256 c возвратом Hexadecimal-значения подписи
    /// </summary>
    public class Sha256Hexadecimal : ISignatureMethod
    {
        /// <summary>
        /// Название
        /// </summary>
        public string Name { get { return "SHA256"; } }

        private ICollectSignatureString stringCollector { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="stringCollector">Коллектор строк</param>
        public Sha256Hexadecimal(ICollectSignatureString stringCollector)
        {
            this.stringCollector = stringCollector;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="signatureBaseString"><inheritdoc/></param>
        /// <returns><inheritdoc/></returns>
        public string GetSignature(string signatureBaseString)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(signatureBaseString));

                StringBuilder builder = new StringBuilder();

                for (int i = 0; i < hashBytes.Length; i++)
                {
                    builder.Append(hashBytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="data"><inheritdoc/></param>
        /// <returns><inheritdoc/></returns>
        public string GetSignatureBaseString(IDictionary<string, string> data)
        {
            return stringCollector.GetSignatureBaseString(data);
        }
    }
}
