using AutoMapper;
using CyberSource.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Soriana.PPS.Common.Constants;
using Soriana.PPS.Common.DTO.Common;
using Soriana.PPS.Common.DTO.Cybersource.PayerAuthentication;
using Soriana.PPS.Common.DTO.Salesforce;
using Soriana.PPS.PayerAuth.ValidateAuthentication.Constants;
using Soriana.PPS.PayerAuth.ValidateAuthentication.Services;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Soriana.PPS.PayerAuth.ValidateAuthentication
{
    public class ValidateAuthenticationFunction
    {
        #region Private Fields
        private readonly IValidateAuthenticationService _ValidateAuthenticationService;
        private readonly ILogger<ValidateAuthenticationFunction> _Logger;
        private readonly IMapper _Mapper;
        #endregion
        #region Constructors
        public ValidateAuthenticationFunction(IValidateAuthenticationService validateAuthenticationService,
                                                IMapper mapper,
                                                ILogger<ValidateAuthenticationFunction> logger)
        {
            _ValidateAuthenticationService = validateAuthenticationService;
            _Mapper = mapper;
            _Logger = logger;
        }
        #endregion
        #region Public Methods
        [FunctionName(ValidateAuthenticationConstants.VALIDATE_AUTHENTICATION_FUNCTION_NAME)]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest request)
        {
            ValidateAuthenticationRequest validateAuthenticationRequest = null;
            try
            {
                _Logger.LogInformation(string.Format(FunctionAppConstants.FUNCTION_EXECUTING_MESSAGE, ValidateAuthenticationConstants.VALIDATE_AUTHENTICATION_FUNCTION_NAME));
                if (!request.Body.CanSeek)
                    throw new Exception(JsonConvert.SerializeObject(new BusinessResponse() { StatusCode = (int)HttpStatusCode.BadRequest, Description = HttpStatusCode.BadRequest.ToString(), DescriptionDetail = ValidateAuthenticationConstants.VALIDATE_AUTHENTICATION_FUNCTION_NAME, ContentRequest = null }));
                request.Body.Position = 0;
                string jsonPaymentOrderProcessRequest = await new StreamReader(request.Body).ReadToEndAsync();
                PaymentOrderProcessRequest paymentOrderProcessRequest = JsonConvert.DeserializeObject<PaymentOrderProcessRequest>(jsonPaymentOrderProcessRequest);
                validateAuthenticationRequest = _Mapper.Map<ValidateAuthenticationRequest>(paymentOrderProcessRequest);
                ValidateAuthentication201Response validateAuthenticationResponse = await _ValidateAuthenticationService.ValidateAuthentication(validateAuthenticationRequest);
                return new OkObjectResult(validateAuthenticationResponse);
            }
            catch (BusinessException ex)
            {
                _Logger.LogError(ex, ValidateAuthenticationConstants.VALIDATE_AUTHENTICATION_FUNCTION_NAME);
                return new BadRequestObjectResult(ex.Message);
            }
            catch (ApiException ex)
            {
                _Logger.LogError(ex, ValidateAuthenticationConstants.VALIDATE_AUTHENTICATION_FUNCTION_NAME);
                string description = string.Concat(ex.ErrorCode.ToString(), CharactersConstants.ESPACE_CHAR, CharactersConstants.HYPHEN_CHAR, CharactersConstants.ESPACE_CHAR, ValidateAuthenticationConstants.VALIDATE_AUTHENTICATION_FUNCTION_NAME);
                return new BadRequestObjectResult(new BusinessResponse()
                {
                    StatusCode = ex.ErrorCode,
                    Description = description,
                    DescriptionDetail = ex.Message,
                    ContentRequest = JsonConvert.SerializeObject(validateAuthenticationRequest),
                    ContentResponse = JsonConvert.SerializeObject(ex)
                });
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, ValidateAuthenticationConstants.VALIDATE_AUTHENTICATION_FUNCTION_NAME);
                return new BadRequestObjectResult(new BusinessResponse()
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Description = string.Concat(HttpStatusCode.InternalServerError.ToString(), CharactersConstants.ESPACE_CHAR, CharactersConstants.HYPHEN_CHAR, CharactersConstants.ESPACE_CHAR, ValidateAuthenticationConstants.VALIDATE_AUTHENTICATION_FUNCTION_NAME),
                    DescriptionDetail = ex.Message,
                    ContentRequest = JsonConvert.SerializeObject(validateAuthenticationRequest),
                    ContentResponse = JsonConvert.SerializeObject(ex)
                });
            }
        }
        #endregion
    }
}