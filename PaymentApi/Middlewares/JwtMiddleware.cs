using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PaymentApi.Configuration;
using PaymentApi.Services;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentApi.Middlewares
{
    /// <summary>
    /// Промежуточный обработчик для проверки JWT
    /// </summary>
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings _appSettings;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="next"><inheritdoc/></param>
        /// <param name="appSettings">Параметры для проверки JWT</param>
        public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings)
        {
            _next = next;
            _appSettings = appSettings.Value;
        }

        /// <summary>
        /// Вызов обработчика
        /// </summary>
        /// <param name="context">Контекст запроса</param>
        /// <param name="userService">Контекст БД с данными пользователей</param>
        public async Task Invoke(HttpContext context, IUserService userService)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
                await AttachUserToContext(context, userService, token);

            await _next(context);
        }

        /// <summary>
        /// Метод валидации токена
        /// </summary>
        /// <param name="context">Контекст запроса</param>
        /// <param name="userService">Контекст БД с данными пользователей</param>
        /// <param name="token">Токен из блока Authorization</param>
        private async Task AttachUserToContext(HttpContext context, IUserService userService, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = jwtToken.Claims.First(x => x.Type == "id").Value;

                // Добавление в контекст запроса параметр User, если он был найден в БД
                context.Items["User"] = await userService.GetById(userId);
            }
            catch
            {
                // TODO: тут может логирование сделать
            }
        }
    }
}