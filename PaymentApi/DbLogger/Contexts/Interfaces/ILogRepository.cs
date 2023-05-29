using System.Collections.Generic;

namespace PaymentApi.DbLogger
{
    /// <summary>
    /// Репозиторий для логов (от него можно создавать логи разных конфигураций)
    /// </summary>
    public interface ILogRepository
    {
        /// <summary>
        /// Запись в лог
        /// </summary>
        /// <param name="log">Объект с данными лога</param>
        Log Log(Log log);

        /// <summary>
        /// Получить данные логов
        /// </summary>
        /// <returns>Список</returns>
        IEnumerable<Log> GetAll();

        /// <summary>
        /// Получение определенное число записей из лога
        /// </summary>
        /// <param name="count">Число записей</param>
        /// <param name="desc">Сортировка по убыванию</param>
        /// <returns>Список записей лога</returns>
        IEnumerable<Log> Get(int count, bool desc);

        /// <summary>
        /// Удалить запись из лога
        /// </summary>
        /// <param name="log">Объект для удаления</param>
        void Remove(Log log);
    }
}