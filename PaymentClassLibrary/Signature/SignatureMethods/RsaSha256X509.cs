using PaymentClassLibrary.SignatureCollectors;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace PaymentClassLibrary.SignatureMethods
{
    /// <summary>
    /// Реализация RSA-SHA256 c извлечением сертификата из контейнера
    /// </summary>
    public class RsaSha256X509 : ISignatureX509
    {
        /// <summary>
        /// Серийный номер сертификата
        /// </summary>
        public string CertSerial { get; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string Name { get { return "RSA-SHA256"; } }

        private ICollectSignatureString stringCollector { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="CertSerial">Серийный номер сертификата</param>
        /// <param name="stringCollector">Коллектор строк</param>
        public RsaSha256X509(string CertSerial, ICollectSignatureString stringCollector)
        {
            this.CertSerial = CertSerial;
            this.stringCollector = stringCollector;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="signatureBaseString"><inheritdoc/></param>
        /// <returns><inheritdoc/></returns>
        public string GetSignature(string signatureBaseString)
        {
            var myStore = new X509Store(StoreName.My, StoreLocation.LocalMachine);
            myStore.Open(OpenFlags.ReadOnly);
            X509Certificate2 myCert = myStore.Certificates.Find(X509FindType.FindBySerialNumber, CertSerial, true)[0];

            RSACryptoServiceProvider key = new RSACryptoServiceProvider();
            key.FromXmlString(myCert.PrivateKey.ToXmlString(true));

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