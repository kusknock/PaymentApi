using PaymentClassLibrary.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PaymentClassLibrary.SignatureModels
{
    /// <summary>
    /// Модель подписи для OAuth
    /// </summary>
    public class OAuthSignature : IModel
    {
        /// <summary>
        /// Строка подписи
        /// </summary>
        [Display(Name = "signature")]
        public string Signature { get; internal set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="signature">Подпись</param>
        public OAuthSignature(string signature)
        {
            Signature = signature;
        }
    }
}
