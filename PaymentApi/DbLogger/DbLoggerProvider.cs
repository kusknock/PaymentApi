using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PaymentApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentApi.DbLogger
{
    /// <summary>
    /// Провайдер для логгирования в базу данных
    /// </summary>
    public class DbLoggerProvider : ILoggerProvider
    {
        private readonly ApplicationContext _context;

        /// <summary>
        /// Конструктор
        /// </summary>
        public DbLoggerProvider()
        {
            /*ALERT GOVNOCODE: ANTI-PATTERN ТАК ПЛОХО! (ПОКА ВЫБОРА НЕТ)*/
            var connectionString = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, false)
                .AddEnvironmentVariables()
                .Build().GetConnectionString("DefaultConnection");
            /*ALERT GOVNOCODE: ANTI-PATTERN ТАК ПЛОХО! (ПОКА ВЫБОРА НЕТ)*/

            var dbSettings = new DbContextOptionsBuilder()
                .UseNpgsql(connectionString);

            _context = new ApplicationContext(dbSettings.Options);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="categoryName"><inheritdoc/></param>
        /// <returns></returns>
        public ILogger CreateLogger(string categoryName)
        {
            return new Logger(categoryName, new LogRepository(_context));
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);   
        }

        /// <summary>
        /// Логгер
        /// </summary>
        public class Logger : ILogger
        {
            private readonly string _categoryName;
            private readonly ILogRepository _repo;

            /// <summary>
            /// Конструктор
            /// </summary>
            /// <param name="categoryName">Категория сообщения для лога</param>
            /// <param name="repo">Репозиторий лога</param>
            public Logger(string categoryName, ILogRepository repo)
            {
                _repo = repo;
                _categoryName = categoryName;
            }

            /// <summary>
            /// <inheritdoc/>
            /// </summary>
            /// <param name="logLevel"><inheritdoc/></param>
            /// <returns><inheritdoc/></returns>
            public bool IsEnabled(LogLevel logLevel)
            {
                return true;
            }

            /// <summary>
            /// <inheritdoc/>
            /// </summary>
            /// <typeparam name="TState"><inheritdoc/></typeparam>
            /// <param name="logLevel"><inheritdoc/></param>
            /// <param name="eventId"><inheritdoc/></param>
            /// <param name="state"><inheritdoc/></param>
            /// <param name="exception"><inheritdoc/></param>
            /// <param name="formatter"><inheritdoc/></param>
            public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
            {
                if (logLevel == LogLevel.Critical || logLevel == LogLevel.Error || logLevel == LogLevel.Warning)
                    RecordMsg(logLevel, eventId, state, exception, formatter);
            }

            /// <summary>
            /// <inheritdoc/>
            /// </summary>
            /// <typeparam name="TState"><inheritdoc/></typeparam>
            /// <param name="logLevel"><inheritdoc/></param>
            /// <param name="eventId"><inheritdoc/></param>
            /// <param name="state"><inheritdoc/></param>
            /// <param name="exception"><inheritdoc/></param>
            /// <param name="formatter"><inheritdoc/></param>
            private void RecordMsg<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
            {
                string stackTrace = exception is not null ? exception.StackTrace : "exception is null";

                _repo.Log(new Log
                {
                    LogLevel = logLevel.ToString(),
                    CategoryName = _categoryName,
                    Msg = string.Format("{0}, {1}, {2}", formatter(state, exception), stackTrace, eventId.Name),
                    User = "username",
                    Timestamp = DateTime.Now
                });
            }

            /// <summary>
            /// <inheritdoc/>
            /// </summary>
            /// <typeparam name="TState"><inheritdoc/></typeparam>
            /// <param name="state"><inheritdoc/></param>
            /// <returns><inheritdoc/></returns>
            public IDisposable BeginScope<TState>(TState state)
            {
                return new NoopDisposable();
            }

            /// <summary>
            /// <inheritdoc/>
            /// </summary>
            private class NoopDisposable : IDisposable
            {
                /// <summary>
                /// <inheritdoc/>
                /// </summary>
                public void Dispose()
                {
                }
            }
        }
    }
}
