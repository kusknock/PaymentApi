using PaymentClassLibrary.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentClassLibrary.TransactionsModels.Tinkoff
{
    /// <summary>
    /// Совершение выплаты
    /// </summary>
    public class PayTransferData : IModel
    {
        /// <summary>
        /// Терминал
        /// </summary>
        [Required]
        public string TerminalKey { get; set; }
        /// <summary>
        /// Идентификатор платежа
        /// </summary>
        [Required]
        public string PaymentId { get; set; }
    }
}
