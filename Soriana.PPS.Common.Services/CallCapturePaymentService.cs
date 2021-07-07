using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Soriana.PPS.Common.Configuration;
using Soriana.PPS.Common.Constants;
using Soriana.PPS.Common.DTO.Common;
using Soriana.PPS.Common.DTO.Cybersource.Payments;
using Soriana.PPS.Common.DTO.Salesforce;
using Soriana.PPS.Common.Enums;
using Soriana.PPS.Common.Helpers;
using Soriana.PPS.Common.HttpClient;
using Soriana.PPS.Common.Services.Constants;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Soriana.PPS.Common.Services
{
    public sealed class CallCapturePaymentService : ICallCapturePaymentService
    {
        #region Private Fields
        private readonly ILogger<CallCapturePaymentService> _Logger;
        private readonly IHttpClientService _HttpClientService;
        private readonly IGetMerchantDefinedDataService _MerchantDefinedDataService;
        private readonly ISavePaymentOrderService _PaymentOrderService;
        private readonly IConfiguration _Configuration;
        private readonly IMapper _Mapper;
        private readonly HttpClientListOptions _HttpClientListOptions;
        private readonly MerchantDefinedDataOptions _MerchantDefinedDataOptions;
        #endregion
        #region Constructors
        public CallCapturePaymentService(IHttpClientService httpClientService,
                                            IGetMerchantDefinedDataService merchantDefinedDataService,
                                            ISavePaymentOrderService paymentOrderService,
                                            IOptions<HttpClientListOptions> httpClientListOptions,
                                            IOptions<MerchantDefinedDataOptions> merchantDefinedDataOptions,
                                            IConfiguration configuration,
                                            IMapper mapper,
                                            ILogger<CallCapturePaymentService> logger)
        {
            _HttpClientService = httpClientService;
            _MerchantDefinedDataService = merchantDefinedDataService;
            _PaymentOrderService = paymentOrderService;
            _HttpClientListOptions = httpClientListOptions.Value;
            _MerchantDefinedDataOptions = merchantDefinedDataOptions.Value;
            _Logger = logger;
            _Mapper = mapper;
            _Configuration = configuration;
        }
        #endregion
        #region Private Methods
        private bool TryGetCapturePaymentResponseValid(object capturesPaymentsResponse, out string captureStatus, out string reasonCodes)
        {
            captureStatus = reasonCodes = string.Empty;
            if (capturesPaymentsResponse is CapturesPayments201Response)
            {
                CapturesPayments201Response capturesPayments201Response = capturesPaymentsResponse as CapturesPayments201Response;
                captureStatus = reasonCodes = capturesPayments201Response.Status;
                if (capturesPayments201Response.Status == CapturePaymentStatus201Enum.PENDING.ToString())
                    return true;
            }
            else
            {
                captureStatus = Status502Enum.SERVER_ERROR.ToString();
                reasonCodes = Status502ReasonCodesEnum.SYSTEM_ERROR.ToString();
            }
            return false;
        }
        #endregion
        #region Public Methods
        public async Task<TransactionResponse> CapturePaymentAsync(PaymentOrderProcessRequest request, PaymentOrderProcessRequest originalRequest)
        {
            byte retryNumber = 0;
            byte retryNumberConfiguration = Convert.ToByte(_Configuration[ServicesCommonConstants.CONFIGURATION_KEY_RETRY_BY_TECHNICAL_ISSUES]);
            bool wasThereTechnicalException;
            object result = null;
            string descriptionDetail;
            string serviceInterface = ServicesEnum.CapturePayment.ToString();
            request.MerchantDefinedData = await _MerchantDefinedDataService.GetMerchantDefinedData(request, originalRequest, serviceInterface);
            BusinessResponse businessResponse = null;
            do
            {
                wasThereTechnicalException = false;
                try
                {
                    string jSONRequest = JsonConvert.SerializeObject(request);
                    ObjectResult capturePaymentResult = await _HttpClientService.PostAsync(jSONRequest, serviceInterface) as ObjectResult;
                    if (capturePaymentResult is OkObjectResult)
                    {
                        result = await ActionResultHelper.GetResponseType<CapturesPayments201Response>(capturePaymentResult.Value as MemoryStream);
                    }
                    else if (capturePaymentResult is BadRequestObjectResult)
                    {
                        businessResponse = await ActionResultHelper.GetResponseType<BusinessResponse>(capturePaymentResult.Value as MemoryStream);
                        break;
                    }
                    else
                    {
                        descriptionDetail = string.Concat(ServicesCommonConstants.PAYMENT_ORDER_PROCESS_SERVICE_NAME, CharactersConstants.ESPACE_CHAR, CharactersConstants.HYPHEN_CHAR, CharactersConstants.ESPACE_CHAR, string.Format(ServicesCommonConstants.PAYMENT_ORDER_PROCESS_UNSUPPORTED_RESPONSE_SERVICE, serviceInterface));
                        businessResponse = new BusinessResponse() { StatusCode = (int)HttpStatusCode.InternalServerError, Description = HttpStatusCode.InternalServerError.ToString(), DescriptionDetail = descriptionDetail, ContentRequest = request };
                        break;
                    }
                    TransactionResponse transactionStatus = new TransactionResponse() { IsValid = TryGetCapturePaymentResponseValid(result, out string decisionStatus, out string reasonCodes), ReasonCodes = reasonCodes, Status = decisionStatus, ResponseObject = result };
                    return transactionStatus;
                }
                catch (Exception ex)
                {
                    _Logger.LogError(ex, string.Format(ServicesCommonConstants.PAYMENT_ORDER_PROCESS_RETRYING_ISSUES_OVER_CALLING_TO, serviceInterface));
                    wasThereTechnicalException = true;
                    retryNumber += 1;
                }
            } while (wasThereTechnicalException && retryNumber <= retryNumberConfiguration);
            if (retryNumber > retryNumberConfiguration)
            {
                descriptionDetail = string.Concat(ServicesCommonConstants.PAYMENT_ORDER_PROCESS_SERVICE_NAME, CharactersConstants.ESPACE_CHAR, CharactersConstants.HYPHEN_CHAR, CharactersConstants.ESPACE_CHAR, string.Format(ServicesCommonConstants.PAYMENT_ORDER_PROCESS_UNAVAILABLE_SERVICE, serviceInterface));
                throw ThrowBusinessExceptionHelper.GetThrowException(request, businessResponse, descriptionDetail, serviceInterface, (int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError.ToString());
            }
            if (businessResponse == null)
            {
                descriptionDetail = string.Concat(ServicesCommonConstants.PAYMENT_ORDER_PROCESS_SERVICE_NAME, CharactersConstants.ESPACE_CHAR, CharactersConstants.HYPHEN_CHAR, CharactersConstants.ESPACE_CHAR, string.Format(ServicesCommonConstants.PAYMENT_ORDER_PROCESS_UNSUPPORTED_RESPONSE_SERVICE, serviceInterface));
                throw ThrowBusinessExceptionHelper.GetThrowException(request, businessResponse, descriptionDetail, serviceInterface, (int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError.ToString());
            }
            throw new BusinessException(businessResponse) { ServiceInterface = serviceInterface };
        }
        #endregion
    }
}
