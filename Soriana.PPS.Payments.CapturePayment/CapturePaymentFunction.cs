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
using Soriana.PPS.Common.DTO.Cybersource.Payments;
using Soriana.PPS.Common.DTO.Salesforce;
using Soriana.PPS.Payments.CapturePayment.Constants;
using Soriana.PPS.Payments.CapturePayment.Services;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Soriana.PPS.Payments.CapturePayment
{
    public class CapturePaymentFunction
    {
        #region Private Fields
        private readonly ICapturePaymentService _CapturePaymentService;
        private readonly ILogger<CapturePaymentFunction> _Logger;
        private readonly IMapper _Mapper;
        #endregion
        #region Constructors
        public CapturePaymentFunction(ICapturePaymentService capturePaymentService,
                                        IMapper mapper,
                                        ILogger<CapturePaymentFunction> logger)
        {
            _CapturePaymentService = capturePaymentService;
            _Mapper = mapper;
            _Logger = logger;
        }
        #endregion
        #region Public Methods
        [FunctionName(CapturePaymentConstants.CAPTURE_PAYMENT_FUNCTION_NAME)]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest request)
        {
            CapturesPaymentsRequest capturesPaymentsRequest = null;
            try
            {
                _Logger.LogInformation(string.Format(FunctionAppConstants.FUNCTION_EXECUTING_MESSAGE, CapturePaymentConstants.CAPTURE_PAYMENT_FUNCTION_NAME));
                if (!request.Body.CanSeek)
                    throw new Exception(JsonConvert.SerializeObject(new BusinessResponse() { StatusCode = (int)HttpStatusCode.BadRequest, Description = HttpStatusCode.BadRequest.ToString(), DescriptionDetail = CapturePaymentConstants.CAPTURE_PAYMENT_NO_CONTENT_REQUEST, ContentRequest = null }));
                request.Body.Position = 0;
                string jsonPaymentOrderProcessRequest = await new StreamReader(request.Body).ReadToEndAsync();
                PaymentOrderProcessRequest paymentOrderProcessRequest = JsonConvert.DeserializeObject<PaymentOrderProcessRequest>(jsonPaymentOrderProcessRequest);
                capturesPaymentsRequest = _Mapper.Map<CapturesPaymentsRequest>(paymentOrderProcessRequest);
                CapturesPayments201Response capturesPaymentsResponse = await _CapturePaymentService.CapturePayment(capturesPaymentsRequest);
                return new OkObjectResult(capturesPaymentsResponse);
            }
            catch (BusinessException ex)
            {
                _Logger.LogError(ex, CapturePaymentConstants.CAPTURE_PAYMENT_FUNCTION_NAME);
                return new BadRequestObjectResult(ex.Message);
            }
            catch (ApiException ex)
            {
                _Logger.LogError(ex, CapturePaymentConstants.CAPTURE_PAYMENT_FUNCTION_NAME);
                string description = string.Concat(ex.ErrorCode.ToString(), CharactersConstants.ESPACE_CHAR, CharactersConstants.HYPHEN_CHAR, CharactersConstants.ESPACE_CHAR, CapturePaymentConstants.CAPTURE_PAYMENT_FUNCTION_NAME);
                return new BadRequestObjectResult(new BusinessResponse()
                {
                    StatusCode = ex.ErrorCode,
                    Description = description,
                    DescriptionDetail = ex.Message,
                    ContentRequest = JsonConvert.SerializeObject(capturesPaymentsRequest),
                    ContentResponse = JsonConvert.SerializeObject(ex)
                });
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, CapturePaymentConstants.CAPTURE_PAYMENT_FUNCTION_NAME);
                return new BadRequestObjectResult(new BusinessResponse()
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Description = string.Concat(HttpStatusCode.InternalServerError.ToString(), CharactersConstants.ESPACE_CHAR, CharactersConstants.HYPHEN_CHAR, CharactersConstants.ESPACE_CHAR, CapturePaymentConstants.CAPTURE_PAYMENT_FUNCTION_NAME),
                    DescriptionDetail = ex.Message,
                    ContentRequest = JsonConvert.SerializeObject(capturesPaymentsRequest),
                    ContentResponse = JsonConvert.SerializeObject(ex)
                });
            }
        }
        #endregion
    }
}

