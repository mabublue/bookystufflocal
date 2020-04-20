using System;
using System.IO;
using bookystufflocal.domain.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace bookystufflocal
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var configurationManager = ConfigurationManager.CreateForWebAndService(Directory.GetCurrentDirectory(), ConfigurationManager.EnvironmentName);

            Log.Logger = SerilogFactory.BuildLoggerConfiguration(configurationManager, "web")
                .Enrich.WithProperty("MachineName", Environment.MachineName)
                .CreateLogger();

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        .UseSerilog()
                        .UseContentRoot(Directory.GetCurrentDirectory())
                        .UseStartup<Startup>();
                });
    }
}
