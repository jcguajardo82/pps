using System.Data;
using System.Data.SqlClient;

using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Soriana.PPS.Common.Configuration;
using Soriana.PPS.Common.DTO.Common;
using Soriana.PPS.Common.DTO.Cybersource.Payments;
using Soriana.PPS.Common.Extensions;
using Soriana.PPS.Common.Mapping.AutoMapper.Converters;
using Soriana.PPS.Common.Mapping.AutoMapper.Profiles;
using Soriana.PPS.Payments.CapturePayment.Services;
using Soriana.PPS.DataAccess.PaymentProcess;
using Soriana.PPS.DataAccess.Configuration;
using Soriana.PPS.Common.Data;
using Soriana.PPS.DataAccess.Repository;
using Soriana.PPS.PaymentOrderProcess.ClosureOrderGrocery.Services;
using Soriana.PPS.Common.Data.Dapper;
using Soriana.PPS.Common.Services;
using Soriana.PPS.Common.Constants;
using Soriana.PPS.Common.Entities;
using Soriana.PPS.Common.HttpClient;
using System.Net.Http;
using Microsoft.Extensions.Options;

[assembly: FunctionsStartup(typeof(Soriana.PPS.PaymentOrderProcess.ClosureOrderGrocery.Startup))]
namespace Soriana.PPS.PaymentOrderProcess.ClosureOrderGrocery
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
            //AutoMapper Injection
            builder.Services.AddAutoMapper(typeof(Startup),
                                            typeof(PaymentsClientReferenceTypeConverter),
                                            typeof(PaymentsPaymentInformationTypeConverter),
                                            typeof(PaymentsOrderInformationTypeConverter),
                                            typeof(PaymentsDeviceInformationTypeConverter));

            //HttpClientOptions
            builder.Services.AddOptions<HttpClientListOptions>().Configure<IConfiguration>((settings, configuration) =>
            {
                string sectionPath = string.Concat(HttpClientListOptions.HTTP_CLIENT_LIST_OPTIONS, CharactersConstants.COLON_CHAR, HttpClientOptions.HTTP_CLIENT_OPTIONS);
                foreach (HttpClientOptions options in configuration.GetSection(sectionPath).Get<HttpClientOptions[]>())
                    settings.HttpClientOptions.Add(options);
            });
            builder.Services.AddOptions<MerchantDefinedDataOptions>().Configure<IConfiguration>((settings, configuration) =>
            {
                configuration.GetSection(MerchantDefinedDataOptions.MERCHANT_DEFINED_DATA_OPTIONS_CONFIGURATION).Bind(settings);
            });

            //Configuration Injection
            IConfiguration configuration = builder.GetContext().Configuration;
            builder.Services.Configure<IConfiguration>(configuration);
            CybersourceOptions cybersourceOptions = new CybersourceOptions();
            configuration.GetSection(CybersourceOptions.CYBERSOURCE_OPTIONS_CONFIGURATION).Bind(cybersourceOptions);

            //SeriLog Injection
            builder.Services.AddSeriLogConfiguration(configuration);

            //HttpClient Injection
            builder.Services.AddHttpClient();
            builder.Services.AddTransient<IHttpClientService, HttpClientService>(init =>
            {
                IHttpClientFactory httpClientFactory = init.GetRequiredService<IHttpClientFactory>();
                IOptions<HttpClientListOptions> options = init.GetRequiredService<IOptions<HttpClientListOptions>>();
                return new HttpClientService(httpClientFactory, options);
            });

            //DataAccess Service Injection -- DataBase
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

            //Business Service Injection
            builder.Services.AddSingleton<ConfigurationAPI>(init =>
            {
                return new ConfigurationAPI(cybersourceOptions.GetCybersourceConfiguration());
            });

            builder.Services.AddScoped<ICapturesAPI, CapturesAPI>();
            builder.Services.AddTransient<ICapturePaymentService, CapturePaymentService>();

            //DataAccess Service Injection -- Repositories Operations
            builder.Services.AddScoped<IRepositoryRead<ClientHasToken>, RepositoryRead<ClientHasToken>>();
            builder.Services.AddScoped<IRepositoryCreate<ClientHasToken>, RepositoryWrite<ClientHasToken>>();
            builder.Services.AddSingleton<ICreateTableType, CreateTableTypeBase>();

            //DataAccess Service Injection -- Repositories Operations
            builder.Services.AddScoped<IClosurePaymentRepository, ClosurePaymentRepository>();
            builder.Services.AddScoped<IClientHasTokenRepository, ClientHasTokenRepository>();
            builder.Services.AddScoped<IPaymentTransactionRepository, PaymentTransactionRepository>();

            //DataAccess Service Injection -- Context
            builder.Services.AddScoped<IPaymentProcessContext, PaymentProcessContext>();

            //Business Service Injection -- Service
            builder.Services.AddScoped<IClosureOrderGroceryService, ClosureOrderGroceryService>();
            builder.Services.AddScoped<IGetMerchantDefinedDataService, GetMerchantDefinedDataService>();
            builder.Services.AddScoped<ICallCapturePaymentService, CallCapturePaymentService>();
 

            //Business Service Injection XML
            builder.Services.AddScoped<IResponseXMLService, ResponseXMLService>();
        }
        #endregion
    }
}
