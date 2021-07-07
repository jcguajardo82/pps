using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

using Soriana.PPS.Common.Constants;
using Soriana.PPS.Common.DTO.Common;
using Soriana.PPS.Common.DTO.Cybersource.Payments;
using Soriana.PPS.PaymentOrderProcess.ClosureOrderGrocery.Services;
using Soriana.PPS.PaymentOrderProcess.ClosureOrderGrocery.Constants;
using Soriana.PPS.Common.DTO.ClosureOrder;

using CyberSource.Client;

namespace Soriana.PPS.PaymentOrderProcess.ClosureOrderGrocery
{
    public class ClosureOrderGroceryFunction
    {
        #region Private Fields
        private readonly ILogger<ClosureOrderGroceryFunction> _Logger;
        private readonly IClosureOrderGroceryService _ClosureOrderGroceryService;
        #endregion

        #region Constructors
        public ClosureOrderGroceryFunction(IClosureOrderGroceryService closureOrderGroceryService,
                                   ILogger<ClosureOrderGroceryFunction> logger)
        {
            _Logger = logger;
            _ClosureOrderGroceryService = closureOrderGroceryService;
        }
        #endregion
        [FunctionName(ClosureOrderGroceryConstants.CLOSURE_ORDER_GROCERY_FUNCTION_NAME)]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest request)
        {
            CapturesPaymentsRequest capturesPaymentsRequest = null;
            try
            {
                _Logger.LogInformation(string.Format(FunctionAppConstants.FUNCTION_EXECUTING_MESSAGE, ClosureOrderGroceryConstants.CLOSURE_ORDER_GROCERY_FUNCTION_NAME));
                if (!request.Body.CanSeek)
                    throw new Exception(JsonConvert.SerializeObject(new BusinessResponse() { StatusCode = (int)HttpStatusCode.BadRequest, Description = HttpStatusCode.BadRequest.ToString(), DescriptionDetail = ClosureOrderGroceryConstants.CLOSURE_ORDER_GROCERY_NO_CONTENT_REQUEST, ContentRequest = null }));
                request.Body.Position = 0;
                string jsonPaymentOrderProcessRequest = await new StreamReader(request.Body).ReadToEndAsync();

                ClosureOrderGroceyRequest paymentOrderProcessRequest = JsonConvert.DeserializeObject<ClosureOrderGroceyRequest>(jsonPaymentOrderProcessRequest);

                var result = await _ClosureOrderGroceryService.ClosureGrocery(paymentOrderProcessRequest);

                //capturesPaymentsRequest = _Mapper.Map<CapturesPaymentsRequest>(paymentOrderProcessRequest);
                //CapturesPayments201Response capturesPaymentsResponse = await _CapturePaymentService.CapturePayment(capturesPaymentsRequest);

                return new OkObjectResult(result);
            }
            catch (BusinessException ex)
            {
                _Logger.LogError(ex, ClosureOrderGroceryConstants.CLOSURE_ORDER_GROCERY_FUNCTION_NAME);
                return new BadRequestObjectResult(ex);
            }
            catch (ApiException ex)
            {
                _Logger.LogError(ex, ClosureOrderGroceryConstants.CLOSURE_ORDER_GROCERY_FUNCTION_NAME);
                string description = string.Concat(ex.ErrorCode.ToString(), CharactersConstants.ESPACE_CHAR, CharactersConstants.HYPHEN_CHAR, CharactersConstants.ESPACE_CHAR, ClosureOrderGroceryConstants.CLOSURE_ORDER_GROCERY_FUNCTION_NAME);
                return new BadRequestObjectResult(new BusinessResponse()
                {
                    StatusCode = ex.ErrorCode,
                    Description = description,
                    DescriptionDetail = ex,
                    ContentRequest = capturesPaymentsRequest
                });
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, ClosureOrderGroceryConstants.CLOSURE_ORDER_GROCERY_FUNCTION_NAME);
                return new BadRequestObjectResult(new BusinessResponse()
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Description = string.Concat(HttpStatusCode.InternalServerError.ToString(), CharactersConstants.ESPACE_CHAR, CharactersConstants.HYPHEN_CHAR, CharactersConstants.ESPACE_CHAR, ClosureOrderGroceryConstants.CLOSURE_ORDER_GROCERY_FUNCTION_NAME),
                    DescriptionDetail = ex,
                    ContentRequest = capturesPaymentsRequest
                });
            }
        }
    }
}
