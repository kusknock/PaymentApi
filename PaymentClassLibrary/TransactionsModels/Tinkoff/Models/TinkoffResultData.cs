using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentClassLibrary.TransactionsModels.Tinkoff
{
    /// <summary>
    /// Общий объект ответов от Тинькофф
    /// </summary>
    public class TinkoffResultData
    {
        /// <summary>
        /// Успешность операции
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Код ошибки, «0» в случае успеха
        /// </summary>
        public string ErrorCode { get; set; }

        /// <summary>
        /// Сообщение
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Детали ошибки
        /// </summary>
        public string Details { get; set; }

        /// <summary>
        /// Платежный ключ, выдается Продавцу при заведении терминала
        /// </summary>
        public string TerminalKey { get; set; }
    }
}
