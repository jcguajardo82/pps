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
    public sealed class CallEnrollmentService : ICallEnrollmentService
    {
        #region Private Fields
        private readonly ILogger<CallEnrollmentService> _Logger;
        private readonly IHttpClientService _HttpClientService;
        private readonly IGetMerchantDefinedDataService _MerchantDefinedDataService;
        private readonly IConfiguration _Configuration;
        #endregion
        #region Constructors
        public CallEnrollmentService(IHttpClientService httpClientService,
                                        IGetMerchantDefinedDataService merchantDefinedDataService,
                                        IConfiguration configuration,
                                        ILogger<CallEnrollmentService> logger)
        {
            _HttpClientService = httpClientService;
            _MerchantDefinedDataService = merchantDefinedDataService;
            _Logger = logger;
            _Configuration = configuration;
        }
        #endregion
        #region Private Methods
        private bool TryGetEnrollmentResponseValid(object enrollmentResponse, out string enrollmentStatus, out string reasonCodes)
        {
            enrollmentStatus = reasonCodes = string.Empty;
            if (enrollmentResponse is Enrollment201Response)
            {
                Enrollment201Response enrollment201Response = enrollmentResponse as Enrollment201Response;
                enrollmentStatus = enrollment201Response.Status;
                reasonCodes = (enrollment201Response.ErrorInformation != null) ? enrollment201Response.ErrorInformation.Reason : string.Empty;
                if (enrollment201Response.Status == CheckPayerAuthEnrollment201Enum.AUTHENTICATION_SUCCESSFUL.ToString() ||
                    enrollment201Response.Status == CheckPayerAuthEnrollment201Enum.PENDING_AUTHENTICATION.ToString())
                    return true;
            }
            else
            {
                enrollmentStatus = Status502Enum.SERVER_ERROR.ToString();
                reasonCodes = Status502ReasonCodesEnum.SYSTEM_ERROR.ToString();
            }
            return false;
        }
        #endregion
        #region Public Methods
        public async Task<TransactionResponse> EnrollmentAsync(PaymentOrderProcessRequest request, PaymentOrderProcessRequest originalRequest)
        {
            byte retryNumber = 0;
            byte retryNumberConfiguration = Convert.ToByte(_Configuration[ServicesCommonConstants.CONFIGURATION_KEY_RETRY_BY_TECHNICAL_ISSUES]);
            bool wasThereTechnicalException;
            object result = null;
            string descriptionDetail;
            string serviceInterface = ServicesEnum.CheckPayerAuthEnrollment.ToString();
            request.MerchantDefinedData = await _MerchantDefinedDataService.GetMerchantDefinedData(request, originalRequest, PaymentOrderProcessEnum.Enrollment.ToString());
            BusinessResponse businessResponse = null;
            do
            {
                wasThereTechnicalException = false;
                try
                {
                    string jSONRequest = JsonConvert.SerializeObject(request);
                    ObjectResult enrollmentResult = await _HttpClientService.PostAsync(jSONRequest, PaymentOrderProcessEnum.Enrollment.ToString()) as ObjectResult;
                    if (enrollmentResult is OkObjectResult)
                    {
                        result = await ActionResultHelper.GetResponseType<Enrollment201Response>(enrollmentResult.Value as MemoryStream);
                    }
                    else if (enrollmentResult is BadRequestObjectResult)
                    {
                        businessResponse = await ActionResultHelper.GetResponseType<BusinessResponse>(enrollmentResult.Value as MemoryStream);
                        break;
                    }
                    else
                    {
                        descriptionDetail = string.Concat(ServicesCommonConstants.PAYMENT_ORDER_PROCESS_SERVICE_NAME, CharactersConstants.ESPACE_CHAR, CharactersConstants.HYPHEN_CHAR, CharactersConstants.ESPACE_CHAR, string.Format(ServicesCommonConstants.PAYMENT_ORDER_PROCESS_UNSUPPORTED_RESPONSE_SERVICE, serviceInterface));
                        businessResponse = new BusinessResponse() { StatusCode = (int)HttpStatusCode.InternalServerError, Description = HttpStatusCode.InternalServerError.ToString(), DescriptionDetail = descriptionDetail, ContentRequest = request };
                        break;
                    }
                    TransactionResponse transactionStatus = new TransactionResponse() { IsValid = TryGetEnrollmentResponseValid(result, out string decisionStatus, out string reasonCodes), ReasonCodes = reasonCodes, Status = decisionStatus, ResponseObject = result };
                    return transactionStatus;
                }
                catch (Exception ex)
                {
                    _Logger.LogError(ex, string.Format(ServicesCommonConstants.PAYMENT_ORDER_PROCESS_RETRYING_ISSUES_OVER_CALLING_TO, PaymentOrderProcessEnum.Enrollment.ToString()));
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
