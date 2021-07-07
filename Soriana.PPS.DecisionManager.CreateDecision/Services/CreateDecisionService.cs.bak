using AutoMapper;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Soriana.PPS.Common.Constants;
using Soriana.PPS.Common.DTO.Common;
using Soriana.PPS.Common.DTO.Cybersource.DecisionManager;
using Soriana.PPS.Common.Enums;
using Soriana.PPS.Common.Services;
using Soriana.PPS.DecisionManager.CreateDecision.Constants;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Soriana.PPS.DecisionManager.CreateDecision.Services
{
    public sealed class CreateDecisionService : ServiceBase, ICreateDecisionService
    {
        #region Private Fields
        private readonly ILogger<CreateDecisionService> _Logger;
        private readonly IDecisionManagerAPI _DecisionManagerAPI;
        private readonly IMapper _Mapper;
        #endregion
        #region Constructors
        public CreateDecisionService(IDecisionManagerAPI decisionManagerAPI,
                                    IMapper mapper,
                                    ILogger<CreateDecisionService> logger)
        {
            _Logger = logger;
            _Mapper = mapper;
            _DecisionManagerAPI = decisionManagerAPI;
        }
        #endregion
        #region Public Methods
        public async Task<DecisionManager201Response> CreateDecision(DecisionManagerRequest decisionManagerCaseRequest)
        {
            ValidateRequest(decisionManagerCaseRequest);
            _Logger.LogInformation(string.Format(WebAPIConstants.WEB_API_EXECUTE_MESSAGE, ServicesEnum.CreateDecisionManager.ToString()));
            _Logger.LogInformation(string.Format(WebAPIConstants.WEB_API_CALLING_WITH_DATA_REQUEST_MESSAGE, ServicesEnum.CreateDecisionManager.ToString(), JsonConvert.SerializeObject(decisionManagerCaseRequest)));
            DecisionManager201Response decisionResponse = _Mapper.Map<DecisionManager201Response>(await _DecisionManagerAPI.CreateBundledDecisionManagerCaseAsync(decisionManagerCaseRequest));
            _Logger.LogInformation(string.Format(WebAPIConstants.WEB_API_EXECUTED_MESSAGE, ServicesEnum.CreateDecisionManager.ToString()));
            ValidateResponse(decisionResponse, decisionManagerCaseRequest);
            return decisionResponse;
        }

        protected override void ValidateRequest(object request)
        {
            DecisionManagerRequest decisionRequest = request as DecisionManagerRequest;
            if (decisionRequest == null ||
                decisionRequest.ClientReferenceInformation == null ||
                string.IsNullOrEmpty(decisionRequest.ClientReferenceInformation.Code) ||
                decisionRequest.PaymentInformation == null ||
                decisionRequest.PaymentInformation.Customer == null ||
                string.IsNullOrEmpty(decisionRequest.PaymentInformation.Customer.CustomerId) ||
                decisionRequest.OrderInformation == null ||
                decisionRequest.OrderInformation.AmountDetails == null ||
                string.IsNullOrEmpty(decisionRequest.OrderInformation.AmountDetails.Currency) ||
                decisionRequest.OrderInformation.LineItems == null ||
                decisionRequest.OrderInformation.LineItems.Count == 0 ||
                decisionRequest.DeviceInformation == null ||
                string.IsNullOrEmpty(decisionRequest.DeviceInformation.FingerprintSessionId) ||
                ///TODO: Validar como enviar la estructura de MDD
                decisionRequest.MerchantDefinedInformation == null ||
                !decisionRequest.MerchantDefinedInformation.Any())
                throw new BusinessException(new BusinessResponse()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Description = HttpStatusCode.BadRequest.ToString(),
                    DescriptionDetail = CreateDecisionConstants.CREATE_DECISION_SERVICE_NAME,
                    ContentRequest = decisionRequest
                })
                {
                    ServiceInterface = ServicesEnum.CreateDecisionManager.ToString()
                };
        }

        protected override void ValidateResponse(object response, object request = null)
        {
            DecisionManager201Response decisionResponse = response as DecisionManager201Response;
            if (decisionResponse == null ||
                !Enum.GetNames(typeof(CreateDecisionStatus201Enum)).Any(pS => pS == decisionResponse.Status))
            {
                throw new BusinessException(new BusinessResponse()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Description = HttpStatusCode.BadRequest.ToString(),
                    DescriptionDetail = CreateDecisionConstants.CREATE_DECISION_SERVICE_NAME,
                    ContentRequest = request,
                    ContentResponse = decisionResponse
                })
                {
                    ServiceInterface = ServicesEnum.CreateDecisionManager.ToString(),
                    ExecutedInnerService = ServicesEnum.CreateDecisionManager.ToString()
                };
            }
        }
        #endregion
    }
}