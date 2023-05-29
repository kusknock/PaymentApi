using PaymentClassLibrary.Signature;
using PaymentClassLibrary.Signature.SignatureMethods;
using PaymentClassLibrary.SignatureCollectors;
using PaymentClassLibrary.SignatureModels;
using PaymentClassLibrary.Utils;
using PaymentClassLibrary.Utils.TypeModels;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PaymentClassLibrary.TransactionsModels.Tinkoff
{
    /// <summary>
    /// Нотификации по оплатам
    /// </summary>
    public class TinkoffPayNotification : IModel
    {
        /// <summary>
        /// Идентификатор карты
        /// </summary>
        public string CardId { get; set; }
        /// <summary>
        /// Первые и последние 4 цифры карты
        /// </summary>
        public string Pan { get; set; }
        /// <summary>
        /// Срок действия карты
        /// </summary>
        public string ExpDate { get; set; }
        /// <summary>
        /// Терминал
        /// </summary>
        public string TerminalKey { get; set; }
        /// <summary>
        /// Сумма
        /// </summary>
        public string Amount { get; set; }
        /// <summary>
        /// Идентификатор заявки
        /// </summary>
        public string OrderId { get; set; }
        /// <summary>
        /// Успешность операции (приходит капсом, поэтому поле игнорируется при создании токена)
        /// </summary>
        [OnlySignatureField]
        public string Success { get; set; }
        /// <summary>
        /// Успешность (в нижнем регистре для создания токена)
        /// </summary>
        public string SuccessLower { get { return Success.ToLower(); } }
        /// <summary>
        /// Статус операции
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// Идентификатор платежа
        /// </summary>
        public string PaymentId { get; set; }
        /// <summary>
        /// Код ошибки
        /// </summary>
        public string ErrorCode { get; set; }
        /// <summary>
        /// Пароль для доступа к терминалу
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Токен (подпись)
        /// </summary>
        [OnlySignatureField]
        public string Token { get; set; }

        /// <summary>
        /// Создание объекта подписи из нотификации
        /// </summary>
        /// <returns>Подпись TinkoffPay</returns>
        public TinkoffPaySignature GetTinkoffPaySignature()
        {
            return new TinkoffPaySignature(Token);
        }
    }
}