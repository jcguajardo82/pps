using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Soriana.PPS.Common.Constants;
using Soriana.PPS.Common.DTO.Common;
using Soriana.PPS.Security.GenerateToken.Constants;
using Soriana.PPS.Security.GenerateToken.Services;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Soriana.PPS.Security.GenerateToken
{
    public class GenerateTokenFunction
    {
        #region Private Fields
        private readonly IGenerateTokenService _GenerateTokenService;
        private readonly ILogger<GenerateTokenFunction> _Logger;
        #endregion
        #region Constructors
        public GenerateTokenFunction(IGenerateTokenService generateTokenService,
                                        ILogger<GenerateTokenFunction> logger)
        {
            _GenerateTokenService = generateTokenService;
            _Logger = logger;
        }
        #endregion
        #region Public Methods
        [FunctionName(GenerateTokenConstants.GENERATE_TOKEN_FUNCTION_NAME)]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest request)
        {
            JsonWebTokenRequest jsonWebTokenRequest = null;
            try
            {
                _Logger.LogInformation(string.Format(FunctionAppConstants.FUNCTION_EXECUTING_MESSAGE, GenerateTokenConstants.GENERATE_TOKEN_FUNCTION_NAME));
                if (!request.Body.CanSeek)
                    throw new Exception(JsonConvert.SerializeObject(new BusinessResponse() { StatusCode = (int)HttpStatusCode.BadRequest, Description = HttpStatusCode.BadRequest.ToString(), DescriptionDetail = GenerateTokenConstants.GENERATE_TOKEN_NO_CONTENT_REQUEST, ContentRequest = null }));
                request.Body.Position = 0;
                string jSONWebToken = await new StreamReader(request.Body).ReadToEndAsync();
                jsonWebTokenRequest = JsonConvert.DeserializeObject<JsonWebTokenRequest>(jSONWebToken);
                JsonWebTokenResponse jsonWebTokenResponse = await _GenerateTokenService.GenerateToken(jsonWebTokenRequest);
                return new OkObjectResult(jsonWebTokenResponse);
            }
            catch (BusinessException ex)
            {
                _Logger.LogError(ex, GenerateTokenConstants.GENERATE_TOKEN_FUNCTION_NAME);
                return new BadRequestObjectResult(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, GenerateTokenConstants.GENERATE_TOKEN_FUNCTION_NAME);
                return new BadRequestObjectResult(new BusinessResponse()
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Description = string.Concat(HttpStatusCode.InternalServerError.ToString(), CharactersConstants.ESPACE_CHAR, CharactersConstants.HYPHEN_CHAR, CharactersConstants.ESPACE_CHAR, GenerateTokenConstants.GENERATE_TOKEN_FUNCTION_NAME),
                    DescriptionDetail = ex.Message,
                    ContentRequest = JsonConvert.SerializeObject(jsonWebTokenRequest),
                    ContentResponse = JsonConvert.SerializeObject(ex)
                });
            }
        }
        #endregion
    }
}
