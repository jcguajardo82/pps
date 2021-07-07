using AutoMapper;
using Microsoft.Extensions.Logging;
using Soriana.PPS.Common.Constants;
using Soriana.PPS.Common.DTO.Common;
using Soriana.PPS.Common.DTO.Cybersource.PayerAuthentication;
using Soriana.PPS.Common.Enums;
using Soriana.PPS.Common.Services;
using Soriana.PPS.PayerAuth.ValidateAuthentication.Constants;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Soriana.PPS.PayerAuth.ValidateAuthentication.Services
{
    public class ValidateAuthenticationService : ServiceBase, IValidateAuthenticationService
    {
        #region Private Fields
        private readonly ILogger<ValidateAuthenticationService> _Logger;
        private readonly IPayerAuthenticationAPI _PayerAuthenticationAPI;
        private readonly IMapper _Mapper;
        #endregion
        #region Constructors
        public ValidateAuthenticationService(IPayerAuthenticationAPI payerAuthenticationAPI,
                                                IMapper mapper,
                                                ILogger<ValidateAuthenticationService> logger)
        {
            _Logger = logger;
            _Mapper = mapper;
            _PayerAuthenticationAPI = payerAuthenticationAPI;
        }
        #endregion
        #region Public Methods
        public async Task<ValidateAuthentication201Response> ValidateAuthentication(ValidateAuthenticationRequest validateRequest)
        {
            ValidateRequest(validateRequest);
            _Logger.LogInformation(string.Format(WebAPIConstants.WEB_API_EXECUTE_MESSAGE, ServicesEnum.ValidateAuthenticationResults.ToString()));
            ValidateAuthentication201Response validateAuthenticationResponse = _Mapper.Map<ValidateAuthentication201Response>(await _PayerAuthenticationAPI.ValidateAuthenticationResultsAsync(validateRequest));
            _Logger.LogInformation(string.Format(WebAPIConstants.WEB_API_EXECUTED_MESSAGE, ServicesEnum.ValidateAuthenticationResults.ToString()));
            ValidateResponse(validateAuthenticationResponse, validateRequest);
            return validateAuthenticationResponse;
        }

        protected override void ValidateRequest(object request)
        {
            ValidateAuthenticationRequest validateAuthenticationRequest = request as ValidateAuthenticationRequest;
            if (validateAuthenticationRequest == null ||
                validateAuthenticationRequest.ClientReferenceInformation == null ||
                string.IsNullOrEmpty(validateAuthenticationRequest.ClientReferenceInformation.Code) ||
                validateAuthenticationRequest.PaymentInformation == null ||
                validateAuthenticationRequest.PaymentInformation.Customer == null ||
                string.IsNullOrEmpty(validateAuthenticationRequest.PaymentInformation.Customer.CustomerId) ||
                validateAuthenticationRequest.OrderInformation == null ||
                validateAuthenticationRequest.OrderInformation.AmountDetails == null ||
                string.IsNullOrEmpty(validateAuthenticationRequest.OrderInformation.AmountDetails.Currency) ||
                validateAuthenticationRequest.OrderInformation.LineItems == null ||
                validateAuthenticationRequest.OrderInformation.LineItems.Count == 0 ||
                validateAuthenticationRequest.ConsumerAuthenticationInformation == null ||
                string.IsNullOrEmpty(validateAuthenticationRequest.ConsumerAuthenticationInformation.AuthenticationTransactionId))
                throw new BusinessException(new BusinessResponse()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Description = HttpStatusCode.BadRequest.ToString(),
                    DescriptionDetail = ValidateAuthenticationConstants.VALIDATE_AUTHENTICATION_SERVICE_NAME,
                    ContentRequest = validateAuthenticationRequest
                })
                {
                    ServiceInterface = ServicesEnum.ValidateAuthenticationResults.ToString()
                };
        }

        protected override void ValidateResponse(object response, object request = null)
        {
            ValidateAuthentication201Response validateAuthenticationResponse = response as ValidateAuthentication201Response;
            if (validateAuthenticationResponse == null ||
                !Enum.GetNames(typeof(ValidateAuthenticationResults201Enum)).Any(pS => pS == validateAuthenticationResponse.Status))
            {
                throw new BusinessException(new BusinessResponse()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Description = HttpStatusCode.BadRequest.ToString(),
                    DescriptionDetail = ValidateAuthenticationConstants.VALIDATE_AUTHENTICATION_SERVICE_NAME,
                    ContentRequest = request,
                    ContentResponse = validateAuthenticationResponse
                })
                {
                    ServiceInterface = ServicesEnum.ValidateAuthenticationResults.ToString(),
                    ExecutedInnerService = ServicesEnum.ValidateAuthenticationResults.ToString()
                };
            }
        }
        #endregion
    }
}
