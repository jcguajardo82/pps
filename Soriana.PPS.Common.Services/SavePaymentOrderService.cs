using Microsoft.Extensions.Logging;
using Soriana.PPS.Common.Constants;
using Soriana.PPS.Common.DTO.Common;
using Soriana.PPS.Common.DTO.Salesforce;
using Soriana.PPS.Common.Enums;
using Soriana.PPS.Common.Services.Constants;
using Soriana.PPS.DataAccess.PaymentProcess;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Soriana.PPS.Common.Services
{
    public sealed class SavePaymentOrderService : ServiceBase, ISavePaymentOrderService
    {
        #region Private Fields
        private readonly ILogger<SavePaymentOrderService> _Logger;
        private readonly IPaymentProcessContext _PaymentProcessorContext;
        #endregion
        #region Constructors
        public SavePaymentOrderService(IPaymentProcessContext paymentProcessContext,
                                        ILogger<SavePaymentOrderService> logger)
        {
            _PaymentProcessorContext = paymentProcessContext;
            _Logger = logger;
        }
        #endregion
        #region Public Methods
        public async Task InsertPaymentOrderAsync(PaymentOrderProcessRequest request)
        {
            ValidateRequest(request);
            await _PaymentProcessorContext.InsertPaymentOrderAsync(request);
            ValidateResponse(request);
        }
        #endregion
        #region Protected Methods
        protected override void ValidateRequest(object request)
        {
            PaymentOrderProcessRequest paymentOrderProcessRequest = request as PaymentOrderProcessRequest;
            if (paymentOrderProcessRequest == null ||
                paymentOrderProcessRequest.Shipments == null ||
                paymentOrderProcessRequest.Shipments.Any(s => s == null) ||
                !paymentOrderProcessRequest.Shipments.Any() ||
                paymentOrderProcessRequest.Shipments.FirstOrDefault().Items == null ||
                !paymentOrderProcessRequest.Shipments.FirstOrDefault().Items.Any() ||
                paymentOrderProcessRequest.Shipments.FirstOrDefault().Items.Any(i => i == null))
                throw new BusinessException(new BusinessResponse()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Description = HttpStatusCode.BadRequest.ToString(),
                    DescriptionDetail = ServicesCommonConstants.SAVE_PAYMENT_ORDER_SERVICE_NAME,
                    ContentRequest = paymentOrderProcessRequest
                })
                {
                    ServiceInterface = ServicesEnum.SavePaymentOrder.ToString()
                };
        }

        protected override void ValidateResponse(object response, object request = null)
        {
            PaymentOrderProcessRequest paymentOrderProcessRequest = response as PaymentOrderProcessRequest;
            string message = string.Empty;
            if (!(paymentOrderProcessRequest.PaymentOrderID > 0))
                throw new BusinessException(new BusinessResponse()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Description = HttpStatusCode.BadRequest.ToString(),
                    DescriptionDetail = string.Concat(ServicesCommonConstants.SAVE_PAYMENT_ORDER_SERVICE_NAME, CharactersConstants.ESPACE_CHAR, CharactersConstants.HYPHEN_CHAR, CharactersConstants.ESPACE_CHAR, message),
                    ContentRequest = response,
                    ContentResponse = response
                })
                {
                    ServiceInterface = ServicesEnum.SavePaymentOrder.ToString(),
                    ExecutedInnerService = ServicesEnum.SavePaymentOrder.ToString()
                };
        }
        #endregion
    }
}
