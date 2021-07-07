using Microsoft.Extensions.Logging;
using Soriana.PPS.Common.Constants;
using Soriana.PPS.Common.DTO.Common;
using Soriana.PPS.Common.DTO.Salesforce;
using Soriana.PPS.Common.Enums;
using Soriana.PPS.Common.Mapping.AutoMapper.Converters.Helpers;
using Soriana.PPS.Common.Services.Constants;
using System;
using System.Linq;
using System.Net;

namespace Soriana.PPS.Common.Services
{
    public sealed class SplitPaymentOrderService : ServiceBase, ISplitPaymentOrderService
    {
        #region Private Fields
        private readonly ILogger<SplitPaymentOrderService> _Logger;
        #endregion
        #region Constructors
        /// 
        /// TODO: Validar si la forma de identificar el tipo de mercancia es atraves del campo shippingReferenceNumber
        /// 
        public SplitPaymentOrderService(ILogger<SplitPaymentOrderService> logger) ////IPaymentProcessContext paymentProcessContext,
        {
            /// 
            /// TODO: Validar si la forma de identificar el tipo de mercancia es atraves del campo shippingReferenceNumber
            /// 
            //_PaymentProcessorContext = paymentProcessContext;
            _Logger = logger;
        }
        #endregion
        #region Private Methods
        /// 
        /// TODO: Validar si la forma de identificar el tipo de mercancia es atraves del campo shippingReferenceNumber
        /// 
        //private async Task<IList<ItemResult>> GetNonGroceryItems()
        //{
        //    return await _PaymentProcessorContext.GetNonGroceryItems();
        //}
        /// 
        /// TODO: Validar si la forma de identificar el tipo de mercancia es atraves del campo shippingReferenceNumber
        /// 
        //private async Task<IList<ItemResult>> GetGroceryItems()
        //{
        //    return await _PaymentProcessorContext.GetGroceryItems();
        //}
        /// 
        /// TODO: Validar si la forma de identificar el tipo de mercancia es atraves del campo shippingReferenceNumber
        /// 
        private PaymentOrderProcessRequest GetPaymentOrderProcessGrocery(PaymentOrderProcessRequest paymentOrderProcessRequest) //, IList<ItemResult> itemGroceries
        {
            if (paymentOrderProcessRequest == null || paymentOrderProcessRequest.Shipments == null) return null;
            PaymentOrderProcessRequest requestGrocery = paymentOrderProcessRequest.Clone();
            requestGrocery.Shipments = (from shipment in paymentOrderProcessRequest.Shipments
                                        where Convert.ToInt32(shipment.ShippingReferenceNumber.Split(CharactersConstants.HYPHEN_CHAR)[1]) == (int)MerchandiseTypeEnum.GROCERY
                                        select shipment).ToList();
            requestGrocery.OrderAmount = (from shipment in requestGrocery.Shipments
                                          from item in shipment.Items
                                          select item.ShippintItemTotal).Sum().ToString();
            requestGrocery.MerchandiseType = MerchandiseTypeEnum.GROCERY;
            return requestGrocery;
        }
        /// 
        /// TODO: Validar si la forma de identificar el tipo de mercancia es atraves del campo shippingReferenceNumber
        /// 
        private PaymentOrderProcessRequest GetPaymentOrderProcessNonGrocery(PaymentOrderProcessRequest paymentOrderProcessRequest) //, IList<ItemResult> itemNonGroceries
        {
            if (paymentOrderProcessRequest == null || paymentOrderProcessRequest.Shipments == null) return null;
            PaymentOrderProcessRequest requestNonGrocery = paymentOrderProcessRequest.Clone();
            requestNonGrocery.Shipments = (from shipment in paymentOrderProcessRequest.Shipments
                                           where Convert.ToInt32(shipment.ShippingReferenceNumber.Split(CharactersConstants.HYPHEN_CHAR)[1]) >= (int)MerchandiseTypeEnum.NONGROCERY
                                           select shipment).ToList();
            requestNonGrocery.OrderAmount = (from shipment in requestNonGrocery.Shipments
                                             from item in shipment.Items
                                             select item.ShippintItemTotal).Sum().ToString();
            requestNonGrocery.MerchandiseType = MerchandiseTypeEnum.NONGROCERY;
            return requestNonGrocery;
        }
        #endregion
        #region Public Methods
        public void SplitPaymentOrderByMerchandiseType(PaymentOrderProcessRequest paymentOrderProcessRequest, out PaymentOrderProcessRequest paymentOrderProcessRequestGrocery, out PaymentOrderProcessRequest paymentOrderProcessRequestNonGrocery)
        {
            paymentOrderProcessRequestGrocery = paymentOrderProcessRequestNonGrocery = null;
            ValidateRequest(paymentOrderProcessRequest);
            /// 
            /// TODO: Validar si la forma de identificar el tipo de mercancia es atraves del campo shippingReferenceNumber
            /// 
            //IList<ItemResult> itemGroceryResults = Task.Run(() => GetGroceryItems()).GetAwaiter().GetResult();
            //IList<ItemResult> itemNonGroceryResults = Task.Run(() => GetNonGroceryItems()).GetAwaiter().GetResult();
            paymentOrderProcessRequestGrocery = GetPaymentOrderProcessGrocery(paymentOrderProcessRequest);
            paymentOrderProcessRequestNonGrocery = GetPaymentOrderProcessNonGrocery(paymentOrderProcessRequest);
        }

        protected override void ValidateRequest(object request)
        {
            PaymentOrderProcessRequest paymentOrderProcessRequest = request as PaymentOrderProcessRequest;
            if (paymentOrderProcessRequest == null ||
                paymentOrderProcessRequest.Shipments == null ||
                !paymentOrderProcessRequest.Shipments.Any() ||
                paymentOrderProcessRequest.Shipments.Any(i => i.Items == null || !i.Items.Any())
                )
                throw new BusinessException(new BusinessResponse()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Description = HttpStatusCode.BadRequest.ToString(),
                    DescriptionDetail = ServicesCommonConstants.SPLIT_PAYMENT_ORDER_SERVICE_NAME,
                    ContentRequest = paymentOrderProcessRequest
                })
                {
                    ServiceInterface = ServicesEnum.SplitOrderPayment.ToString()
                };
            PaymentOrderProcessRequestHelper.IsShippingReferenceNumberValid(paymentOrderProcessRequest, ServicesEnum.SplitOrderPayment.ToString());
        }

        protected override void ValidateResponse(object response, object request = null)
        {
            _Logger.LogInformation(string.Format(ServicesCommonConstants.SPLIT_PAYMENT_ORDER_SERVICE_NO_VALIDATE_RESPONSE, ServicesEnum.SplitOrderPayment.ToString()));
        }
        #endregion
    }
}
