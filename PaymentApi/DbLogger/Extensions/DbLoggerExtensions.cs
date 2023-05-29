using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentApi.DbLogger.Extensions
{
    /// <summary>
    /// Расширения DbLogger
    /// </summary>
    public static class DbLoggerExtensions
    {
        /// <summary>
        /// Добавление контейнера DbLogger в сервисы
        /// </summary>
        /// <param name="builder">Строитель логов из сервисов приложения .NET Core</param>
        /// <returns>Объект строителя</returns>
        public static ILoggingBuilder AddDbLogger(this ILoggingBuilder builder)
        {
            builder.Services.AddSingleton<ILoggerProvider, DbLoggerProvider>();
            return builder;
        }
    }
}
