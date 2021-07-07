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
    public sealed class SavePaymentTransactionStatusService : ServiceBase, ISavePaymentTransactionStatusService
    {
        #region Private Fields
        private readonly ILogger<SavePaymentTransactionStatusService> _Logger;
        private readonly IPaymentProcessContext _PaymentProcessorContext;
        #endregion
        #region Constructors
        public SavePaymentTransactionStatusService(IPaymentProcessContext paymentProcessContext,
                                                    ILogger<SavePaymentTransactionStatusService> logger)
        {
            _PaymentProcessorContext = paymentProcessContext;
            _Logger = logger;
        }
        #endregion
        #region Public Methods
        public async Task InsertPaymentTransactionStatusAsync(PaymentOrderProcessRequest request)
        {
            ValidateRequest(request);
            await _PaymentProcessorContext.InsertPaymentTrasactionStatusWithTableTypeAsync(request);
            ValidateResponse(request);
        }
        #endregion
        #region Protected Methods
        protected override void ValidateRequest(object request)
        {
            PaymentOrderProcessRequest paymentOrderProcessRequest = request as PaymentOrderProcessRequest;
            if (paymentOrderProcessRequest == null ||
                !(paymentOrderProcessRequest.TransactionReferenceID > 0) ||
                string.IsNullOrEmpty(paymentOrderProcessRequest.PaymentTransactionService) ||
                string.IsNullOrEmpty(paymentOrderProcessRequest.TransactionStatus))
                throw new BusinessException(new BusinessResponse()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Description = HttpStatusCode.BadRequest.ToString(),
                    DescriptionDetail = ServicesCommonConstants.SAVE_PAYMENT_TRANSACTION_STATUS_SERVICE_NAME,
                    ContentRequest = paymentOrderProcessRequest
                })
                {
                    ServiceInterface = ServicesEnum.SavePaymentTransactionStatus.ToString(),
                    ExecutedInnerService = ServicesEnum.SavePaymentTransactionStatus.ToString()
                };
        }

        protected override void ValidateResponse(object response, object request = null)
        {
            _Logger.LogInformation(string.Format(ServicesCommonConstants.SAVE_PAYMENT_TRANSACTION_STATUS_SERVICE_NO_VALIDATE_RESPONSE, ServicesEnum.SavePaymentTransactionStatus.ToString()));
        }
        #endregion
    }
}
