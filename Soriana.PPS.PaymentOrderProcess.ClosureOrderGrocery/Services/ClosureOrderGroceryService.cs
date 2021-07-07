using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

using Soriana.PPS.Common.Constants;
using Soriana.PPS.Common.DTO.Common;
using Soriana.PPS.Common.DTO.Cybersource.Payments;
using Soriana.PPS.Common.Enums;
using Soriana.PPS.Common.Services;
using Soriana.PPS.Common.DTO.ClosureOrder;
using Soriana.PPS.Common.DTO.Salesforce;
using Soriana.PPS.DataAccess.PaymentProcess;
using Soriana.PPS.Payments.CapturePayment.Constants;
using Soriana.PPS.Common.DTO.PaymentProcessor;

namespace Soriana.PPS.PaymentOrderProcess.ClosureOrderGrocery.Services
{
    public class ClosureOrderGroceryService : IClosureOrderGroceryService
    {
        #region Private Fields
        private readonly ILogger<ClosureOrderGroceryService> _Logger;
        private readonly ICapturesAPI _CapturesAPI;
        private readonly IMapper _Mapper;
        private readonly ICallCapturePaymentService _CallCapturePaymentService;
        private readonly IPaymentProcessContext _PaymentProcessContext;
        private readonly IResponseXMLService _ResponseXMLService;
        #endregion

        #region Constructor
        public ClosureOrderGroceryService(ICapturesAPI capturesAPI,
                                          IPaymentProcessContext paymentProcessContext,
                                          IResponseXMLService responseXMLService,
                                          ICallCapturePaymentService callCapturePaymentService,
                                          IMapper mapper,
                                          ILogger<ClosureOrderGroceryService> logger)
        {
            _Logger = logger;
            _Mapper = mapper;
            _CapturesAPI = capturesAPI;
            _PaymentProcessContext = paymentProcessContext;
            _CallCapturePaymentService = callCapturePaymentService;
            _ResponseXMLService = responseXMLService;
        }
        #endregion

        #region Public Methods
        public async Task<string> ClosureGrocery(ClosureOrderGroceyRequest closureOrderGrocery)
        {
            var TransactionByOrder = await _PaymentProcessContext.GetTransactionbyOrder(closureOrderGrocery.Id_Num_Orden);
            var RequestOrderTransaction = await _PaymentProcessContext.GetJsonbyOrder(TransactionByOrder[0].PaymentTransactionID);

            decimal amount = decimal.Parse(closureOrderGrocery.Importe) * decimal.Parse("1.20");

            string jsonOrderTransaction = RequestOrderTransaction[0].PaymentTransactionJSONRequest;
            PaymentOrderProcessRequest paymentOrderProcessRequest = JsonConvert.DeserializeObject<PaymentOrderProcessRequest>(jsonOrderTransaction);

            

            TransactionResponse response = await _CallCapturePaymentService.CapturePaymentAsync(paymentOrderProcessRequest, paymentOrderProcessRequest);
         
            return "";
        }
        #endregion

    }
}
