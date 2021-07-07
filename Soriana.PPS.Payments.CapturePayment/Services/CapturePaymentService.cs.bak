using AutoMapper;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Soriana.PPS.Common.Constants;
using Soriana.PPS.Common.DTO.Common;
using Soriana.PPS.Common.DTO.Cybersource.Payments;
using Soriana.PPS.Common.Enums;
using Soriana.PPS.Common.Services;
using Soriana.PPS.Payments.CapturePayment.Constants;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Soriana.PPS.Payments.CapturePayment.Services
{
    public class CapturePaymentService : ServiceBase, ICapturePaymentService
    {
        #region Private Fields
        private readonly ILogger<CapturePaymentService> _Logger;
        private readonly ICapturesAPI _CapturesAPI;
        private readonly IMapper _Mapper;
        #endregion
        #region Constructors
        public CapturePaymentService(ICapturesAPI capturesAPI,
                                        IMapper mapper,
                                        ILogger<CapturePaymentService> logger)
        {
            _Logger = logger;
            _Mapper = mapper;
            _CapturesAPI = capturesAPI;
        }
        #endregion
        #region Public Methods
        public async Task<CapturesPayments201Response> CapturePayment(CapturesPaymentsRequest capturesPaymentsRequest)
        {
            ValidateRequest(capturesPaymentsRequest);
            _Logger.LogInformation(string.Format(WebAPIConstants.WEB_API_EXECUTE_MESSAGE, ServicesEnum.CapturePayment.ToString()));
            _Logger.LogInformation(string.Format(WebAPIConstants.WEB_API_CALLING_WITH_DATA_REQUEST_MESSAGE, ServicesEnum.CapturePayment, JsonConvert.SerializeObject(capturesPaymentsRequest)));
            CapturesPayments201Response capturesPaymentsResponse = _Mapper.Map<CapturesPayments201Response>(await _CapturesAPI.CapturePaymentAsync(capturesPaymentsRequest, capturesPaymentsRequest.TransactionAuthorizationId));
            _Logger.LogInformation(string.Format(WebAPIConstants.WEB_API_EXECUTED_MESSAGE, ServicesEnum.CapturePayment.ToString()));
            ValidateResponse(capturesPaymentsResponse, capturesPaymentsRequest);
            return capturesPaymentsResponse;
        }

        protected override void ValidateRequest(object request)
        {
            CapturesPaymentsRequest capturesPaymentsRequest = request as CapturesPaymentsRequest;
            if (capturesPaymentsRequest == null ||
                capturesPaymentsRequest.ClientReferenceInformation == null ||
                string.IsNullOrEmpty(capturesPaymentsRequest.ClientReferenceInformation.Code) ||
                capturesPaymentsRequest.PaymentInformation == null ||
                capturesPaymentsRequest.PaymentInformation.Customer == null ||
                string.IsNullOrEmpty(capturesPaymentsRequest.PaymentInformation.Customer.CustomerId) ||
                capturesPaymentsRequest.OrderInformation == null ||
                capturesPaymentsRequest.OrderInformation.AmountDetails == null ||
                string.IsNullOrEmpty(capturesPaymentsRequest.OrderInformation.AmountDetails.Currency) ||
                capturesPaymentsRequest.OrderInformation.LineItems == null ||
                capturesPaymentsRequest.OrderInformation.LineItems.Count == 0 ||
                capturesPaymentsRequest.DeviceInformation == null ||
                ///TODO: Pendiente validar el envio de Device Information
                //string.IsNullOrEmpty(capturesPaymentsRequest.DeviceInformation.HostName) ||
                //string.IsNullOrEmpty(capturesPaymentsRequest.DeviceInformation.IpAddress) ||
                //string.IsNullOrEmpty(capturesPaymentsRequest.DeviceInformation.UserAgent) ||
                ///TODO: Validar como enviar la estructura de MDD
                capturesPaymentsRequest.MerchantDefinedInformation == null ||
                !capturesPaymentsRequest.MerchantDefinedInformation.Any() ||
                string.IsNullOrEmpty(capturesPaymentsRequest.TransactionAuthorizationId)
                )
                throw new BusinessException(new BusinessResponse()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Description = HttpStatusCode.BadRequest.ToString(),
                    DescriptionDetail = CapturePaymentConstants.CAPTURE_PAYMENT_SERVICE_NAME,
                    ContentRequest = capturesPaymentsRequest
                })
                {
                    ServiceInterface = ServicesEnum.CapturePayment.ToString()
                };
        }

        protected override void ValidateResponse(object response, object request = null)
        {
            CapturesPayments201Response capturesPaymentsResponse = response as CapturesPayments201Response;
            if (capturesPaymentsResponse == null ||
                !Enum.GetNames(typeof(CapturePaymentStatus201Enum)).Any(pS => pS == capturesPaymentsResponse.Status))
            {
                throw new BusinessException(new BusinessResponse()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Description = HttpStatusCode.BadRequest.ToString(),
                    DescriptionDetail = CapturePaymentConstants.CAPTURE_PAYMENT_SERVICE_NAME,
                    ContentRequest = request,
                    ContentResponse = capturesPaymentsResponse
                })
                {
                    ServiceInterface = ServicesEnum.CapturePayment.ToString(),
                    ExecutedInnerService = ServicesEnum.CapturePayment.ToString()
                };
            }
        }
        #endregion
    }
}