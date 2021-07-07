using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Soriana.PPS.Common.Configuration;
using Soriana.PPS.Common.Constants;
using Soriana.PPS.Common.Data;
using Soriana.PPS.Common.Data.Dapper;
using Soriana.PPS.Common.Entities;
using Soriana.PPS.Common.Extensions;
using Soriana.PPS.Common.HttpClient;
using Soriana.PPS.Common.Mapping.AutoMapper.Converters;
using Soriana.PPS.Common.Services;
using Soriana.PPS.DataAccess.Configuration;
using Soriana.PPS.DataAccess.PaymentProcess;
using Soriana.PPS.DataAccess.Repository;
using Soriana.PPS.PaymentOrderProcess.Services;
using System.Data;
using System.Net.Http;

[assembly: FunctionsStartup(typeof(Soriana.PPS.PaymentOrderProcess.Startup))]
namespace Soriana.PPS.PaymentOrderProcess
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
            //AutoMapper Injection
            builder.Services.AddAutoMapper(typeof(Startup),
                                            typeof(PayloadJWTTypeConverter),
                                            typeof(ToDateFromStringConverter),
                                            typeof(ToTimeFromStringConverter));
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
            //HttpClient Injection
            builder.Services.AddHttpClient();
            //SeriLog Injection
            builder.Services.AddSeriLogConfiguration(configuration);
            //HttpClient Service Injection
            builder.Services.AddTransient<IHttpClientService, HttpClientService>(init =>
            {
                IHttpClientFactory httpClientFactory = init.GetRequiredService<IHttpClientFactory>();
                IOptions<HttpClientListOptions> options = init.GetRequiredService<IOptions<HttpClientListOptions>>();
                return new HttpClientService(httpClientFactory, options);
            });
            //DataAccess Service Injection -- Db Connection
            builder.Services.AddScoped<IDbConnection>(o =>
            {
                PaymentProcessorOptions paymentProcessorOptions = new PaymentProcessorOptions();
                configuration.GetSection(PaymentProcessorOptions.PAYMENT_PROCESSOR_OPTIONS_CONFIGURATION).Bind(paymentProcessorOptions);
                return new System.Data.SqlClient.SqlConnection(paymentProcessorOptions.ConnectionString);
            });
            //DataAccess Service Injection -- Unit of Work
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>(o =>
            {
                return new UnitOfWork(o.GetRequiredService<IDbConnection>());
            });
            //DataAccess Service Injection -- Repositories Operations
            builder.Services.AddScoped<IRepositoryRead<ClientHasToken>, RepositoryRead<ClientHasToken>>();
            builder.Services.AddScoped<IRepositoryCreate<ClientHasToken>, RepositoryWrite<ClientHasToken>>();
            builder.Services.AddScoped<IRepositoryCreate<PaymentTransaction>, RepositoryWrite<PaymentTransaction>>();
            builder.Services.AddScoped<IRepositoryRead<PaymentTransaction>, RepositoryRead<PaymentTransaction>>();
            builder.Services.AddScoped<IRepositoryCreate<PaymentTransactionStatus>, RepositoryWrite<PaymentTransactionStatus>>();
            builder.Services.AddScoped<IRepositoryRead<PaymentTransactionStatus>, RepositoryRead<PaymentTransactionStatus>>();
            builder.Services.AddScoped<IRepositoryCreate<PaymentOrder>, RepositoryWrite<PaymentOrder>>();
            builder.Services.AddScoped<IRepositoryRead<PaymentOrder>, RepositoryRead<PaymentOrder>>();
            builder.Services.AddScoped<IRepositoryCreate<PaymentTransactionJsonRequest>, RepositoryWrite<PaymentTransactionJsonRequest>>();
            builder.Services.AddScoped<IRepositoryRead<PaymentTransactionJsonRequest>, RepositoryRead<PaymentTransactionJsonRequest>>();
            builder.Services.AddSingleton<ICreateTableType, CreateTableTypeBase>();
            //DataAccess Service Injection -- Repositories
            builder.Services.AddScoped<IClientHasTokenRepository, ClientHasTokenRepository>();
            builder.Services.AddScoped<IPaymentTransactionRepository, PaymentTransactionRepository>();
            builder.Services.AddScoped<IPaymentTransactionStatusRepository, PaymentTransactionStatusRepository>();
            builder.Services.AddScoped<IItemRepository, ItemRepository>();
            builder.Services.AddScoped<IPaymentOrderRepository, PaymentOrderRepository>();
            builder.Services.AddScoped<IPaymentTransactionJsonRequestRepository, PaymentTransactionJsonRequestRepository>();
            //DataAccess Service Injection -- Context Database
            builder.Services.AddScoped<IPaymentProcessContext, PaymentProcessContext>();
            //Business Service Injection
            builder.Services.AddScoped<ISplitPaymentOrderService, SplitPaymentOrderService>();
            builder.Services.AddScoped<ISequencePaymentOrderTransactionService, SequencePaymentOrderTransactionService>();
            builder.Services.AddScoped<IGetMerchantDefinedDataService, GetMerchantDefinedDataService>();
            builder.Services.AddScoped<ISavePaymentOrderService, SavePaymentOrderService>();
            builder.Services.AddScoped<ICallCreateDecisionService, CallCreateDecisionService>();
            builder.Services.AddScoped<ICallProcessPaymentService, CallProcessPaymentService>();
            builder.Services.AddScoped<ICallCapturePaymentService, CallCapturePaymentService>();
            builder.Services.AddScoped<ICallEnrollmentService, CallEnrollmentService>();
            builder.Services.AddScoped<ICallValidateAuthenticationService, CallValidateAuthenticationService>();
            builder.Services.AddScoped<ICallNotifyAuthenticationService, CallNotifyAuthenticationService>();
            builder.Services.AddScoped<IPaymentOrderProcessService, PaymentOrderProcessService>();
            builder.Services.AddScoped<ISavePaymentTransactionStatusService, SavePaymentTransactionStatusService>();
            builder.Services.AddScoped<ISavePaymentTransactionService, SavePaymentTransactionService>();
            builder.Services.AddScoped<ISetGenerateTokenService, SetGenerateTokenService>();
            builder.Services.AddScoped<ISetTransactionReferenceService, SetTransactionReferenceService>();
            builder.Services.AddScoped<IUpdatePaymentTransactionJsonRequestService, UpdatePaymentTransactionJsonRequestService>();
        }
        #endregion
    }
}
