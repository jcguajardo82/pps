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
using Soriana.PPS.Common.DTO.Cybersource.PayerAuthentication;
using Soriana.PPS.Common.DTO.Salesforce;
using Soriana.PPS.PayerAuth.Enrollment.Constants;
using Soriana.PPS.PayerAuth.Enrollment.Services;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Soriana.PPS.PayerAuth.Enrollment
{
    public class EnrollmentFunction
    {
        #region Private Fields
        private readonly IEnrollmentService _EnrollmentService;
        private readonly ILogger<EnrollmentFunction> _Logger;
        private readonly IMapper _Mapper;
        #endregion
        #region Constructors
        public EnrollmentFunction(IEnrollmentService enrollmentService,
                                    IMapper mapper,
                                    ILogger<EnrollmentFunction> logger)
        {
            _EnrollmentService = enrollmentService;
            _Mapper = mapper;
            _Logger = logger;
        }
        #endregion
        #region Public Methods
        [FunctionName(EnrollmentConstants.ENROLLMENT_FUNCTION_NAME)]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest request)
        {
            EnrollmentRequest enrollmentRequest = null;
            try
            {
                _Logger.LogInformation(string.Format(FunctionAppConstants.FUNCTION_EXECUTING_MESSAGE, EnrollmentConstants.ENROLLMENT_FUNCTION_NAME));
                if (!request.Body.CanSeek)
                    throw new Exception(JsonConvert.SerializeObject(new BusinessResponse() { StatusCode = (int)HttpStatusCode.BadRequest, Description = HttpStatusCode.BadRequest.ToString(), DescriptionDetail = EnrollmentConstants.ENROLLMENT_NO_CONTENT_REQUEST, ContentRequest = null }));
                request.Body.Position = 0;
                string jsonPaymentOrderProcessRequest = await new StreamReader(request.Body).ReadToEndAsync();
                PaymentOrderProcessRequest paymentOrderProcessRequest = JsonConvert.DeserializeObject<PaymentOrderProcessRequest>(jsonPaymentOrderProcessRequest);
                enrollmentRequest = _Mapper.Map<EnrollmentRequest>(paymentOrderProcessRequest);
                Enrollment201Response enrollmentResponse = await _EnrollmentService.Enrollment(enrollmentRequest);
                return new OkObjectResult(enrollmentResponse);
            }
            catch (BusinessException ex)
            {
                _Logger.LogError(ex, EnrollmentConstants.ENROLLMENT_FUNCTION_NAME);
                return new BadRequestObjectResult(ex.Message);
            }
            catch (ApiException ex)
            {
                _Logger.LogError(ex, EnrollmentConstants.ENROLLMENT_FUNCTION_NAME);
                string description = string.Concat(ex.ErrorCode.ToString(), CharactersConstants.ESPACE_CHAR, CharactersConstants.HYPHEN_CHAR, CharactersConstants.ESPACE_CHAR, EnrollmentConstants.ENROLLMENT_FUNCTION_NAME);
                return new BadRequestObjectResult(new BusinessResponse()
                {
                    StatusCode = ex.ErrorCode,
                    Description = description,
                    DescriptionDetail = ex.Message,
                    ContentRequest = JsonConvert.SerializeObject(enrollmentRequest),
                    ContentResponse = JsonConvert.SerializeObject(ex)
                });
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, EnrollmentConstants.ENROLLMENT_FUNCTION_NAME);
                return new BadRequestObjectResult(new BusinessResponse()
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Description = string.Concat(HttpStatusCode.InternalServerError.ToString(), CharactersConstants.ESPACE_CHAR, CharactersConstants.HYPHEN_CHAR, CharactersConstants.ESPACE_CHAR, EnrollmentConstants.ENROLLMENT_FUNCTION_NAME),
                    DescriptionDetail = ex.Message,
                    ContentRequest = JsonConvert.SerializeObject(enrollmentRequest),
                    ContentResponse = JsonConvert.SerializeObject(ex)
                });
            }
        }
        #endregion
    }
}

