//using Microsoft.AspNetCore.Authorization; // штатные средства авторизации aspnetcore
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PaymentApi.Configuration;
using PaymentApi.DbLogger;
using PaymentApi.Utils; // в этом пространстве имен определен Middleware в котором проводится проверка JWT через самописный фильтр авторизации
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PaymentApi.Controllers
{
    /// <summary>
    /// Контроллер по умолчанию при создании проекта
    /// </summary>
    [LocalOnly]
    [ApiController]
    [Route("[controller]")]
    public class CheckController : ControllerBase
    {
        private readonly ILogRepository log;

        /// <summary>
        /// Конструктор
        /// </summary>
        public CheckController(ILogRepository _log)
        {
            log = _log;
        }

        /// <summary>
        /// Метод Get
        /// </summary>
        [HttpGet]
        public IEnumerable<Log> Get(int count = 10)
        {
            return log.Get(count, true);
        }

        /// <summary>
        /// Метод GetTinkoffSettings
        /// </summary>
        [HttpGet]
        [Route("GetTinkoffSettings")]
        public TinkoffSettings GetTinkoffSettings()
        {
            var tinkoffSettings = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json", false, false)
                 .AddEnvironmentVariables()
                 .Build()
                 .GetSection("TinkoffTestSettings")
                 .Get<TinkoffSettings>();


            return tinkoffSettings;
        }
    }
}
