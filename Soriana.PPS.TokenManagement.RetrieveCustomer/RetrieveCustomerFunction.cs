using CyberSource.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Soriana.PPS.Common.Constants;
using Soriana.PPS.Common.DTO.Common;
using Soriana.PPS.Common.DTO.Cybersource.TokenManagement;
using Soriana.PPS.TokenManagement.RetrieveCustomer.Constants;
using Soriana.PPS.TokenManagement.RetrieveCustomer.Services;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Soriana.PPS.TokenManagement.RetrieveCustomer
{
    public class RetrieveCustomerFunction
    {
        #region Private Fields
        private readonly IRetrieveCustomerService _RetrieveCustomerService;
        private readonly ILogger<RetrieveCustomerFunction> _Logger;
        #endregion
        #region Constructors
        public RetrieveCustomerFunction(IRetrieveCustomerService retrieveCustomerService,
                                        ILogger<RetrieveCustomerFunction> logger)
        {
            _RetrieveCustomerService = retrieveCustomerService;
            _Logger = logger;
        }
        #endregion
        #region Public Methods
        [FunctionName(RetrieveCustomerConstants.RETRIEVE_CUSTOMER_FUNCTION_NAME)]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest request)
        {
            SimpleCustomerRequest customerRequest = null;
            try
            {
                _Logger.LogInformation(string.Format(FunctionAppConstants.FUNCTION_EXECUTING_MESSAGE, RetrieveCustomerConstants.RETRIEVE_CUSTOMER_FUNCTION_NAME));
                if (!request.Body.CanSeek)
                    return new BadRequestObjectResult(new BusinessResponse() { StatusCode = (int)HttpStatusCode.BadRequest, Description = HttpStatusCode.BadRequest.ToString(), DescriptionDetail = RetrieveCustomerConstants.RETRIEVE_CUSTOMER_FUNCTION_NAME, ContentRequest = null });
                request.Body.Position = 0;
                string jsonCustomerRequest = await new StreamReader(request.Body).ReadToEndAsync();
                customerRequest = JsonConvert.DeserializeObject<SimpleCustomerRequest>(jsonCustomerRequest);
                TmsCustomersResponse customerResponse = await _RetrieveCustomerService.RetrieveCustomer(customerRequest);
                return new OkObjectResult(customerResponse);
            }
            catch (BusinessException ex)
            {
                _Logger.LogError(ex, RetrieveCustomerConstants.RETRIEVE_CUSTOMER_FUNCTION_NAME);
                return new BadRequestObjectResult(ex.Message);
            }
            catch (ApiException ex)
            {
                _Logger.LogError(ex, RetrieveCustomerConstants.RETRIEVE_CUSTOMER_FUNCTION_NAME);
                string description = string.Concat(ex.ErrorCode.ToString(), CharactersConstants.ESPACE_CHAR, CharactersConstants.HYPHEN_CHAR, CharactersConstants.ESPACE_CHAR, RetrieveCustomerConstants.RETRIEVE_CUSTOMER_FUNCTION_NAME);
                return new BadRequestObjectResult(new BusinessResponse()
                {
                    StatusCode = ex.ErrorCode,
                    Description = description,
                    DescriptionDetail = ex.Message,
                    ContentRequest = JsonConvert.SerializeObject(customerRequest),
                    ContentResponse = JsonConvert.SerializeObject(ex)
                });
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, RetrieveCustomerConstants.RETRIEVE_CUSTOMER_FUNCTION_NAME);
                return new BadRequestObjectResult(new BusinessResponse()
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Description = string.Concat(HttpStatusCode.InternalServerError.ToString(), CharactersConstants.ESPACE_CHAR, CharactersConstants.HYPHEN_CHAR, CharactersConstants.ESPACE_CHAR, RetrieveCustomerConstants.RETRIEVE_CUSTOMER_FUNCTION_NAME),
                    DescriptionDetail = ex.Message,
                    ContentRequest = JsonConvert.SerializeObject(customerRequest),
                    ContentResponse = JsonConvert.SerializeObject(ex)
                });
            }
        }
        #endregion
    }
}

