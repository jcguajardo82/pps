using Microsoft.Extensions.Logging;
using Soriana.PPS.Common.DTO.Common;
using Soriana.PPS.Common.DTO.Salesforce;
using Soriana.PPS.Common.Enums;
using Soriana.PPS.Common.Services.Constants;
using Soriana.PPS.DataAccess.PaymentProcess;
using System.Net;
using System.Threading.Tasks;

namespace Soriana.PPS.Common.Services
{
    public class UpdatePaymentTransactionJsonRequestService : ServiceBase, IUpdatePaymentTransactionJsonRequestService
    {
        #region Private Fields
        private readonly ILogger<UpdatePaymentTransactionJsonRequestService> _Logger;
        private readonly IPaymentProcessContext _PaymentProcessorContext;
        #endregion
        #region Constructors
        public UpdatePaymentTransactionJsonRequestService(IPaymentProcessContext paymentProcessContext,
                                                            ILogger<UpdatePaymentTransactionJsonRequestService> logger)
        {
            _PaymentProcessorContext = paymentProcessContext;
            _Logger = logger;
        }
        #endregion
        #region Public Methods
        public async Task UpdatePaymentTransactionJsonRequestAsync(PaymentOrderProcessRequest request)
        {
            ValidateRequest(request);
            await _PaymentProcessorContext.UpdatePaymentTransactionJsonRequestAsync(request);
            ValidateResponse(request);
        }
        #endregion
        #region Protected Methods
        protected override void ValidateRequest(object request)
        {
            PaymentOrderProcessRequest paymentOrderProcessRequest = request as PaymentOrderProcessRequest;
            if (paymentOrderProcessRequest == null ||
                !(paymentOrderProcessRequest.TransactionReferenceID > 0))
                throw new BusinessException(new BusinessResponse()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Description = HttpStatusCode.BadRequest.ToString(),
                    DescriptionDetail = ServicesCommonConstants.UPDATE_PAYMENT_TRANSACTION_JSON_REQUEST_SERVICE_NAME,
                    ContentRequest = paymentOrderProcessRequest
                })
                {
                    ServiceInterface = ServicesEnum.UpdatePaymentTransactionJsonRequest.ToString(),
                    ExecutedInnerService = ServicesEnum.UpdatePaymentTransactionJsonRequest.ToString()
                };
        }

        protected override void ValidateResponse(object response, object request = null)
        {
            _Logger.LogInformation(string.Format(ServicesCommonConstants.SAVE_PAYMENT_TRANSACTION_STATUS_SERVICE_NO_VALIDATE_RESPONSE, ServicesEnum.UpdatePaymentTransactionJsonRequest.ToString()));
        }
        #endregion
    }
}
