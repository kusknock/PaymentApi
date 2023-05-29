using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PaymentApi.Models;
using System;
using System.Linq;

namespace PaymentApi.Utils
{
    /// <summary>
    /// Атрибут только для авторизованного доступа к методам или контроллерам
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="context">Контекст запроса</param>
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // пытаемся получаем пользователя из контекста
            var user = context.HttpContext.Items["User"] as User;
            
            // получаем ip, если он есть в белом списке
            var ipAddress = context.HttpContext.Items["IpAddress"] as IpAddress;

            // проверяем "проверки" [условия]
            if (user != null || ipAddress != null)
                return; // даем вызывающему методу работать дальше

            context.Result = new UnauthorizedResult(); // не даем
        }
    }
}