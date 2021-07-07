using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;
using Soriana.PPS.Common.Configuration;
using Soriana.PPS.Common.Constants;

namespace Soriana.PPS.Common.Extensions
{
    public static class SeriLogConfigurationServiceExtensions
    {
        #region Public Methods
        public static void AddSeriLogConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            SeriLogOptions seriLogOptions = new SeriLogOptions();
            configuration.GetSection(SeriLogOptions.SERILOG_OPTIONS_CONFIG_SECTION).Bind(seriLogOptions);
            string connectionString = configuration[FunctionAppConstants.FUNCTION_APPINSIGHTS_INSTRUMENTATIONKEY];
            ILogger seriLogger = (string.IsNullOrEmpty(seriLogOptions.LogFilePath)) ? new LoggerConfiguration()
                .WriteTo.ApplicationInsights(connectionString, TelemetryConverter.Traces, LogEventLevel.Verbose)
                .CreateLogger() : new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File(seriLogOptions.LogFilePath, rollingInterval: RollingInterval.Hour)
                .CreateLogger();
            services.AddLogging(log => log.AddSerilog(seriLogger));
        }
        #endregion
    }
}
