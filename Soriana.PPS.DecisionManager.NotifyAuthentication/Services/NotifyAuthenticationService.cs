using Microsoft.Extensions.Logging;
using Soriana.PPS.Common.Constants;
using Soriana.PPS.Common.DTO.Common;
using Soriana.PPS.Common.DTO.Cybersource.DecisionManager;
using Soriana.PPS.Common.Enums;
using Soriana.PPS.Common.Services;
using Soriana.PPS.DecisionManager.Common.Compatibility.Services;
using Soriana.PPS.DecisionManager.NotifyAuthentication.Constants;
using System.Net;
using System.Threading.Tasks;

namespace Soriana.PPS.DecisionManager.NotifyAuthentication.Services
{
    public class NotifyAuthenticationService : ServiceBase, INotifyAuthenticationService
    {
        #region Private Fields
        private readonly ILogger<NotifyAuthenticationService> _Logger;
        private readonly INotifyAuthenticationSOAPService _NotifyAuthenticationSOAPService;
        #endregion
        #region Constructors
        public NotifyAuthenticationService(INotifyAuthenticationSOAPService notifyAuthenticationSOAPService,
                                            ILogger<NotifyAuthenticationService> logger)
        {
            _Logger = logger;
            _NotifyAuthenticationSOAPService = notifyAuthenticationSOAPService;
        }
        #endregion
        #region Private Methods
        private NotifyAuthenticationResponse GetNotifyAuthenticationFromString(string notifyAuthentication)
        {
            NotifyAuthenticationResponse notifyAuthenticationResponse = new NotifyAuthenticationResponse();
            string[] keyValue = notifyAuthentication.Split(CharactersConstants.EQUAL_CHAR);
            for (int i = 0; i < keyValue.Length; i += 2)
            {
                if (keyValue[i].Contains(PaymentProcessorConstants.PAYMENT_ORDER_PROCESS_NOTIFY_VALIDATION_REASON_CODE))
                {
                    notifyAuthenticationResponse.ReasonCode = keyValue[i + 1];
                }
                else if (keyValue[i].Contains(PaymentProcessorConstants.PAYMENT_ORDER_PROCESS_NOTIFY_VALIDATION_MERCHANT_REFERENCE_CODE))
                {
                    notifyAuthenticationResponse.AuthorizationCode = keyValue[i + 1];
                }
                else if (keyValue[i].Contains(PaymentProcessorConstants.PAYMENT_ORDER_PROCESS_NOTIFY_VALIDATION_REQUEST_ID))
                {
                    notifyAuthenticationResponse.RequestID = keyValue[i + 1];
                }
            }
            return notifyAuthenticationResponse;
        }
        #endregion
        #region Public Methods
        public async Task<NotifyAuthenticationResponse> NotifyAuthenticationAsync(NotifyAuthenticationRequest notifyAuthenticationRequest)
        {
            ValidateRequest(notifyAuthenticationRequest);
            _Logger.LogInformation(string.Format(WebAPIConstants.WEB_API_EXECUTE_MESSAGE, ServicesEnum.NotifyAuthenticationDecisionManager.ToString()));
            string notifyAuthentication = await _NotifyAuthenticationSOAPService.NotifyAuthenticationAsync(notifyAuthenticationRequest);
            NotifyAuthenticationResponse notifyAuthenticationResponse = GetNotifyAuthenticationFromString(notifyAuthentication);
            _Logger.LogInformation(string.Format(WebAPIConstants.WEB_API_EXECUTED_MESSAGE, ServicesEnum.NotifyAuthenticationDecisionManager.ToString()));
            ValidateResponse(notifyAuthenticationResponse, notifyAuthenticationRequest);
            return notifyAuthenticationResponse;
        }

        protected override void ValidateRequest(object request)
        {
            NotifyAuthenticationRequest notifyAuthenticationRequest = request as NotifyAuthenticationRequest;
            if (notifyAuthenticationRequest == null ||
                string.IsNullOrEmpty(notifyAuthenticationRequest.ActionCode) ||
                string.IsNullOrEmpty(notifyAuthenticationRequest.Comments) ||
                string.IsNullOrEmpty(notifyAuthenticationRequest.MerchantReferenceCode) ||
                string.IsNullOrEmpty(notifyAuthenticationRequest.RequestID))
                throw new BusinessException(new BusinessResponse()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Description = HttpStatusCode.BadRequest.ToString(),
                    DescriptionDetail = NotifyAuthenticationConstants.NOTIFY_AUTHENTICATION_SERVICE_NAME,
                    ContentRequest = notifyAuthenticationRequest
                })
                {
                    ServiceInterface = ServicesEnum.NotifyAuthenticationDecisionManager.ToString()
                };
        }

        protected override void ValidateResponse(object response, object request = null)
        {
            NotifyAuthenticationResponse notifyAuthenticationResponse = response as NotifyAuthenticationResponse;
            if (string.IsNullOrEmpty(notifyAuthenticationResponse.ReasonCode))
            {
                throw new BusinessException(new BusinessResponse()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Description = HttpStatusCode.BadRequest.ToString(),
                    DescriptionDetail = NotifyAuthenticationConstants.NOTIFY_AUTHENTICATION_SERVICE_NAME,
                    ContentRequest = request,
                    ContentResponse = notifyAuthenticationResponse
                })
                {
                    ServiceInterface = ServicesEnum.NotifyAuthenticationDecisionManager.ToString(),
                    ExecutedInnerService = ServicesEnum.NotifyAuthenticationDecisionManager.ToString()
                };
            }
        }
        #endregion
    }
}
