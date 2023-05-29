using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PaymentApi.DbLogger;
using PaymentApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace PaymentApi.Tests
{
    public class LoggerTests
    {
        private readonly ITestOutputHelper output;
        private readonly ILogRepository repo;

        public LoggerTests(ITestOutputHelper _output)
        {
            output = _output;

            var connectionString = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, false)
                .AddEnvironmentVariables()
                .Build().GetConnectionString("DefaultConnection");

            var dbSettings = new DbContextOptionsBuilder()
                .UseNpgsql(connectionString);

            repo = new LogRepository(new ApplicationContext(dbSettings.Options));
            
        }

        [Fact]
        public void LogTests()
        {
            var count = repo.GetAll().Count();

            var log = new Log
            {
                CategoryName = "",
                LogLevel = "",
                Msg = "",
                Timestamp = DateTime.Now,
                User = "user"
            };

            repo.Log(log);

            var items = repo.GetAll();

            var addedItem = items.ToList()[items.Count()-1];

            output.WriteLine(addedItem.User);

            Assert.Equal(addedItem.Id, log.Id);

            repo.Remove(log);

            Assert.Equal(count, repo.GetAll().Count());
        }
    }
}
