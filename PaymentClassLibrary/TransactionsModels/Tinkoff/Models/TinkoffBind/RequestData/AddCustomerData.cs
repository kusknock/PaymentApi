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
    /// Добавление клиента
    /// </summary>
    public class AddCustomerData : IModel
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
        /// Почта
        /// </summary>
        [Required]
        public string Email { get; set; }
        /// <summary>
        /// Телефон
        /// </summary>
        [Required]
        public string Phone { get; set; }
    }
}
