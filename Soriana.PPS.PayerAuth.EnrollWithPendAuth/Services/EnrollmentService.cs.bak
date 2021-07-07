using AutoMapper;
using Microsoft.Extensions.Logging;
using Soriana.PPS.Common.Constants;
using Soriana.PPS.Common.DTO.Common;
using Soriana.PPS.Common.DTO.Cybersource.PayerAuthentication;
using Soriana.PPS.Common.Enums;
using Soriana.PPS.Common.Services;
using Soriana.PPS.PayerAuth.Enrollment.Constants;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Soriana.PPS.PayerAuth.Enrollment.Services
{
    public class EnrollmentService : ServiceBase, IEnrollmentService
    {
        #region Private Fields
        private readonly ILogger<EnrollmentService> _Logger;
        private readonly IPayerAuthenticationAPI _PayerAuthenticationAPI;
        private readonly IMapper _Mapper;
        #endregion
        #region Constructors
        public EnrollmentService(IPayerAuthenticationAPI payerAuthenticationAPI,
                                    IMapper mapper,
                                    ILogger<EnrollmentService> logger)
        {
            _Logger = logger;
            _Mapper = mapper;
            _PayerAuthenticationAPI = payerAuthenticationAPI;
        }
        #endregion
        #region Public Methods
        public async Task<Enrollment201Response> Enrollment(EnrollmentRequest enrollmentRequest)
        {
            ValidateRequest(enrollmentRequest);
            _Logger.LogInformation(string.Format(WebAPIConstants.WEB_API_EXECUTE_MESSAGE, ServicesEnum.CheckPayerAuthEnrollment.ToString()));
            Enrollment201Response enrollmentResponse = _Mapper.Map<Enrollment201Response>(await _PayerAuthenticationAPI.CheckPayerAuthEnrollmentAsync(enrollmentRequest));
            _Logger.LogInformation(string.Format(WebAPIConstants.WEB_API_EXECUTED_MESSAGE, ServicesEnum.CheckPayerAuthEnrollment.ToString()));
            ValidateResponse(enrollmentResponse, enrollmentRequest);
            return enrollmentResponse;
        }

        protected override void ValidateRequest(object request)
        {
            EnrollmentRequest enrollmentRequest = request as EnrollmentRequest;
            if (enrollmentRequest == null ||
                enrollmentRequest.ClientReferenceInformation == null ||
                string.IsNullOrEmpty(enrollmentRequest.ClientReferenceInformation.Code) ||
                enrollmentRequest.PaymentInformation == null ||
                enrollmentRequest.PaymentInformation.Customer == null ||
                string.IsNullOrEmpty(enrollmentRequest.PaymentInformation.Customer.CustomerId) ||
                enrollmentRequest.OrderInformation == null ||
                enrollmentRequest.OrderInformation.AmountDetails == null ||
                string.IsNullOrEmpty(enrollmentRequest.OrderInformation.AmountDetails.Currency) ||
                enrollmentRequest.OrderInformation.LineItems == null ||
                enrollmentRequest.OrderInformation.LineItems.Count == 0 ||
                enrollmentRequest.DeviceInformation == null ||
                ///TODO: Validar si aplica enviar la informacion de DeviceInformation
                string.IsNullOrEmpty(enrollmentRequest.DeviceInformation.IpAddress) ||
                ///TODO: Validar como enviar la estructura de MDD
                enrollmentRequest.MerchantDefinedInformation == null ||
                !enrollmentRequest.MerchantDefinedInformation.Any())
                throw new BusinessException(new BusinessResponse()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Description = HttpStatusCode.BadRequest.ToString(),
                    DescriptionDetail = EnrollmentConstants.ENROLLMENT_SERVICE_NAME,
                    ContentRequest = enrollmentRequest
                })
                {
                    ServiceInterface = ServicesEnum.CheckPayerAuthEnrollment.ToString()
                };
        }

        protected override void ValidateResponse(object response, object request = null)
        {
            Enrollment201Response enrollmentResponse = response as Enrollment201Response;
            if (enrollmentResponse == null ||
                !Enum.GetNames(typeof(CheckPayerAuthEnrollment201Enum)).Any(pS => pS == enrollmentResponse.Status))
            {
                throw new BusinessException(new BusinessResponse()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Description = HttpStatusCode.BadRequest.ToString(),
                    DescriptionDetail = EnrollmentConstants.ENROLLMENT_SERVICE_NAME,
                    ContentRequest = request,
                    ContentResponse = enrollmentResponse
                })
                {
                    ServiceInterface = ServicesEnum.CheckPayerAuthEnrollment.ToString(),
                    ExecutedInnerService = ServicesEnum.CheckPayerAuthEnrollment.ToString()
                };
            }
        }
        #endregion
    }
}
