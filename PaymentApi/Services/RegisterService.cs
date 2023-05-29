using Microsoft.AspNetCore.Identity;
using PaymentApi.Models;
using System.Threading.Tasks;

namespace PaymentApi.Services
{
    /// <summary>
    /// Контейнер для регистрации пользователей
    /// </summary>
    public class RegisterService : IRegisterService
    {
        private readonly UserManager<User> _userManager;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="userManager">Контекст БД через Identity</param>
        public RegisterService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="model"><inheritdoc/></param>
        /// <returns><inheritdoc/></returns>
        public async Task<RegistrationResponse> Register(RegistrationRequest model)
        {
            var user = new User { UserName = model.UserName };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
                return new RegistrationResponse(model, result.Errors);

            return new RegistrationResponse(user);
        }
    }
}