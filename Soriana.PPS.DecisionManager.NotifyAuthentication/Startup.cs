using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Soriana.PPS.Common.Extensions;
using Soriana.PPS.DecisionManager.Common.Compatibility.Configuration;
using Soriana.PPS.DecisionManager.Common.Compatibility.Services;
using Soriana.PPS.DecisionManager.NotifyAuthentication.Services;

[assembly: FunctionsStartup(typeof(Soriana.PPS.DecisionManager.NotifyAuthentication.Startup))]
namespace Soriana.PPS.DecisionManager.NotifyAuthentication
{
    public class Startup : FunctionsStartup
    {
        #region Constructors
        public Startup()
        { }
        #endregion
        #region Overrides Methods
        public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
        {
            base.ConfigureAppConfiguration(builder);
        }
        public override void Configure(IFunctionsHostBuilder builder)
        {
            //Formatter Injection
            builder.Services.AddMvcCore().AddNewtonsoftJson(options => options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore);
            //Configuration Injection
            IConfiguration configuration = builder.GetContext().Configuration;
            builder.Services.AddOptions<CybersourceOptions>().Configure<IConfiguration>((settings, configuration) =>
            {
                configuration.GetSection(CybersourceOptions.CYBERSOURCE_OPTIONS_CONFIGURATION).Bind(settings);
            });
            //SeriLog Injection
            builder.Services.AddSeriLogConfiguration(configuration);
            //Business Service Injection
            builder.Services.AddScoped<INotifyAuthenticationSOAPService, NotifyAuthenticationSOAPService>();
            builder.Services.AddScoped<INotifyAuthenticationService, NotifyAuthenticationService>();
        }
        #endregion
    }
}
