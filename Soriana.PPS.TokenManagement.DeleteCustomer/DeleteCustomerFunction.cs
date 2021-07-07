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
using Soriana.PPS.TokenManagement.DeleteCustomer.Constants;
using Soriana.PPS.TokenManagement.DeleteCustomer.Services;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Soriana.PPS.TokenManagement.DeleteCustomer
{
    public class DeleteCustomerFunction
    {
        #region Private Fields
        private readonly IDeleteCustomerService _DeleteCustomerService;
        private readonly ILogger<DeleteCustomerFunction> _Logger;
        #endregion
        #region Constructors
        public DeleteCustomerFunction(IDeleteCustomerService deleteCustomerService,
                                        ILogger<DeleteCustomerFunction> logger)
        {
            _DeleteCustomerService = deleteCustomerService;
            _Logger = logger;
        }
        #endregion
        #region Public Methods
        [FunctionName(DeleteCustomerConstants.DELETE_CUSTOMER_FUNCTION_NAME)]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest request)
        {
            SimpleCustomerRequest customerRequest = null;
            try
            {
                _Logger.LogInformation(string.Format(FunctionAppConstants.FUNCTION_EXECUTING_MESSAGE, DeleteCustomerConstants.DELETE_CUSTOMER_FUNCTION_NAME));
                if (!request.Body.CanSeek)
                    return new BadRequestObjectResult(new BusinessResponse() { StatusCode = (int)HttpStatusCode.BadRequest, Description = HttpStatusCode.BadRequest.ToString(), DescriptionDetail = DeleteCustomerConstants.DELETE_CUSTOMER_FUNCTION_NAME, ContentRequest = null });
                request.Body.Position = 0;
                string jsonCustomerRequest = await new StreamReader(request.Body).ReadToEndAsync();
                customerRequest = JsonConvert.DeserializeObject<SimpleCustomerRequest>(jsonCustomerRequest);
                await _DeleteCustomerService.DeleteCustomer(customerRequest);
                return new OkObjectResult(new BusinessResponse() { StatusCode = (int)HttpStatusCode.OK, Description = HttpStatusCode.OK.ToString(), DescriptionDetail = DeleteCustomerConstants.DELETE_CUSTOMER_FUNCTION_NAME, ContentRequest = customerRequest });
            }
            catch (BusinessException ex)
            {
                _Logger.LogError(ex, DeleteCustomerConstants.DELETE_CUSTOMER_FUNCTION_NAME);
                return new BadRequestObjectResult(ex.Message);
            }
            catch (ApiException ex)
            {
                _Logger.LogError(ex, DeleteCustomerConstants.DELETE_CUSTOMER_FUNCTION_NAME);
                string description = string.Concat(Convert.ToString(ex.ErrorCode.ToString()), CharactersConstants.ESPACE_CHAR, CharactersConstants.HYPHEN_CHAR, CharactersConstants.ESPACE_CHAR, DeleteCustomerConstants.DELETE_CUSTOMER_FUNCTION_NAME);
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
                _Logger.LogError(ex, DeleteCustomerConstants.DELETE_CUSTOMER_FUNCTION_NAME);
                return new BadRequestObjectResult(new BusinessResponse()
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Description = string.Concat(HttpStatusCode.InternalServerError.ToString(), CharactersConstants.ESPACE_CHAR, CharactersConstants.HYPHEN_CHAR, CharactersConstants.ESPACE_CHAR, DeleteCustomerConstants.DELETE_CUSTOMER_FUNCTION_NAME),
                    DescriptionDetail = ex.Message,
                    ContentRequest = JsonConvert.SerializeObject(customerRequest),
                    ContentResponse = JsonConvert.SerializeObject(ex)
                });
            }
        }
        #endregion
    }
}

