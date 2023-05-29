using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PaymentApi.Models;
using PaymentApi.Models.IdentityModels;
using PaymentApi.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PaymentApi.Services
{

    /// <summary>
    /// Репозиторий для работы пользователями из БД
    /// </summary>
    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;

        private readonly UserManager<User> _userManager;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="appSettings">Параметры приложения для создания токена</param>
        /// <param name="userManager">Контекст БД</param>
        public UserService(IOptions<AppSettings> appSettings, UserManager<User> userManager)
        {
            _appSettings = appSettings.Value;
            _userManager = userManager;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="model"><inheritdoc/></param>
        /// <returns><inheritdoc/></returns>
        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);

            // проверяем существование пользователя
            if (user == null)
                return ErrorResponse(model, IdentityTypeErrors.UserNotFound, "User not found");

            var isPassValid = await _userManager.CheckPasswordAsync(user, model.Password);

            // проверяем правильно ли он ввел пароль
            if (!isPassValid)
                return ErrorResponse(model, IdentityTypeErrors.InvalidUserNameOrPassword, "Invalid UserName or Password");

            // генерируем токен записываем его в модель
            user.JwtToken = GenerateJwtToken(user);

            var resultUpdate = await _userManager.UpdateAsync(user);

            // проверяем успешность обновления
            if (!resultUpdate.Succeeded)
                return new AuthenticateResponse(model, resultUpdate.Errors);

            return new AuthenticateResponse(user);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public IEnumerable<User> GetAll()
        {
            return _userManager.Users.ToList();
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public async Task<User> GetById(string id)
        {
            return await _userManager.FindByIdAsync(id.ToString());
        }

        /// <summary>
        /// Формирование ошибочного ответа
        /// </summary>
        /// <param name="model">Данные аутентификации</param>
        /// <param name="code">Код ошибки</param>
        /// <param name="description">Описание ошибки</param>
        /// <returns></returns>
        private static AuthenticateResponse ErrorResponse(AuthenticateRequest model, string code, string description)
        {
           var errors = new List<IdentityError>
                {
                    new IdentityError()
                    {
                        Code = code,
                        Description = description
                    }
                };

            return new AuthenticateResponse(model, errors);
        }

        /// <summary>
        /// Генерация JWT
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private string GenerateJwtToken(User user)
        {
            // Создание токена на 7 дней
            // TODO: можно вынести количество дней в настройки
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}