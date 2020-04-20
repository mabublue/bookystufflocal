using Serilog;
using Serilog.Events;

namespace bookystufflocal.domain.Helpers
{
    public static class SerilogFactory
    {
        public static LoggerConfiguration BuildLoggerConfiguration(ConfigurationManager configurationManager, string application)
        {
            var logGroupName = $"/campaign-agent-system/application-logs/{ConfigurationManager.EnvironmentName.ToLower()}/{application}";
            //var options = new CloudWatchSinkOptions
            //{
            //    LogGroupName = logGroupName,
            //    TextFormatter = new JsonTextFormatter(),
            //    MinimumLogEventLevel = LogEventLevel.Information,
            //    BatchSizeLimit = 100,
            //    QueueSizeLimit = 10000,
            //    Period = TimeSpan.FromSeconds(10),
            //    CreateLogGroup = true,
            //    LogStreamNameProvider = new DefaultLogStreamProvider(),
            //    RetryAttempts = 5
            //};

            //var client = new AmazonCloudWatchLogsClient(RegionEndpoint.APSoutheast2);

            var loggerConfiguration = new LoggerConfiguration()
                                      .Enrich.WithDemystifiedStackTraces()
                                      .ReadFrom.Configuration(configurationManager.Configuration)
                                      .MinimumLevel.Verbose()
                                      .WriteTo.Logger(console => console.ReadFrom.Configuration(configurationManager.Configuration.GetSection("Serilog.Console")))
                                      //.WriteTo.Logger(x => x.WriteTo.AmazonCloudWatch(options, client).Filter.ByIncludingOnly(Matching.FromSource("CampaignAgent")))
                                      //.WriteTo.Sentry(o =>
                                      //{
                                      //    o.MinimumBreadcrumbLevel = LogEventLevel.Debug;
                                      //    o.MinimumEventLevel = LogEventLevel.Warning;
                                      //    o.AttachStacktrace = true;
                                      //    o.SendDefaultPii = true;
                                      //    o.InitializeSdk = true;
                                      //    o.Environment = ConfigurationManager.EnvironmentName.ToLower();
                                      //    o.MaxBreadcrumbs = 100;
                                      //    o.Dsn = new Dsn("https://6fc3cc04e02846558ad4e6b1a6907d0d@sentry.io/1769616");
                                      //    o.BeforeSend = SentryEnricherV2.EnrichSentryEvent;
                                      //    o.Release = ConfigurationManager.GetGlobalVersionNumber();
                                      //    // Other configuration
                                      //})
                                      .Enrich.FromLogContext();


            loggerConfiguration.MinimumLevel.Override("Microsoft", LogEventLevel.Information);

            return loggerConfiguration;
        }
    }
}
