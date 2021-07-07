using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Soriana.PPS.Common.Configuration;
using Soriana.PPS.Common.DTO.Common;
using Soriana.PPS.Common.DTO.Cybersource.PayerAuthentication;
using Soriana.PPS.Common.Extensions;
using Soriana.PPS.Common.Mapping.AutoMapper.Converters;
using Soriana.PPS.PayerAuth.ValidateAuthentication.Services;

[assembly: FunctionsStartup(typeof(Soriana.PPS.PayerAuth.ValidateAuthentication.Startup))]
namespace Soriana.PPS.PayerAuth.ValidateAuthentication
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
            builder.Services.AddAutoMapper(typeof(Startup),
                                            typeof(ValidateAuthenticationRiskAuthenticationSetupsClientReferenceInformationTypeConverter),
                                            typeof(ValidateAuthenticationPaymentInformationTypeConverter),
                                            typeof(ValidateAuthenticationOrderInformationTypeConverter),
                                            typeof(ValidateAuthenticationRiskAuthenticationResultsConsumerAuthenticationInformationTypeConverter));
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
                return new ConfigurationAPI(cybersourceOptions.GetCybersourceConfiguration());
            });
            builder.Services.AddScoped<IPayerAuthenticationAPI, PayerAuthenticationAPI>();
            builder.Services.AddTransient<IValidateAuthenticationService, ValidateAuthenticationService>();
        }
        #endregion
    }
}
