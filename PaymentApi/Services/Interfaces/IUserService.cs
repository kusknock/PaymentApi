using PaymentApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PaymentApi.Services
{
    /// <summary>
    /// Интерфейс контейнера аутентификации
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Получаем из базы данные пользователя
        /// </summary>
        /// <param name="model">Данные запроса на аутентификацию</param>
        /// <returns>Успешный или ошибочный <see cref="AuthenticateResponse"/></returns>
        Task<AuthenticateResponse> Authenticate(AuthenticateRequest model);
        /// <summary>
        /// Возвращает список всех пользователей
        /// </summary>
        IEnumerable<User> GetAll();
        /// <summary>
        /// Возвращает пользователя по идентификатору
        /// </summary>
        Task<User> GetById(string id);
    }
}