using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PaymentApi.Configuration;
using PaymentApi.Models;
using PaymentApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentApi.Middlewares
{
    /// <summary>
    /// Промежуточная обработка запроса для проверки IP-адреса
    /// </summary>
    public class CheckIpMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="next"><inheritdoc/></param>
        public CheckIpMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Метод для вызова Middleware во время запроса
        /// </summary>
        /// <param name="context">Контекст запроса</param>
        /// <param name="appContext">Контекст БД</param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context, ApplicationContext appContext)
        {
            context.Items["IpAddress"] = appContext.IpAddresses
                                                   .Where(ip => ip.Host == context.Connection.RemoteIpAddress.ToString())
                                                   .FirstOrDefault();

            await _next(context);
        }
    }
}
