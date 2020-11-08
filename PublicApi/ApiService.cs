﻿using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace PublicApi
{
    public class ApiService
    {
        private IHost _host;

        public async Task StartAsync(string[] args)
        {
            _host = await Host.CreateDefaultBuilder(args).UseContentRoot(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
                .ConfigureLogging(logging =>
                {
                    logging.AddConsole();
                    logging.AddDebug();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .StartAsync();
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