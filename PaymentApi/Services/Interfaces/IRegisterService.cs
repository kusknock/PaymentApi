using PaymentApi.Models;
using PaymentApi.Models.IdentityModels;
using System.Threading.Tasks;

namespace PaymentApi.Services
{
    /// <summary>
    /// Интерфейс контейнера регистрации
    /// </summary>
    public interface IRegisterService
    {
        /// <summary>
        /// Метод записывающий в базу данные пользователей
        /// </summary>
        /// <param name="model">Данные пользователя</param>
        /// <returns>Успешный или ошибочный <see cref="RegistrationResponse"/></returns>
        Task<RegistrationResponse> Register(RegistrationRequest model);
    }
}