using Microsoft.AspNetCore.Identity;

namespace PaymentApi.Models
{
    /// <summary>
    /// Модель пользователя, отнаследованная от Identity
    /// </summary>
    public class User : IdentityUser
    {
        /// <summary>
        /// Токен
        /// </summary>
        public string JwtToken { get; set; }
    }
}
