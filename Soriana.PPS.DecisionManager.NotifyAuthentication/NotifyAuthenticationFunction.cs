using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Soriana.PPS.Common.Constants;
using Soriana.PPS.Common.DTO.Common;
using Soriana.PPS.Common.DTO.Cybersource.DecisionManager;
using Soriana.PPS.DecisionManager.Common.Compatibility.DTO.Cybersource.Common;
using Soriana.PPS.DecisionManager.NotifyAuthentication.Constants;
using Soriana.PPS.DecisionManager.NotifyAuthentication.Services;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Soriana.PPS.DecisionManager.NotifyAuthentication
{
    public class NotifyAuthenticationFunction
    {
        #region Private Fields
        private readonly INotifyAuthenticationService _NotifyAuthenticationService;
        private readonly ILogger<NotifyAuthenticationFunction> _Logger;
        #endregion
        #region Constructors
        public NotifyAuthenticationFunction(INotifyAuthenticationService notifyAuthenticationService,
                                            ILogger<NotifyAuthenticationFunction> logger)
        {
            _NotifyAuthenticationService = notifyAuthenticationService;
            _Logger = logger;
        }
        #endregion
        #region Public Methods
        [FunctionName(NotifyAuthenticationConstants.NOTIFY_AUTHENTICATION_FUNCTION_NAME)]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest request)
        {
            NotifyAuthenticationRequest notifyAuthenticationRequest = null;
            try
            {
                _Logger.LogInformation(string.Format(FunctionAppConstants.FUNCTION_EXECUTING_MESSAGE, NotifyAuthenticationConstants.NOTIFY_AUTHENTICATION_FUNCTION_NAME));
                if (!request.Body.CanSeek)
                    throw new Exception(JsonConvert.SerializeObject(new BusinessResponse() { StatusCode = (int)HttpStatusCode.BadRequest, Description = HttpStatusCode.BadRequest.ToString(), DescriptionDetail = NotifyAuthenticationConstants.NOTIFY_AUTHENTICATION_NO_CONTENT_REQUEST, ContentRequest = null }));
                request.Body.Position = 0;
                string jsonNotifyAuthenticationRequest = await new StreamReader(request.Body).ReadToEndAsync();
                notifyAuthenticationRequest = JsonConvert.DeserializeObject<NotifyAuthenticationRequest>(jsonNotifyAuthenticationRequest);
                NotifyAuthenticationResponse notifyAuthenticationResponse = await _NotifyAuthenticationService.NotifyAuthenticationAsync(notifyAuthenticationRequest);
                return new OkObjectResult(notifyAuthenticationResponse);
            }
            catch (BusinessException ex)
            {
                _Logger.LogError(ex, NotifyAuthenticationConstants.NOTIFY_AUTHENTICATION_FUNCTION_NAME);
                return new BadRequestObjectResult(ex.Message);
            }
            catch (CybersourceCompatibilityFaultException ex)
            {
                _Logger.LogError(ex, NotifyAuthenticationConstants.NOTIFY_AUTHENTICATION_FUNCTION_NAME);
                ///
                /// TODO: Validar como se regresa la exception para SOAP services
                /// 
                string description = string.Concat(ex.Code.Name, CharactersConstants.ESPACE_CHAR, CharactersConstants.HYPHEN_CHAR, CharactersConstants.ESPACE_CHAR, NotifyAuthenticationConstants.NOTIFY_AUTHENTICATION_FUNCTION_NAME);
                return new BadRequestObjectResult(new BusinessResponse()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Description = description,
                    DescriptionDetail = ex.Message,
                    ContentRequest = JsonConvert.SerializeObject(notifyAuthenticationRequest),
                    ContentResponse = JsonConvert.SerializeObject(ex)
                });
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, NotifyAuthenticationConstants.NOTIFY_AUTHENTICATION_FUNCTION_NAME);
                return new BadRequestObjectResult(new BusinessResponse()
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Description = string.Concat(HttpStatusCode.InternalServerError.ToString(), CharactersConstants.ESPACE_CHAR, CharactersConstants.HYPHEN_CHAR, CharactersConstants.ESPACE_CHAR, NotifyAuthenticationConstants.NOTIFY_AUTHENTICATION_FUNCTION_NAME),
                    DescriptionDetail = ex.Message,
                    ContentRequest = JsonConvert.SerializeObject(notifyAuthenticationRequest),
                    ContentResponse = JsonConvert.SerializeObject(ex)
                });
            }
        }
        #endregion
    }
}
