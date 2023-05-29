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
    /// Удаление карты из системы банка (в банке карта помечается «D»)
    /// </summary>
    public class RemoveCardData : IModel
    {
        /// <summary>
        /// Идентификатор терминала
        /// </summary>
        [Required]
        public string TerminalKey { get; set; }
        /// <summary>
        /// Идентификатор клиента
        /// </summary>
        [Required]
        public string CustomerKey { get; set; }
        /// <summary>
        /// Идентификатор карты в системе банка
        /// </summary>
        [Required]
        public string CardId { get; set; }
    }
}
