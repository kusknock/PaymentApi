using Microsoft.AspNetCore.Identity;
using PaymentApi.Models.IdentityModels;
using System.Collections.Generic;

namespace PaymentApi.Models
{
    /// <summary>
    /// Ответ после аутентификации
    /// </summary>
    public class AuthenticateResponse : IIdentityResponse
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Токен JWT
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public IEnumerable<IdentityError> Errors { get; private set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="user">Данные пользователя</param>
        public AuthenticateResponse(User user)
        {
            Id = user.Id;
            Username = user.UserName;
            Token = user.JwtToken;
            Errors = null;
        }

        /// <summary>
        /// Конструктор с ошибками
        /// </summary>
        /// <param name="model">Данные запроса</param>
        /// <param name="errors">Полученные ошибки</param>
        public AuthenticateResponse(AuthenticateRequest model, IEnumerable<IdentityError> errors)
        {
            Id = "-1";
            Username = model.UserName;
            Token = null;
            Errors = errors;
        }
    }
}