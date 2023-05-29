using PaymentClassLibrary.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PaymentClassLibrary.SignatureModels
{
    /// <summary>
    /// Модель подписи SBC
    /// </summary>
    public class AlfaBankSignature : IModel
    {
        /// <summary>
        /// Токен
        /// </summary>
        [Display(Name = "control")]
        public string Control { get; internal set; }

        /// <summary>
        /// Конструктор 
        /// </summary>
        /// <param name="control">Control токен</param>
        public AlfaBankSignature(string control)
        {
            Control = control;
        }
    }
}
