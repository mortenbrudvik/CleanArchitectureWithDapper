using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;

namespace PublicApi
{
    public class ApiService
    {
        private IHost _host;

        public async Task StartAsync(string[] args)
        {
            var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
            try
            {
                logger.Debug("Start Public API Service");

                _host = CreateHostBuilder(args);

                await _host.StartAsync();
            }
            catch (Exception exception)
            {
                logger.Error(exception, "Failed to start API", exception);
                throw;
            }
        }

        public async Task StopAsync()
        {
            using (_host)
            {
                NLog.LogManager.Shutdown();
                await _host.StopAsync(TimeSpan.FromSeconds(5));
            }
        }

        private IHost CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args).UseContentRoot(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.SetMinimumLevel(LogLevel.Trace);
                })
                .UseNLog()
                .Build();
    }
}