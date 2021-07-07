using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Soriana.PPS.Common.Constants;
using Soriana.PPS.Common.DTO.Common;
using Soriana.PPS.Common.DTO.Salesforce;
using Soriana.PPS.PaymentOrderProcess.Constants;
using Soriana.PPS.PaymentOrderProcess.Services;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Soriana.PPS.PaymentOrderProcess
{
    public class PaymentOrderProcessFunction
    {
        #region Private Fields
        private readonly IPaymentOrderProcessService _PaymentOrderProcessService;
        private readonly ILogger<PaymentOrderProcessFunction> _Logger;
        #endregion
        #region Constructors
        public PaymentOrderProcessFunction(IPaymentOrderProcessService paymentOrderProcessService,
                                            ILogger<PaymentOrderProcessFunction> logger)
        {
            _PaymentOrderProcessService = paymentOrderProcessService;
            _Logger = logger;
        }
        #endregion
        #region Public Methods
        [FunctionName(PaymentOrderProcessConstants.PAYMENT_ORDER_PROCESS_FUNCTION_NAME)]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest request)
        {
            try
            {
                _Logger.LogInformation(string.Format(FunctionAppConstants.FUNCTION_EXECUTING_MESSAGE, PaymentOrderProcessConstants.PAYMENT_ORDER_PROCESS_FUNCTION_NAME));
                if (!request.Body.CanSeek)
                    return new BadRequestObjectResult(new BusinessResponse() { StatusCode = (int)HttpStatusCode.BadRequest, Description = HttpStatusCode.BadRequest.ToString(), DescriptionDetail = PaymentOrderProcessConstants.PAYMENT_ORDER_PROCESS_FUNCTION_NAME, ContentRequest = null });
                request.Body.Position = 0;
                string jsonPostCustomerRequest = await new StreamReader(request.Body).ReadToEndAsync();
                PaymentOrderProcessRequest paymentOrderProcessRequest = JsonConvert.DeserializeObject<PaymentOrderProcessRequest>(jsonPostCustomerRequest);
                PaymentOrderProcessResponse paymentOrderProcessResponse = await _PaymentOrderProcessService.PaymentOrderProcess(paymentOrderProcessRequest);
                if (paymentOrderProcessResponse.ResponseCode == Convert.ToString((int)HttpStatusCode.OK))
                    return new OkObjectResult(paymentOrderProcessResponse);
                else
                    return new BadRequestObjectResult(paymentOrderProcessResponse);
            }
            catch (BusinessException ex)
            {
                _Logger.LogError(ex, PaymentOrderProcessConstants.PAYMENT_ORDER_PROCESS_FUNCTION_NAME);
                BusinessResponse businessResponse = JsonConvert.DeserializeObject<BusinessResponse>(ex.Message);
                return new BadRequestObjectResult(JsonConvert.SerializeObject(new PaymentOrderProcessResponse() { ResponseCode = Convert.ToString(businessResponse?.StatusCode), ResponseErrorText = businessResponse?.Description, ResponseError = ex.Message }));
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, PaymentOrderProcessConstants.PAYMENT_ORDER_PROCESS_FUNCTION_NAME);
                BusinessResponse businessResponse = new BusinessResponse()
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Description = string.Concat(HttpStatusCode.InternalServerError.ToString(), CharactersConstants.ESPACE_CHAR, CharactersConstants.HYPHEN_CHAR, CharactersConstants.ESPACE_CHAR, PaymentOrderProcessConstants.PAYMENT_ORDER_PROCESS_FUNCTION_NAME),
                    DescriptionDetail = ex.Message
                };
                return new BadRequestObjectResult(JsonConvert.SerializeObject(new PaymentOrderProcessResponse() { ResponseCode = Convert.ToString(businessResponse.StatusCode), ResponseErrorText = businessResponse.Description, ResponseError = JsonConvert.SerializeObject(businessResponse) }));
            }
        }
        #endregion
    }
}

