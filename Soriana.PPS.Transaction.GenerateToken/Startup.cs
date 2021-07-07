using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Soriana.PPS.Common.Configuration;
using Soriana.PPS.Common.Extensions;
using Soriana.PPS.Security.GenerateToken.Services;

[assembly: FunctionsStartup(typeof(Soriana.PPS.Security.GenerateToken.Startup))]
namespace Soriana.PPS.Security.GenerateToken
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
            builder.Services.AddOptions<JWTCardinalOptions>().Configure<IConfiguration>((settings, configuration) =>
            {
                configuration.GetSection(JWTCardinalOptions.JWT_CARDINAL_OPTION_CONFIG_SECTION).Bind(settings);
            });
            //SeriLog Injection
            builder.Services.AddSeriLogConfiguration(configuration);
            //Business Service Injection
            builder.Services.AddTransient<IGenerateTokenService, GenerateTokenService>();
        }
        #endregion
    }
}
