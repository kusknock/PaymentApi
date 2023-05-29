using PaymentClassLibrary.SignatureCollectors;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace PaymentClassLibrary.SignatureMethods
{
    /// <summary>
    /// Реализация хэш-алгоритма SHA256
    /// </summary>
    public class Sha256 : ISignatureMethod
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string Name { get { return "SHA256"; } }

        private ICollectSignatureString stringCollector { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="stringCollector">Сборщик строк</param>
        public Sha256(ICollectSignatureString stringCollector)
        {
            this.stringCollector = stringCollector;
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        public Sha256() { }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="signatureBaseString"><inheritdoc/></param>
        /// <returns><inheritdoc/></returns>
        public string GetSignature(string signatureBaseString)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(signatureBaseString);
                byte[] outputBytes = sha256.ComputeHash(inputBytes);

                string result = Convert.ToBase64String(outputBytes);

                return result;
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