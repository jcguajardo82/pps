using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Soriana.PPS.Common.Configuration;
using Soriana.PPS.Common.Constants;
using Soriana.PPS.Common.DTO.Common;
using Soriana.PPS.Common.Enums;
using Soriana.PPS.Common.Extensions;
using Soriana.PPS.Common.Security.JWT;
using Soriana.PPS.Common.Services;
using Soriana.PPS.Security.GenerateToken.Constants;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Soriana.PPS.Security.GenerateToken.Services
{
    public sealed class GenerateTokenService : ServiceBase, IGenerateTokenService
    {
        #region Private Fields
        private readonly ILogger<GenerateTokenService> _Logger;
        private readonly JWTCardinalOptions _JWTCardinalOptions;
        #endregion
        #region Constructors
        public GenerateTokenService(IOptions<JWTCardinalOptions> options, ILogger<GenerateTokenService> logger)
        {
            _Logger = logger;
            ValidateOptions(options);
            _JWTCardinalOptions = options.Value;
        }
        #endregion
        #region Public Methods
        public async Task<JsonWebTokenResponse> GenerateToken(JsonWebTokenRequest jwtRequest)
        {
            ValidateRequest(jwtRequest);
            _Logger.LogInformation(string.Format(WebAPIConstants.WEB_API_EXECUTE_MESSAGE, ServicesEnum.GenerateToken.ToString()));
            JsonWebTokenResponse jsonWebTokenResponse = await Task.Run(() => { return CreatePaymentProcessJWT(jwtRequest); });
            _Logger.LogInformation(string.Format(WebAPIConstants.WEB_API_EXECUTED_MESSAGE, ServicesEnum.GenerateToken.ToString()));
            //ValidateResponse(jsonWebTokenResponse, jwtRequest);
            return jsonWebTokenResponse;
        }

        protected override void ValidateRequest(object request)
        {
            JsonWebTokenRequest jsonWebTokenRequest = request as JsonWebTokenRequest;
            if (jsonWebTokenRequest == null ||
                string.IsNullOrEmpty(jsonWebTokenRequest.ReferenceId) ||
                jsonWebTokenRequest.Payload == null ||
                jsonWebTokenRequest.Payload.OrdenDetails == null ||
                string.IsNullOrEmpty(jsonWebTokenRequest.Payload.OrdenDetails.OrderNumber) ||
                string.IsNullOrEmpty(jsonWebTokenRequest.Payload.OrdenDetails.CurrencyCode) ||
                string.IsNullOrEmpty(jsonWebTokenRequest.Payload.OrdenDetails.Amount))
                throw new BusinessException(new BusinessResponse()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Description = HttpStatusCode.BadRequest.ToString(),
                    DescriptionDetail = GenerateTokenConstants.GENERATE_TOKEN_SERVICE_NAME,
                    ContentRequest = jsonWebTokenRequest
                })
                {
                    ServiceInterface = ServicesEnum.GenerateToken.ToString()
                };
        }

        protected override void ValidateResponse(object response, object request = null)
        {
            JsonWebTokenResponse jsonWebTokenResponse = response as JsonWebTokenResponse;
            if (jsonWebTokenResponse != null)
                jsonWebTokenResponse.APIKey = _JWTCardinalOptions.APIKey;
            Exception exception = null;
            if (jsonWebTokenResponse == null ||
                string.IsNullOrEmpty(jsonWebTokenResponse.JsonWebToken) ||
                ///TODO: Validar si es la correcta implementación para generar Cardinal Token
                !JsonWebTokenHelper.TryValidJWTCardinal(jsonWebTokenResponse, out exception))
            {
                jsonWebTokenResponse.APIKey = null;
                throw new BusinessException(new BusinessResponse()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Description = (exception == null) ? HttpStatusCode.BadRequest.ToString() : string.Concat(HttpStatusCode.BadRequest.ToString(), CharactersConstants.ESPACE_CHAR, CharactersConstants.HYPHEN_CHAR, CharactersConstants.ESPACE_CHAR, GenerateTokenConstants.GENERATE_TOKEN_SERVICE_NAME),
                    DescriptionDetail = exception == null ? GenerateTokenConstants.GENERATE_TOKEN_SERVICE_NAME as object : exception,
                    ContentRequest = request,
                    ContentResponse = jsonWebTokenResponse
                })
                {
                    ServiceInterface = ServicesEnum.GenerateToken.ToString(),
                    ExecutedInnerService = ServicesEnum.GenerateToken.ToString()
                };
            }
            jsonWebTokenResponse.APIKey = null;
        }
        #endregion
        #region Private Methods
        private JsonWebTokenResponse CreatePaymentProcessJWT(JsonWebTokenRequest request)
        {
            DateTime dateTime = DateTime.Today;
            request.JWTId = Guid.NewGuid().ToString();
            request.IssuedAt = dateTime.ToUnixTime().ToString();
            dateTime = dateTime.AddMinutes(_JWTCardinalOptions.ExpirationTimeMinutes);
            request.Expiration = dateTime.ToUnixTime().ToString();
            request.Issuer = _JWTCardinalOptions.APIIdentifier;
            request.OrganizationUnitId = _JWTCardinalOptions.OrganizationUnitId;
            request.APIIdentifier = _JWTCardinalOptions.APIIdentifier;
            request.APIKey = _JWTCardinalOptions.APIKey;
            return JsonWebTokenHelper.CreateJWTCardinal(request);
        }
        private void ValidateOptions(IOptions<JWTCardinalOptions> options)
        {
            if (options == null ||
                options.Value == null ||
                string.IsNullOrEmpty(options.Value.APIIdentifier) ||
                string.IsNullOrEmpty(options.Value.APIKey) ||
                string.IsNullOrEmpty(options.Value.OrganizationUnitId) ||
                options.Value.ExpirationTimeMinutes == 0)
            {
                throw new BusinessException(new BusinessResponse()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Description = HttpStatusCode.BadRequest.ToString(),
                    DescriptionDetail = string.Concat(GenerateTokenConstants.GENERATE_TOKEN_SERVICE_NAME, CharactersConstants.ESPACE_CHAR, CharactersConstants.HYPHEN_CHAR, CharactersConstants.ESPACE_CHAR, GenerateTokenConstants.GENERATE_TOKEN_OPTIONS),
                    ContentRequest = options
                })
                {
                    ServiceInterface = ServicesEnum.GenerateToken.ToString()
                };
            }
        }
        #endregion
    }
}
