using PaymentClassLibrary.SignatureCollectors;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace PaymentClassLibrary.SignatureMethods
{
    /// <summary>
    /// Реализация хэш-алгоритма SHA1
    /// </summary>
    public class Sha1 : ISignatureMethod
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string Name { get { return "SHA1"; } }

        private ICollectSignatureString stringCollector { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="stringCollector">Сборщик строк</param>
        public Sha1(ICollectSignatureString stringCollector)
        {
            this.stringCollector = stringCollector;
        }

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public Sha1() { }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="signatureBaseString"><inheritdoc/></param>
        /// <returns><inheritdoc/></returns>
        public string GetSignature(string signatureBaseString)
        {
            using (var sha1 = SHA1.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(signatureBaseString);
                byte[] outputBytes = sha1.ComputeHash(inputBytes);

                string result = BitConverter.ToString(outputBytes).Replace("-", "").ToLower();

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