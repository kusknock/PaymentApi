using Microsoft.AspNetCore.Identity;
using PaymentApi.Models;
using PaymentApi.Models.IdentityModels;
using System.Collections.Generic;

namespace PaymentApi.Models
{
    /// <summary>
    /// Ответ после регистрации
    /// </summary>
    public class RegistrationResponse : IIdentityResponse
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
        /// <inheritdoc/>
        /// </summary>
        public IEnumerable<IdentityError> Errors { get; private set; }

        /// <summary>
        /// Конструктор успешный
        /// </summary>
        /// <param name="user">Данные пользователя</param>
        public RegistrationResponse(User user)
        {
            Id = user.Id;
            Username = user.UserName;
            Errors = null;
        }
        /// <summary>
        /// Конструктор с ошибками
        /// </summary>
        /// <param name="model">Данные для регистрации</param>
        /// <param name="errors">Список ошибок</param>
        public RegistrationResponse(RegistrationRequest model, IEnumerable<IdentityError> errors)
        {
            Id = "-1";
            Username = model.UserName;
            Errors = errors;
        }
    }
}