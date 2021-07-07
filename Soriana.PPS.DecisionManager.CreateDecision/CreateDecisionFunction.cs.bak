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
using Soriana.PPS.Common.DTO.Cybersource.DecisionManager;
using Soriana.PPS.Common.DTO.Salesforce;
using Soriana.PPS.DecisionManager.CreateDecision.Constants;
using Soriana.PPS.DecisionManager.CreateDecision.Services;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Soriana.PPS.DecisionManager.CreateDecision
{
    public class CreateDecisionFunction
    {
        #region Private Fields
        private readonly ICreateDecisionService _CreateDecisionService;
        private readonly ILogger<CreateDecisionFunction> _Logger;
        private readonly IMapper _Mapper;
        #endregion
        #region Constructors
        public CreateDecisionFunction(ICreateDecisionService createDecisionService,
                                        IMapper mapper,
                                        ILogger<CreateDecisionFunction> logger)
        {
            _CreateDecisionService = createDecisionService;
            _Mapper = mapper;
            _Logger = logger;
        }
        #endregion
        #region Public Methods
        [FunctionName(CreateDecisionConstants.CREATE_DECISION_FUNCTION_NAME)]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest request)
        {
            DecisionManagerRequest decisionManagerRequest = null;
            try
            {
                _Logger.LogInformation(string.Format(FunctionAppConstants.FUNCTION_EXECUTING_MESSAGE, CreateDecisionConstants.CREATE_DECISION_FUNCTION_NAME));
                if (!request.Body.CanSeek)
                    throw new Exception(JsonConvert.SerializeObject(new BusinessResponse() { StatusCode = (int)HttpStatusCode.BadRequest, Description = HttpStatusCode.BadRequest.ToString(), DescriptionDetail = CreateDecisionConstants.CREATE_DECISION_NO_CONTENT_REQUEST, ContentRequest = null }));
                request.Body.Position = 0;
                string jsonPaymentOrderProcessRequest = await new StreamReader(request.Body).ReadToEndAsync();
                PaymentOrderProcessRequest paymentOrderProcessRequest = JsonConvert.DeserializeObject<PaymentOrderProcessRequest>(jsonPaymentOrderProcessRequest);
                decisionManagerRequest = _Mapper.Map<DecisionManagerRequest>(paymentOrderProcessRequest);
                DecisionManager201Response decisionResponse = await _CreateDecisionService.CreateDecision(decisionManagerRequest);
                return new OkObjectResult(decisionResponse);
            }
            catch (BusinessException ex)
            {
                _Logger.LogError(ex, CreateDecisionConstants.CREATE_DECISION_FUNCTION_NAME);
                return new BadRequestObjectResult(ex.Message);
            }
            catch (ApiException ex)
            {
                _Logger.LogError(ex, CreateDecisionConstants.CREATE_DECISION_FUNCTION_NAME);
                string description = string.Concat(ex.ErrorCode.ToString(), CharactersConstants.ESPACE_CHAR, CharactersConstants.HYPHEN_CHAR, CharactersConstants.ESPACE_CHAR, CreateDecisionConstants.CREATE_DECISION_FUNCTION_NAME);
                return new BadRequestObjectResult(new BusinessResponse()
                {
                    StatusCode = ex.ErrorCode,
                    Description = description,
                    DescriptionDetail = ex.Message,
                    ContentRequest = JsonConvert.SerializeObject(decisionManagerRequest),
                    ContentResponse = JsonConvert.SerializeObject(ex)
                }); ;
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, CreateDecisionConstants.CREATE_DECISION_FUNCTION_NAME);
                return new BadRequestObjectResult(new BusinessResponse()
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Description = string.Concat(HttpStatusCode.InternalServerError.ToString(), CharactersConstants.ESPACE_CHAR, CharactersConstants.HYPHEN_CHAR, CharactersConstants.ESPACE_CHAR, CreateDecisionConstants.CREATE_DECISION_FUNCTION_NAME),
                    DescriptionDetail = ex.Message,
                    ContentRequest = JsonConvert.SerializeObject(decisionManagerRequest),
                    ContentResponse = JsonConvert.SerializeObject(ex)
                });
            }
        }
        #endregion
    }
}