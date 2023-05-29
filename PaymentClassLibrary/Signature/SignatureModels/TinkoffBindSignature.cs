using PaymentClassLibrary.SignatureMethods;
using PaymentClassLibrary.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PaymentClassLibrary.SignatureModels
{
    /// <summary>
    /// Модель подписи для Тинькофф E2C алгоритмов
    /// </summary>
    public class TinkoffBindSignature : IModel
    {
        private ISignatureX509 signatureMethod;

        /// <summary>
        /// Хэш SHA256Hexadecimal строки 
        /// </summary>
        [Display(Name = "DigestValue")]
        public string DigestValue { get; set; }

        /// <summary>
        /// Подпись DigestValue
        /// </summary>
        [Display(Name = "SignatureValue")]
        public string SignatureValue { get; set; }

        /// <summary>
        /// Серийный номер сертификата
        /// </summary>
        [Display(Name = "X509SerialNumber")]
        public string CertSerial { get { return signatureMethod.CertSerial; } }

        /// <summary>
        /// Конструктор 
        /// </summary>
        /// <param name="digestValue">Хэщ</param>
        /// <param name="signature">Подпись</param>
        /// <param name="signatureMethod">Метод шифрования</param>
        public TinkoffBindSignature(string digestValue, string signature, ISignatureMethod signatureMethod)
        {
            DigestValue = digestValue;
            SignatureValue = signature;
            this.signatureMethod = signatureMethod as ISignatureX509;
        }
    }
}
