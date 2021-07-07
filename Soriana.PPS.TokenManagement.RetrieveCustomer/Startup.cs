using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Soriana.PPS.Common.Configuration;
using Soriana.PPS.Common.DTO.Common;
using Soriana.PPS.Common.DTO.Cybersource.TokenManagement;
using Soriana.PPS.Common.Extensions;
using Soriana.PPS.TokenManagement.RetrieveCustomer.Services;

[assembly: FunctionsStartup(typeof(Soriana.PPS.TokenManagement.RetrieveCustomer.Startup))]
namespace Soriana.PPS.TokenManagement.RetrieveCustomer
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
            builder.Services.Configure<IConfiguration>(configuration);
            CybersourceOptions cybersourceOptions = new CybersourceOptions();
            configuration.GetSection(CybersourceOptions.CYBERSOURCE_OPTIONS_CONFIGURATION).Bind(cybersourceOptions);
            //SeriLog Injection
            builder.Services.AddSeriLogConfiguration(configuration);
            //Business Service Injection
            builder.Services.AddSingleton<ConfigurationAPI>(init =>
            {
                return new ConfigurationAPI(cybersourceOptions.ToDictionary<string, string>());
            });
            builder.Services.AddScoped<ICustomerAPI, CustomerAPI>();
            builder.Services.AddTransient<IRetrieveCustomerService, RetrieveCustomerService>();
        }
        #endregion
    }
}
