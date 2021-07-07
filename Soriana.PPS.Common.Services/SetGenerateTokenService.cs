using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Soriana.PPS.Common.Constants;
using Soriana.PPS.Common.DTO.Common;
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
    public sealed class SetGenerateTokenService : ISetGenerateTokenService
    {
        #region Private Fields
        private readonly ILogger<SetGenerateTokenService> _Logger;
        private readonly IHttpClientService _HttpClientService;
        private readonly IConfiguration _Configuration;
        private readonly IMapper _Mapper;
        #endregion
        #region Constructors
        public SetGenerateTokenService(IHttpClientService httpClientService,
                                        IConfiguration configuration,
                                        IMapper mapper,
                                        ILogger<SetGenerateTokenService> logger)
        {
            _HttpClientService = httpClientService;
            _Configuration = configuration;
            _Mapper = mapper;
            _Logger = logger;
        }
        #endregion
        #region Public Methods
        public async Task SetGenerateTokenAsync(PaymentOrderProcessRequest request)
        {
            byte retryNumber = 0;
            byte retryNumberConfiguration = Convert.ToByte(_Configuration[ServicesCommonConstants.CONFIGURATION_KEY_RETRY_BY_TECHNICAL_ISSUES]);
            bool wasThereTechnicalException;
            string descriptionDetail;
            string serviceInterface = ServicesEnum.GenerateToken.ToString();
            BusinessResponse businessResponse = null;
            do
            {
                wasThereTechnicalException = false;
                try
                {
                    JsonWebTokenRequest jsonWebTokenRequest = _Mapper.Map<JsonWebTokenRequest>(request);
                    string jSONRequest = JsonConvert.SerializeObject(jsonWebTokenRequest);
                    ObjectResult generateTokenResult = await _HttpClientService.PostAsync(jSONRequest, PaymentOrderProcessEnum.GenerateToken.ToString()) as ObjectResult;
                    JsonWebTokenResponse jsonWebTokenResponse = null;
                    if (generateTokenResult is OkObjectResult)
                    {
                        jsonWebTokenResponse = await ActionResultHelper.GetResponseType<JsonWebTokenResponse>(generateTokenResult.Value as MemoryStream);
                    }
                    else if (generateTokenResult is BadRequestObjectResult)
                    {
                        businessResponse = await ActionResultHelper.GetResponseType<BusinessResponse>(generateTokenResult.Value as MemoryStream);
                        break;
                    }
                    else
                    {
                        descriptionDetail = string.Concat(ServicesCommonConstants.PAYMENT_ORDER_PROCESS_SERVICE_NAME, CharactersConstants.ESPACE_CHAR, CharactersConstants.HYPHEN_CHAR, CharactersConstants.ESPACE_CHAR, string.Format(ServicesCommonConstants.PAYMENT_ORDER_PROCESS_UNSUPPORTED_RESPONSE_SERVICE, serviceInterface));
                        businessResponse = new BusinessResponse() { StatusCode = (int)HttpStatusCode.InternalServerError, Description = HttpStatusCode.InternalServerError.ToString(), DescriptionDetail = descriptionDetail, ContentRequest = request };
                        break;
                    }
                    if (jsonWebTokenResponse == null)
                    {
                        descriptionDetail = string.Concat(ServicesCommonConstants.PAYMENT_ORDER_PROCESS_SERVICE_NAME, CharactersConstants.ESPACE_CHAR, CharactersConstants.HYPHEN_CHAR, CharactersConstants.ESPACE_CHAR, string.Format(ServicesCommonConstants.PAYMENT_ORDER_PROCESS_UNSUPPORTED_RESPONSE_SERVICE, serviceInterface));
                        businessResponse = new BusinessResponse() { StatusCode = (int)HttpStatusCode.InternalServerError, Description = HttpStatusCode.InternalServerError.ToString(), DescriptionDetail = descriptionDetail, ContentRequest = request };
                        break;
                    }
                    request.InternalCardinalToken = jsonWebTokenResponse.JsonWebToken;
                    return;
                }
                catch (Exception ex)
                {
                    _Logger.LogError(ex, string.Format(ServicesCommonConstants.PAYMENT_ORDER_PROCESS_RETRYING_ISSUES_OVER_CALLING_TO, PaymentOrderProcessEnum.GenerateToken.ToString()));
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
