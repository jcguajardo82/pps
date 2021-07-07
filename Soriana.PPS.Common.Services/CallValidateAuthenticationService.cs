using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Soriana.PPS.Common.Constants;
using Soriana.PPS.Common.DTO.Common;
using Soriana.PPS.Common.DTO.Cybersource.PayerAuthentication;
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
    public sealed class CallValidateAuthenticationService : ICallValidateAuthenticationService
    {
        #region Private Fields
        private readonly ILogger<CallValidateAuthenticationService> _Logger;
        private readonly IHttpClientService _HttpClientService;
        private readonly IConfiguration _Configuration;
        #endregion
        #region Constructors
        public CallValidateAuthenticationService(IHttpClientService httpClientService,
                                                    IConfiguration configuration,
                                                    ILogger<CallValidateAuthenticationService> logger)
        {
            _HttpClientService = httpClientService;
            _Logger = logger;
            _Configuration = configuration;
        }
        #endregion
        #region Private Methods
        private bool TryGetValidateAuthenticationResponseValid(object validateAuthenticationResponse, out string validateAuthenticationStatus, out string reasonCodes)
        {
            validateAuthenticationStatus = reasonCodes = string.Empty;
            if (validateAuthenticationResponse is ValidateAuthentication201Response)
            {
                ValidateAuthentication201Response validateAuthentication201Response = validateAuthenticationResponse as ValidateAuthentication201Response;
                validateAuthenticationStatus = validateAuthentication201Response.Status;
                reasonCodes = (validateAuthentication201Response.ErrorInformation != null) ? validateAuthentication201Response.ErrorInformation.Reason : string.Empty;
                if (validateAuthentication201Response.Status == ValidateAuthenticationResults201Enum.AUTHENTICATION_SUCCESSFUL.ToString())
                    return true;
            }
            else
            {
                validateAuthenticationStatus = Status502Enum.SERVER_ERROR.ToString();
                reasonCodes = Status502ReasonCodesEnum.SYSTEM_ERROR.ToString();
            }
            return false;
        }
        #endregion
        #region Public Methods
        public async Task<TransactionResponse> ValidateAuthenticationAsync(PaymentOrderProcessRequest request)
        {
            byte retryNumber = 0;
            byte retryNumberConfiguration = Convert.ToByte(_Configuration[ServicesCommonConstants.CONFIGURATION_KEY_RETRY_BY_TECHNICAL_ISSUES]);
            bool wasThereTechnicalException;
            object result = null;
            string descriptionDetail;
            string serviceInterface = ServicesEnum.ValidateAuthenticationResults.ToString();
            BusinessResponse businessResponse = null;
            do
            {
                wasThereTechnicalException = false;
                try
                {
                    string jSONRequest = JsonConvert.SerializeObject(request);
                    ObjectResult validateAuthenticationResult = await _HttpClientService.PostAsync(jSONRequest, PaymentOrderProcessEnum.ValidateAuthentication.ToString()) as ObjectResult;
                    if (validateAuthenticationResult is OkObjectResult)
                    {
                        result = await ActionResultHelper.GetResponseType<ValidateAuthentication201Response>(validateAuthenticationResult.Value as MemoryStream);
                    }
                    else if (validateAuthenticationResult is BadRequestObjectResult)
                    {
                        businessResponse = await ActionResultHelper.GetResponseType<BusinessResponse>(validateAuthenticationResult.Value as MemoryStream);
                        break;
                    }
                    else
                    {
                        descriptionDetail = string.Concat(ServicesCommonConstants.PAYMENT_ORDER_PROCESS_SERVICE_NAME, CharactersConstants.ESPACE_CHAR, CharactersConstants.HYPHEN_CHAR, CharactersConstants.ESPACE_CHAR, string.Format(ServicesCommonConstants.PAYMENT_ORDER_PROCESS_UNSUPPORTED_RESPONSE_SERVICE, serviceInterface));
                        businessResponse = new BusinessResponse() { StatusCode = (int)HttpStatusCode.InternalServerError, Description = HttpStatusCode.InternalServerError.ToString(), DescriptionDetail = descriptionDetail, ContentRequest = request };
                        break;
                    }
                    TransactionResponse transactionStatus = new TransactionResponse() { IsValid = TryGetValidateAuthenticationResponseValid(result, out string decisionStatus, out string reasonCodes), ReasonCodes = reasonCodes, Status = decisionStatus, ResponseObject = result };
                    return transactionStatus;
                }
                catch (Exception ex)
                {
                    _Logger.LogError(ex, string.Format(ServicesCommonConstants.PAYMENT_ORDER_PROCESS_RETRYING_ISSUES_OVER_CALLING_TO, PaymentOrderProcessEnum.ValidateAuthentication.ToString()));
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
