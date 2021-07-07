using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Soriana.PPS.Common.Configuration;
using Soriana.PPS.Common.Constants;
using Soriana.PPS.Common.DTO.Common;
using Soriana.PPS.Common.DTO.Cybersource.DecisionManager;
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
    public sealed class CallCreateDecisionService : ICallCreateDecisionService
    {
        #region Private Fields
        private readonly ILogger<CallCreateDecisionService> _Logger;
        private readonly IHttpClientService _HttpClientService;
        private readonly IGetMerchantDefinedDataService _MerchantDefinedDataService;
        private readonly ISavePaymentOrderService _PaymentOrderService;
        private readonly IConfiguration _Configuration;
        private readonly IMapper _Mapper;
        private readonly HttpClientListOptions _HttpClientListOptions;
        private readonly MerchantDefinedDataOptions _MerchantDefinedDataOptions;
        #endregion
        #region Constructors
        public CallCreateDecisionService(IHttpClientService httpClientService,
                                            IGetMerchantDefinedDataService merchantDefinedDataService,
                                            ISavePaymentOrderService paymentOrderService,
                                            IOptions<HttpClientListOptions> httpClientListOptions,
                                            IOptions<MerchantDefinedDataOptions> merchantDefinedDataOptions,
                                            IConfiguration configuration,
                                            IMapper mapper,
                                            ILogger<CallCreateDecisionService> logger)
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
        private bool TryGetDecisionStatusValid(object decisionManagerResponse, out string decisionStatus, out string reasonCodes)
        {
            decisionStatus = reasonCodes = string.Empty;
            if (decisionManagerResponse is DecisionManager201Response)
            {
                DecisionManager201Response decisionManager201 = decisionManagerResponse as DecisionManager201Response;
                decisionStatus = decisionManager201.Status;
                reasonCodes = (decisionManager201.ErrorInformation != null) ? decisionManager201.ErrorInformation.Reason : string.Empty;
                if (decisionManager201.Status == CreateDecisionStatus201Enum.ACCEPTED.ToString() ||
                    decisionManager201.Status == CreateDecisionStatus201Enum.PENDING_REVIEW.ToString())
                    return true;
            }
            else
            {
                decisionStatus = Status502Enum.SERVER_ERROR.ToString();
                reasonCodes = Status502ReasonCodesEnum.SYSTEM_ERROR.ToString();
            }
            return false;
        }
        #endregion
        #region Public Methods
        public async Task<TransactionResponse> CreateDecisionAsync(PaymentOrderProcessRequest request, PaymentOrderProcessRequest originalRequest)
        {
            byte retryNumber = 0;
            byte retryNumberConfiguration = Convert.ToByte(_Configuration[ServicesCommonConstants.CONFIGURATION_KEY_RETRY_BY_TECHNICAL_ISSUES]);
            bool wasThereTechnicalException;
            string descriptionDetail;
            string serviceInterface = ServicesEnum.CreateDecisionManager.ToString();
            object result = null;
            request.MerchantDefinedData = await _MerchantDefinedDataService.GetMerchantDefinedData(request, originalRequest, PaymentOrderProcessEnum.CreateDecision.ToString());
            BusinessResponse businessResponse = null;
            do
            {
                wasThereTechnicalException = false;
                try
                {
                    string jSONRequest = JsonConvert.SerializeObject(request);
                    ObjectResult createDecisionResult = await _HttpClientService.PostAsync(jSONRequest, PaymentOrderProcessEnum.CreateDecision.ToString()) as ObjectResult;
                    if (createDecisionResult is OkObjectResult)
                    {
                        result = await ActionResultHelper.GetResponseType<DecisionManager201Response>(createDecisionResult.Value as MemoryStream);
                    }
                    else if (createDecisionResult is BadRequestObjectResult)
                    {
                        businessResponse = await ActionResultHelper.GetResponseType<BusinessResponse>(createDecisionResult.Value as MemoryStream);
                        break;
                    }
                    else
                    {
                        descriptionDetail = string.Concat(ServicesCommonConstants.PAYMENT_ORDER_PROCESS_SERVICE_NAME, CharactersConstants.ESPACE_CHAR, CharactersConstants.HYPHEN_CHAR, CharactersConstants.ESPACE_CHAR, string.Format(ServicesCommonConstants.PAYMENT_ORDER_PROCESS_UNSUPPORTED_RESPONSE_SERVICE, serviceInterface));
                        businessResponse = new BusinessResponse() { StatusCode = (int)HttpStatusCode.InternalServerError, Description = HttpStatusCode.InternalServerError.ToString(), DescriptionDetail = descriptionDetail, ContentRequest = request };
                        break;
                    }
                    TransactionResponse transactionStatus = new TransactionResponse() { IsValid = TryGetDecisionStatusValid(result, out string decisionStatus, out string reasonCodes), ReasonCodes = reasonCodes, Status = decisionStatus, ResponseObject = result };
                    return transactionStatus;
                }
                catch (Exception ex)
                {
                    _Logger.LogError(ex, string.Format(ServicesCommonConstants.PAYMENT_ORDER_PROCESS_RETRYING_ISSUES_OVER_CALLING_TO, PaymentOrderProcessEnum.CreateDecision.ToString()));
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
