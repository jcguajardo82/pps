using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Soriana.PPS.Common.Configuration;
using Soriana.PPS.Common.Constants;using Soriana.PPS.Common.DTO.Common;
using Soriana.PPS.Common.DTO.Cybersource.PayerAuthentication;
using Soriana.PPS.Common.Enums;using Soriana.PPS.Common.Extensions;
using Soriana.PPS.Common.Mapping.AutoMapper.Converters;
using Soriana.PPS.PayerAuth.Enrollment.Services;
using System.Linq;
[assembly: FunctionsStartup(typeof(Soriana.PPS.PayerAuth.Enrollment.Startup))]
namespace Soriana.PPS.PayerAuth.Enrollment
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
                                            typeof(EnrollmentRiskAuthenticationsDeviceInformationTypeConverter),
                                            typeof(EnrollmentRiskAuthenticationSetupsClientReferenceInformationTypeConverter),
                                            typeof(EnrollmentRiskAuthenticationsOrderInformationTypeConverter),
                                            typeof(EnrollmentRiskAuthenticationsPaymentInformationTypeConverter),
                                            typeof(EnrollmentConsumerAuthenticationInformationTypeConverter));
            //Configuration Injection
            IConfiguration configuration = builder.GetContext().Configuration;
            builder.Services.Configure<IConfiguration>(configuration);
            //CybersourceOptions cybersourceOptions = new CybersourceOptions();
            //configuration.GetSection(CybersourceOptions.CYBERSOURCE_OPTIONS_CONFIGURATION).Bind(cybersourceOptions);

            PPS.Common.Configuration.CybersourceOptions cybersourceOptions = new PPS.Common.Configuration.CybersourceOptions();
            string sectionCybersourcePath = string.Concat(CybersourceListOptions.CYBERSOURCE_LIST_OPTIONS_CONFIGURATION, CharactersConstants.COLON_CHAR, PPS.Common.Configuration.CybersourceOptions.CYBERSOURCE_OPTIONS_CONFIGURATION);
            cybersourceOptions = configuration.GetSection(sectionCybersourcePath).Get<PPS.Common.Configuration.CybersourceOptions[]>().Where(o => o.AffiliationType.ToLower() == AffiliationTypeEnum.Default.ToString().ToLower()).FirstOrDefault();
            builder.Services.AddOptions<CybersourceListOptions>().Configure<IConfiguration>((settings, configuration) =>
            {
                //string sectionCybersourcePath = string.Concat(CybersourceListOptions.CYBERSOURCE_LIST_OPTIONS_CONFIGURATION, CharactersConstants.COLON_CHAR, CybersourceOptions.CYBERSOURCE_OPTIONS_CONFIGURATION);
                foreach (PPS.Common.Configuration.CybersourceOptions options in configuration.GetSection(sectionCybersourcePath).Get<PPS.Common.Configuration.CybersourceOptions[]>())
                    settings.CybersourceOptions.Add(options);
            });


            //SeriLog Injection
            builder.Services.AddSeriLogConfiguration(configuration);
            //Business Service Injection
            builder.Services.AddSingleton<ConfigurationAPI>(init =>
            {
                return new ConfigurationAPI(cybersourceOptions.GetCybersourceConfiguration());
            });
            builder.Services.AddScoped<IPayerAuthenticationAPI, PayerAuthenticationAPI>();
            builder.Services.AddTransient<IEnrollmentService, EnrollmentService>();
        }
        #endregion
    }
}
