using Microsoft.AspNetCore.Identity;
using PaymentApi.Models.IdentityModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentApi.Models
{
    /// <summary>
    /// Модель запроса на аутентификацию
    /// </summary>
    public class AuthenticateRequest 
    {
        /// <summary>
        /// Имя пользователя (обязательное поле)
        /// </summary>
        [Required]
        public string UserName { get; set; }
        /// <summary>
        /// Пароль (обязательное поле)
        /// </summary>
        [Required]
        public string Password { get; set; }
    }
}
