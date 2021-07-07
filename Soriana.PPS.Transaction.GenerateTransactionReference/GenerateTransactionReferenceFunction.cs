using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Soriana.PPS.Common.Constants;
using Soriana.PPS.Common.DTO.Common;
using Soriana.PPS.Common.DTO.PaymentProcessor;
using Soriana.PPS.Control.GenerateTransactionReference.Constants;
using Soriana.PPS.Control.GenerateTransactionReference.Services;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Soriana.PPS.Control.GenerateTransactionReference
{
    public class GenerateTransactionReferenceFunction
    {
        #region Private Fields
        private readonly IGenerateTransactionReferenceService _GenerateTransactionReferenceService;
        private readonly ILogger<GenerateTransactionReferenceFunction> _Logger;
        #endregion
        #region Constructors
        public GenerateTransactionReferenceFunction(IGenerateTransactionReferenceService generateTransactionReferenceService,
                                                    ILogger<GenerateTransactionReferenceFunction> logger)
        {
            _GenerateTransactionReferenceService = generateTransactionReferenceService;
            _Logger = logger;
        }
        #endregion
        #region Public Methods
        [FunctionName(GenerateTransactionReferenceConstants.GENERATE_TRANSACTION_REFERENCE_FUNCTION_NAME)]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest request)
        {
            try
            {
                _Logger.LogInformation(string.Format(FunctionAppConstants.FUNCTION_EXECUTING_MESSAGE, GenerateTransactionReferenceConstants.GENERATE_TRANSACTION_REFERENCE_FUNCTION_NAME));
                TransactionReferenceResponse transactionReferenceResponse = await _GenerateTransactionReferenceService.GenerateTransactionReference();
                return new OkObjectResult(transactionReferenceResponse);
            }
            catch (BusinessException ex)
            {
                _Logger.LogError(ex, GenerateTransactionReferenceConstants.GENERATE_TRANSACTION_REFERENCE_FUNCTION_NAME);
                return new BadRequestObjectResult(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, GenerateTransactionReferenceConstants.GENERATE_TRANSACTION_REFERENCE_FUNCTION_NAME);
                return new BadRequestObjectResult(JsonConvert.SerializeObject(new BusinessResponse()
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Description = string.Concat(HttpStatusCode.InternalServerError.ToString(), CharactersConstants.ESPACE_CHAR, CharactersConstants.HYPHEN_CHAR, CharactersConstants.ESPACE_CHAR, GenerateTransactionReferenceConstants.GENERATE_TRANSACTION_REFERENCE_FUNCTION_NAME),
                    DescriptionDetail = ex.Message,
                    ContentRequest = await request.ReadAsStringAsync(),
                    ContentResponse = JsonConvert.SerializeObject(ex)
                }));
            }
        }
        #endregion
    }
}
