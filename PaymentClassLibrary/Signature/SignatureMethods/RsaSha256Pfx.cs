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
using System.Security.Cryptography.X509Certificates;

namespace PaymentClassLibrary.SignatureMethods
{
    /// <summary>
    /// Метод RSA-SHA256
    /// </summary>
    public class RsaSha256Pfx : ISignatureX509
    {
        private X509Certificate2 cert;
        /// <summary>
        /// Серийный номер сертификата
        /// </summary>
        public string CertSerial { get { return cert.GetSerialNumberString(); } }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string Name { get { return "RSA-SHA256"; } }

        private ICollectSignatureString stringCollector { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="certPath">Ключ шифрования</param>
        /// <param name="password">Пароль от контейнера ключа</param>
        /// <param name="stringCollector">Сборщик строк</param>
        public RsaSha256Pfx(string certPath, string password, ICollectSignatureString stringCollector)
        {
            cert = new X509Certificate2(certPath, password, X509KeyStorageFlags.Exportable);
            this.stringCollector = stringCollector;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="signatureBaseString"><inheritdoc/></param>
        /// <returns><inheritdoc/></returns>
        public string GetSignature(string signatureBaseString)
        {
            RSACryptoServiceProvider key = new RSACryptoServiceProvider();
            key.FromXmlString(cert.PrivateKey.ToXmlString(true));

            byte[] baseStringBytes = Convert.FromBase64String(signatureBaseString);
            byte[] signedHashValue = key.SignData(baseStringBytes, CryptoConfig.MapNameToOID("SHA256"));

            string base64StringHash = Convert.ToBase64String(signedHashValue);

            return base64StringHash;
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