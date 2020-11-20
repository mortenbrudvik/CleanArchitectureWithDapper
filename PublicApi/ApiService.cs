using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
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

                _host = CreateHost(args);
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
            if(_host==null)
                return;
            
            using (_host)
            {
                await _host.StopAsync(TimeSpan.FromSeconds(5));
            }
        }

        private static IHost CreateHost(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.SetMinimumLevel(LogLevel.Trace);
                    logging.AddNLog("nlog.config");
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    var apiUrl = GetSetting("apiServiceUrl");
                    var launchBrowser = bool.Parse(GetSetting("launchBrowser"));

                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseUrls(apiUrl);
                    webBuilder.UseKestrel();

                    if(launchBrowser)
                        Process.Start(new ProcessStartInfo(apiUrl) { UseShellExecute = true });
                })
                .Build();

        private static string GetSetting(string key)=>
            new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build().GetSection(key).Value;

    }
}