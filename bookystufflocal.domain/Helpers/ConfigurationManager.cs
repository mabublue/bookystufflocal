using System;
using bookystufflocal.domain.DomainLayer.BaseModels;
using Microsoft.Extensions.Configuration;

namespace bookystufflocal.domain.Helpers
{
    public class ConfigurationManager
    {
        public static ConfigurationManager CreateForWebAndService(string rootPath, string environmentName)
        {
            //var awsOptions = new AWSOptions
            //{
            //    Region = RegionEndpoint.APSoutheast2
            //};

            //var builder = BuildBaseConfiguration(rootPath, environmentName).AddSystemsManager($"/cas/{environmentName.ToLower()}", awsOptions);
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(rootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{environmentName.ToLower()}.json", true, true)
                .AddJsonFile($"appsettings.{environmentName.ToLower()}.private.json", true, true)
                .AddEnvironmentVariables();

            return new ConfigurationManager(configBuilder.Build());
        }

        private ConfigurationManager(IConfigurationRoot configuration)
        {
            Configuration = configuration;
        }

        public IConfigurationRoot Configuration { get; }

        public static string EnvironmentName => GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT").ToLower();
        public ConnectionString ConnectionString => new ConnectionString(GetEnvironmentVariable("ConnectionString_BookyStuffLocal"));




        public static bool IsBuildServer() => EnvironmentName == "appveyor";
        public static bool IsDevelopment() => EnvironmentName == "development";
        public static bool IsStaging() => EnvironmentName == "staging";
        public static bool IsProduction() => EnvironmentName == "production";

        private static string GetEnvironmentVariable(string variable)
        {
            var value = Environment.GetEnvironmentVariable(variable);
            if (string.IsNullOrEmpty(value))
            {
                throw new InvalidOperationException($"Unable to start application. You are missing the {variable} environment variable.");
            }
            return value;
        }

    }
}
