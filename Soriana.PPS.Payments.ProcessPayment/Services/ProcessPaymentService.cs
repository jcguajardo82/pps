using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Soriana.PPS.Common.Configuration;
using Soriana.PPS.Common.Constants;
using Soriana.PPS.Common.DTO.Common;
using Soriana.PPS.Common.DTO.Cybersource.Payments;
using Soriana.PPS.Common.Enums;
using Soriana.PPS.Common.Services;
using Soriana.PPS.Payments.ProcessPayment.Constants;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Soriana.PPS.Payments.ProcessPayment.Services
{
    public class ProcessPaymentService : ServiceBase, IProcessPaymentService
    {
        #region Private Fields
        private readonly ILogger<ProcessPaymentService> _Logger;
        private IPaymentsAPI _PaymentsAPI;
        private readonly IMapper _Mapper;
        private readonly CybersourceListOptions _Options;
        #endregion
        #region Constructors
        public ProcessPaymentService(IPaymentsAPI paymentsAPI,
                                        IMapper mapper,
                                        IOptions<CybersourceListOptions> options,
                                        ILogger<ProcessPaymentService> logger)
        {
            _PaymentsAPI = paymentsAPI;
            _Mapper = mapper;
            _Options = options.Value;
            _Logger = logger;
        }
        #endregion
        #region Private Method
        private void SetPaymentsAPIInstanceByAffiliationType(PaymentsRequest paymentRequest)
        {
            if (paymentRequest.IsRetrying)
            {
                CybersourceOptions cybersourceOptions = _Options.CybersourceOptions.Where(c => c.AffiliationType.ToLower() == paymentRequest.AffiliationType.ToString().ToLower()).FirstOrDefault();
                if (cybersourceOptions != null)
                    _PaymentsAPI = new PaymentsAPI(new ConfigurationAPI(cybersourceOptions.GetCybersourceConfiguration()));
            }
        }
        #endregion
        #region Public Methods
        public async Task<Payments201Response> ProcessPayment(PaymentsRequest paymentRequest)
        {
            ValidateRequest(paymentRequest);
            SetPaymentsAPIInstanceByAffiliationType(paymentRequest);
            _Logger.LogInformation(string.Format(WebAPIConstants.WEB_API_EXECUTE_MESSAGE, ServicesEnum.ProcessPayment.ToString()));
            _Logger.LogInformation(string.Format(WebAPIConstants.WEB_API_CALLING_WITH_DATA_REQUEST_MESSAGE, ServicesEnum.ProcessPayment, JsonConvert.SerializeObject(paymentRequest)));
            Payments201Response paymentsResponse = _Mapper.Map<Payments201Response>(await _PaymentsAPI.CreatePaymentAsync(paymentRequest));
            _Logger.LogInformation(string.Format(WebAPIConstants.WEB_API_EXECUTED_MESSAGE, ServicesEnum.ProcessPayment.ToString()));
            ValidateResponse(paymentsResponse, paymentRequest);
            return paymentsResponse;
        }

        protected override void ValidateRequest(object request)
        {
            PaymentsRequest paymentsRequest = request as PaymentsRequest;
            if (paymentsRequest == null ||
                paymentsRequest.ClientReferenceInformation == null ||
                string.IsNullOrEmpty(paymentsRequest.ClientReferenceInformation.Code) ||
                paymentsRequest.PaymentInformation == null ||
                paymentsRequest.PaymentInformation.Customer == null ||
                string.IsNullOrEmpty(paymentsRequest.PaymentInformation.Customer.CustomerId) ||
                paymentsRequest.OrderInformation == null ||
                paymentsRequest.OrderInformation.AmountDetails == null ||
                string.IsNullOrEmpty(paymentsRequest.OrderInformation.AmountDetails.Currency) ||
                paymentsRequest.OrderInformation.LineItems == null ||
                paymentsRequest.OrderInformation.LineItems.Count == 0 ||
                paymentsRequest.DeviceInformation == null ||
                string.IsNullOrEmpty(paymentsRequest.DeviceInformation.FingerprintSessionId) ||
                ///TODO: Validar como enviar la estructura de MDD
                paymentsRequest.MerchantDefinedInformation == null ||
                !paymentsRequest.MerchantDefinedInformation.Any())
                throw new BusinessException(new BusinessResponse()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Description = HttpStatusCode.BadRequest.ToString(),
                    DescriptionDetail = ProcessPaymentConstants.PROCESS_PAYMENT_SERVICE_NAME,
                    ContentRequest = paymentsRequest
                })
                {
                    ServiceInterface = ServicesEnum.ProcessPayment.ToString()
                };
        }

        protected override void ValidateResponse(object response, object request = null)
        {
            Payments201Response paymentsResponse = response as Payments201Response;
            if (paymentsResponse == null ||
                !Enum.GetNames(typeof(PaymentStatus201Enum)).Any(pS => pS == paymentsResponse.Status))
            {
                throw new BusinessException(new BusinessResponse()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Description = HttpStatusCode.BadRequest.ToString(),
                    DescriptionDetail = ProcessPaymentConstants.PROCESS_PAYMENT_SERVICE_NAME,
                    ContentRequest = request,
                    ContentResponse = paymentsResponse
                })
                {
                    ServiceInterface = ServicesEnum.ProcessPayment.ToString(),
                    ExecutedInnerService = ServicesEnum.ProcessPayment.ToString()
                };
            }
        }
        #endregion
    }
}
