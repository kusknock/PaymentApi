using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentApi
{
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public class Program
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="args"><inheritdoc/></param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="args"><inheritdoc/></param>
        /// <returns><inheritdoc/></returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseUrls("http://localhost:5000", "http://10.128.0.6:5000" );
                });
    }
}
