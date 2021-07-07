using Microsoft.Extensions.Logging;
using Soriana.PPS.Common.Constants;
using Soriana.PPS.Common.DTO.Common;
using Soriana.PPS.Common.DTO.Salesforce;
using Soriana.PPS.Common.Enums;
using Soriana.PPS.Common.Services.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Soriana.PPS.Common.Services
{
    public class SequencePaymentOrderTransactionService : ServiceBase, ISequencePaymentOrderTransactionService
    {
        #region Private Fields
        private readonly ILogger<SequencePaymentOrderTransactionService> _Logger;
        #endregion
        #region Constructors
        public SequencePaymentOrderTransactionService(ILogger<SequencePaymentOrderTransactionService> logger)
        {
            _Logger = logger;
        }
        #endregion
        #region Public Methods
        public IList<PaymentOrderProcessRequest> GetSequencePaymentOrderTransaction(params PaymentOrderProcessRequest[] request)
        {
            ValidateRequest(request);
            IList<PaymentOrderProcessRequest> paymentOrderProcessRequestList = request.OrderByDescending(req => Convert.ToDouble(req.OrderAmount)).ToList();
            for (byte i = 0; i < paymentOrderProcessRequestList.Count; i++)
            {
                if (paymentOrderProcessRequestList[i].Shipments == null || paymentOrderProcessRequestList[i].Shipments.Count == 0)
                {
                    paymentOrderProcessRequestList.Remove(paymentOrderProcessRequestList[i]);
                    i--;
                    continue;
                }
                paymentOrderProcessRequestList[i].SequencePaymentOrder = Convert.ToByte(i + 1);
            }
            return paymentOrderProcessRequestList;
        }

        protected override void ValidateRequest(object request)
        {
            PaymentOrderProcessRequest[] paymentOrderProcessRequestList = request as PaymentOrderProcessRequest[];
            if (paymentOrderProcessRequestList == null ||
                paymentOrderProcessRequestList.Length == 0)
                throw new BusinessException(new BusinessResponse()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Description = HttpStatusCode.BadRequest.ToString(),
                    DescriptionDetail = ServicesCommonConstants.SEQUENCE_PAYMENT_ORDER_TRANSACTION_SERVICE_NAME,
                    ContentRequest = paymentOrderProcessRequestList
                })
                {
                    ServiceInterface = ServicesEnum.SequencePaymentOrderTransaction.ToString()
                };
            if (paymentOrderProcessRequestList.Any(req => !double.TryParse(req.OrderAmount, out double _)))
                throw new BusinessException(new BusinessResponse()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Description = HttpStatusCode.BadRequest.ToString(),
                    DescriptionDetail = string.Concat(ServicesCommonConstants.SEQUENCE_PAYMENT_ORDER_TRANSACTION_SERVICE_NAME, CharactersConstants.ESPACE_CHAR, CharactersConstants.HYPHEN_CHAR, CharactersConstants.ESPACE_CHAR, ServicesCommonConstants.PAYMENT_ORDER_PROCESS_INVALID_ORDER_AMOUNT),
                    ContentRequest = paymentOrderProcessRequestList
                })
                {
                    ServiceInterface = ServicesEnum.SequencePaymentOrderTransaction.ToString()
                };
        }

        protected override void ValidateResponse(object response, object request = null)
        {
            _Logger.LogInformation(string.Format(ServicesCommonConstants.SEQUENCE_PAYMENT_ORDER_TRANSACTION_SERVICE_NO_VALIDATE_RESPONSE, ServicesEnum.SequencePaymentOrderTransaction.ToString()));
        }
        #endregion
    }
}
