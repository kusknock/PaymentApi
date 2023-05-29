using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentApi.Models.IdentityModels
{
    /// <summary>
    /// Типы ошибок при регистрации и авторизации пользователей
    /// </summary>
    public static class IdentityTypeErrors
    {
        /// <summary>
        /// Пользователя не существует
        /// </summary>
        public static readonly string UserNotFound = "404";
        /// <summary>
        /// Неправильно введенные данные
        /// </summary>
        public static readonly string InvalidUserNameOrPassword = "400";
    }
}
