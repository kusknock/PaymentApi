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
    /// Получение данных клиента
    /// </summary>
    public class CustomerData : IModel
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
    }
}
