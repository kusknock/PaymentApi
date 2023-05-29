using PaymentClassLibrary.SignatureCollectors;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace PaymentClassLibrary.SignatureMethods
{
    /// <summary>
    /// Метод RSA-SHA256
    /// </summary>
    public class RsaSha256 : ISignatureMethod
    {
        private string accessKey;

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string Name { get { return "RSA-SHA256"; } }

        private ICollectSignatureString stringCollector { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="accessKey">Ключ шифрования</param>
        /// <param name="stringCollector">Сборщик строк</param>
        public RsaSha256(string accessKey, ICollectSignatureString stringCollector)
        {
            this.accessKey = accessKey;
            this.stringCollector = stringCollector;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="signatureBaseString"><inheritdoc/></param>
        /// <returns><inheritdoc/></returns>
        public string GetSignature(string signatureBaseString)
        {
            using (TextReader reader = new StringReader(accessKey))
            {
                AsymmetricCipherKeyPair keyPair = (AsymmetricCipherKeyPair)new PemReader(reader).ReadObject();

                RsaPrivateCrtKeyParameters keyParams = (RsaPrivateCrtKeyParameters)keyPair.Private;
                RSAParameters rsaParameters = DotNetUtilities.ToRSAParameters(keyParams);

                RSACryptoServiceProvider key = new RSACryptoServiceProvider();
                key.ImportParameters(rsaParameters);

                byte[] baseStringBytes = Encoding.UTF8.GetBytes(signatureBaseString);
                byte[] signedHashValue = key.SignData(baseStringBytes, CryptoConfig.MapNameToOID("SHA256"));

                string base64StringHash = Convert.ToBase64String(signedHashValue);

                return base64StringHash;
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