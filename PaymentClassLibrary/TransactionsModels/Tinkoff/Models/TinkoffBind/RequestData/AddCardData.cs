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
    /// Привязка карты 
    /// </summary>
    public class AddCardData : IModel
    {
        /// <summary>
        /// Терминал
        /// </summary>
        [Required]
        public string TerminalKey { get; set; }
        /// <summary>
        /// Идентификатор клиента
        /// </summary>
        [Required]
        public string CustomerKey { get; set; }
        /// <summary>
        /// Тип привязки (NO/HOLD/3DS/3DSHOLD)
        /// </summary>
        [Required]
        public string CheckType { get; set; }
    }
}
