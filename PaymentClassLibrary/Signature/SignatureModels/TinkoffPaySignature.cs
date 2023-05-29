using PaymentClassLibrary.SignatureMethods;
using PaymentClassLibrary.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PaymentClassLibrary.SignatureModels
{
    /// <summary>
    /// Подпись для оплат Тинькофф
    /// </summary>
    public class TinkoffPaySignature : IModel
    {
        /// <summary>
        /// Токен
        /// </summary>
        [Display(Name = "Token")]
        public string Token { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="token">Токен</param>
        public TinkoffPaySignature(string token)
        {
            Token = token;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="obj"><inheritdoc/></param>
        /// <returns><inheritdoc/></returns>
        public override bool Equals(object obj)
        {
            return obj is TinkoffPaySignature signature &&
                   Token == signature.Token;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns><inheritdoc/></returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
