using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using NLog.Web;

namespace PublicApi
{
    public class ApiService
    {
        private IHost _host;

        public async Task StartAsync(string[] args)
        {
            var logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

            try
            {
                logger.Debug("Start Public API Service");

                _host = await Host.CreateDefaultBuilder(args).UseContentRoot(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
                    .ConfigureLogging(logging =>
                    {
                        //logging.ClearProviders();
                    })
                    .ConfigureWebHostDefaults(webBuilder =>
                    {
                        webBuilder.UseStartup<Startup>();
                    })
                    .UseNLog()
                    .StartAsync();

            }
            catch (Exception exception)
            {
                logger.Error(exception, "Failed to start API");
                throw;
            }
            finally
            {
                NLog.LogManager.Shutdown();
            }
        }

        public async Task StopAsync()
        {
            using (_host)
            {
                await _host.StopAsync(TimeSpan.FromSeconds(5));
            }
        }
    }
}