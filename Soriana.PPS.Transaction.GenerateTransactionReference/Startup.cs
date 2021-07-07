using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Soriana.PPS.Common.Data;
using Soriana.PPS.Common.Data.Dapper;
using Soriana.PPS.Common.Entities;
using Soriana.PPS.Common.Extensions;
using Soriana.PPS.Control.GenerateTransactionReference.Services;
using Soriana.PPS.DataAccess.Configuration;
using Soriana.PPS.DataAccess.PaymentProcess;
using Soriana.PPS.DataAccess.Repository;
using System.Data;
using System.Data.SqlClient;

[assembly: FunctionsStartup(typeof(Soriana.PPS.Control.GenerateTransactionReference.Startup))]
namespace Soriana.PPS.Control.GenerateTransactionReference
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
            //SeriLog Injection
            builder.Services.AddSeriLogConfiguration(configuration);
            //DataAccess Service Injection -- Db Connection
            builder.Services.AddScoped<IDbConnection>(o =>
            {
                PaymentProcessorOptions paymentProcessorOptions = new PaymentProcessorOptions();
                configuration.GetSection(PaymentProcessorOptions.PAYMENT_PROCESSOR_OPTIONS_CONFIGURATION).Bind(paymentProcessorOptions);
                return new SqlConnection(paymentProcessorOptions.ConnectionString);
            });
            //DataAccess Service Injection -- Unit of Work
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>(o =>
            {
                return new UnitOfWork(o.GetRequiredService<IDbConnection>());
            });
            //DataAccess Service Injection -- Repositories Operations
            builder.Services.AddScoped<IRepositoryRead<ClientHasToken>, RepositoryRead<ClientHasToken>>();
            builder.Services.AddScoped<IRepositoryCreate<ClientHasToken>, RepositoryWrite<ClientHasToken>>();
            builder.Services.AddScoped<IRepositoryRead<PaymentTransaction>, RepositoryRead<PaymentTransaction>>();
            builder.Services.AddScoped<IRepositoryCreate<PaymentTransaction>, RepositoryWrite<PaymentTransaction>>();
            builder.Services.AddSingleton<ICreateTableType, CreateTableTypeBase>();
            //DataAccess Service Injection -- Repositories
            builder.Services.AddScoped<IClientHasTokenRepository, ClientHasTokenRepository>();
            builder.Services.AddScoped<IPaymentTransactionRepository, PaymentTransactionRepository>();
            builder.Services.AddScoped<IPaymentProcessContext, PaymentProcessContext>();
            //Business Service Injection
            builder.Services.AddScoped<IGenerateTransactionReferenceService, GenerateTransactionReferenceService>();
        }
        #endregion
    }
}
