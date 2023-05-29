using System;

namespace PaymentApi.DbLogger
{
    /// <summary>
    /// Модель логов
    /// </summary>
    public class Log
    {
        /// <summary>
        /// Идентификатор лога
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Уровень логгирования
        /// </summary>
        public string LogLevel { get; set; }
        /// <summary>
        /// Категория исключения
        /// </summary>
        public string CategoryName { get; set; }
        /// <summary>
        /// Сообщение
        /// </summary>
        public string Msg { get; set; }
        /// <summary>
        /// Пользователь от которого была совершена запись в лог
        /// </summary>
        public string User { get; set; }
        /// <summary>
        /// Временная метка
        /// </summary>
        public DateTime Timestamp { get; set; }
    }
}